using AppLittera.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppLittera.ViewModel
{
    public class BooksVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }

        private ObservableCollection<Book> _books;

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged(); }
        }

        public BooksVM()
        {
            _books = new ObservableCollection<Book>();

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }
        
        public ICommand RefreshDataCommand { get; }

        public async Task RefreshData()
        {
            if (!IsBusy)
            {
                LitteraApi api = new LitteraApi();
                List<Book> libraryDB;
                ObservableCollection<Book> libraryApi;
                _books.Clear();
                IsBusy = true;
                libraryApi = await api.GetLibrary();

                if (libraryApi != null)
                {
                    foreach (Book book in libraryApi)
                    {
                        if (await App.BookDB.GetBookAsync(book.Id_api) == null)
                            await App.BookDB.SaveBookAsync(book);
                    }
                }

                libraryDB = await App.BookDB.GetBookAsync();

                foreach (Book book in libraryDB)
                    _books.Add(book);

                IsBusy = false;
            }
        }
    }
}
