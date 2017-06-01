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

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await Task.Delay(100);

            Boolean ret = DependencyService.Get<IFileHelper>().DownloadFile("http://www.axmag.com/download/pdfurl-guide.pdf",
                                                                            //((Book)e.SelectedItem).Content
                                                                            String.Format("book_{0}.epub", ((Book)e.SelectedItem).Id_api));

            DependencyService.Get<IFileHelper>().OpenPdf(String.Format("book_{0}.epub", ((Book)e.SelectedItem).Id_api));

            //EpubBook epub = EpubReader.ReadBook(DependencyService.Get<IFileHelper>().GetLocalFilePath(String.Format("book_{0}.epub", ((Book)e.SelectedItem).Id_api)));

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
