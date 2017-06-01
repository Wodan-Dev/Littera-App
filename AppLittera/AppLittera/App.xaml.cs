using AppLittera.Database;
using AppLittera.Model;
using AppLittera.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AppLittera
{
    public partial class App : Application
    {
        static DBUser userDB;
        static DBBook bookDB;
        static User user;

        public App()
        {
            InitializeComponent();
            
        }

        public static DBUser UserDB
        {
            get
            {
                if (userDB == null)
                {
                    userDB = new DBUser(DependencyService.Get<IFileHelper>().GetLocalFilePath("litteradb.db3"));
                }
                return userDB;
            }
        }

        public static DBBook BookDB
        {
            get
            {
                if (bookDB == null)
                {
                    bookDB = new DBBook(DependencyService.Get<IFileHelper>().GetLocalFilePath("litteradb.db3"));
                }
                return bookDB;
            }
        }

        public static User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        protected override async void OnStart()
        {
            List<User> users = await UserDB.GetUserAsync();

            if (users.Count == 0)
            {
                MainPage = new NavigationPage(new LoginPage())
                {
                    BarTextColor = Color.Black,
                    BarBackgroundColor = Color.White
                };
            }
            else
            {
                user = users[0];

                TabbedPage page = new TabbedPage() { BarTextColor = Color.Black };
                page.Children.Add(new LibraryPage());
                page.Children.Add(new UserPage());
                
                MainPage = page;
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
