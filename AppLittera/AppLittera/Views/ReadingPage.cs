using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLittera;
using Xamarin.Forms;

namespace AppLittera.Views
{
    public class ReadingPage : ContentPage
    {
        public ReadingPage(String filePath)
        {
            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout
            {
                Children = {
                    new EbookViewer {
                        Uri = filePath,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }
            };
        }

        protected override bool OnBackButtonPressed()
        {
            TabbedPage page = new TabbedPage() { BarTextColor = Color.Black };
            page.Children.Add(new LibraryPage());
            page.Children.Add(new UserPage());

            App.Current.MainPage = page;

            return true;
        }
    }
}
