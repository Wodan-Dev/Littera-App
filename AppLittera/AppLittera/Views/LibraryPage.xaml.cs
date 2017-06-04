using AppLittera.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppLittera.Database;
using AppLittera.Model;
//using VersFx.Formats.Text.Epub;
//using VersFx.Formats.Text.Epub;

namespace AppLittera.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibraryPage : ContentPage
    {
        private BooksVM booksVM;
        private Boolean isFirstLoad = true;

        public LibraryPage()
        {
            InitializeComponent();
            booksVM = new BooksVM();

            BindingContext = booksVM;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            if (((Book)e.SelectedItem).Content != "")
            {
                String bookName = String.Format("book_{0}.epub", ((Book)e.SelectedItem).Id_api);

                if (!DependencyService.Get<IFileHelper>().FileExists(bookName))
                {
                    if (!DependencyService.Get<IFileHelper>().DownloadFile(((Book)e.SelectedItem).Content, bookName))
                    {
                        DependencyService.Get<IMessage>().ShowShortMessage("Não foi encontrado o livro escolhido.");
                        return;
                    }
                }

                App.Current.MainPage = new ReadingPage(DependencyService.Get<IFileHelper>().GetLocalFilePath(bookName));
            }
            else
                DependencyService.Get<IMessage>().ShowShortMessage("O arquivo deste livro ainda não está disponível.");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (isFirstLoad)
            {
                await booksVM.RefreshData();
                isFirstLoad = false;
            }
        }

       
    }
}
