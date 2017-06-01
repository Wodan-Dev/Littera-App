using AppLittera.Model;
using AppLittera.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppLittera.ViewModel
{
    public class UserVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand BtnSairClickCommand { get; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ProfilePic
        {
            get { return GetUserImage(); }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set
            {
                Name = value;
                OnPropertyChanged();
            }
        }

        private String username;
        public String Username
        {
            get { return username; }
            set
            {
                Username = value;
                OnPropertyChanged();
            }
        }

        public UserVM()
        {
            name = App.User.Name;
            username = App.User.Username;

            BtnSairClickCommand = new Command(BtnSairClick);

            /*profilePic = new Image { Aspect = Aspect.AspectFit };
            profilePic = new Image { Aspect = Aspect.AspectFit };
            try
            {
                profilePic.Source = ImageSource.FromUri(new Uri("https://static.cineclick.com.br/sites/adm/uploads/banco_imagens/31/602x0_1439644246.jpg"));
            }
            catch (Exception)
            {
                profilePic = null;
            }*/
        }

        private ImageSource GetUserImage()
        {
            ImageSource img = null;

            try
            {
                img = new UriImageSource()
                {
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(1000, 0, 0, 0),
                    Uri = new Uri(App.User.CoverImage)
                    //Uri = new Uri("https://static.cineclick.com.br/sites/adm/uploads/banco_imagens/31/602x0_1439644246.jpg")
                };
            }
            catch (Exception)
            {
            }

            return img;
        }
        
        private async void BtnSairClick()
        {
            Boolean confirm = await App.Current.MainPage.DisplayAlert("Acesso", "Deseja realmente sair?", "Sim", "Não");

            if (confirm)
            {
                IsBusy = true;

                await App.UserDB.DeleteUserAsync(App.User);
                List<Book> library = await App.BookDB.GetBookAsync();

                if (library != null)
                {
                    foreach (Book book in library)
                        await App.BookDB.DeleteBookAsync(book);
                }
                
                App.User = new Model.User();

                App.Current.MainPage = new NavigationPage(new LoginPage());

                IsBusy = false;
            }

        }
    }
}
