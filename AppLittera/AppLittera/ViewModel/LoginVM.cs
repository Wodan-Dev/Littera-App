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
    class LoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand BtnClickCommand { get; }

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

        public String username;
        public String Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public String email;
        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public String password;
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public LoginVM()
        {
            BtnClickCommand = new Command(BtnEnterClick);
        }

        private async void BtnEnterClick()
        {
            if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(email))
                await App.Current.MainPage.DisplayAlert("Acesso", "Informe o usuário ou e-mail.", "Ok");
            else if (String.IsNullOrEmpty(password))
                await App.Current.MainPage.DisplayAlert("Acesso", "Informe o senha.", "Ok");
            else
            {
                IsBusy = true;

                Credentials credentials = new Credentials()
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                LitteraApi api = new LitteraApi();
                String token = await api.Authenticate(credentials);

                if (!String.IsNullOrEmpty(token))
                {
                    if (token != "*erro*")
                    {
                        App.User = new User()
                        {
                            Username = username
                        };

                        // Busca os dados do usuário
                        User user = await api.GetUser();
                        user.Token = token;
    
                        await App.UserDB.SaveUserAsync(user);

                        // Define the user to te application
                        App.User = user;

                        TabbedPage page = new TabbedPage() { BarTextColor = Color.Black };
                        page.Children.Add(new LibraryPage());
                        page.Children.Add(new UserPage());

                        App.Current.MainPage = page;
                    }
                    else
                        DependencyService.Get<IMessage>().ShowShortMessage("Não foi possível conectar ao Littera");
                }
                else
                    DependencyService.Get<IMessage>().ShowShortMessage("Usuário e senha não encontrados.");

                IsBusy = false;
            }
        }
    }
}
