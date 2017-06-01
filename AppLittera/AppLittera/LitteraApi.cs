using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using AppLittera.Model;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace AppLittera
{
    #region Library
    public class jsonLibrary
    {
        public int status { get; set; }
        public bool success { get; set; }
        public List<jsonLibraryItem> data { get; set; }
    }

    public class jsonLibraryItem
    {
        public jsonLibraryDetail _id_book { get; set; }
        public int visible { get; set; }
        public int favorite { get; set; }
        public string date_saved { get; set; }
    }

    public class jsonLibraryDetail
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string synopsis { get; set; }
        public string content { get; set; }
        public string cover_image { get; set; }
    }
    #endregion

    #region User
    public class jsonUser
    {
        public int status { get; set; }
        public bool success { get; set; }
        public jsonUserInfo data { get; set; }
    }

    public class jsonUserInfo
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string cover_image { get; set; }
    }
    #endregion

    class LitteraApi
    {
        /// <summary>
        /// Get all books from user's library
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<Book>> GetLibrary()
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            String path = String.Format(Url.LIBRARYINFO, App.User.Username);
            String json;
            try
            {
                json = await Request.GetAsyncJson(path);
            }
            catch (Exception)
            {
                json = "";
            }
            
            JContainer container = (JContainer)JsonConvert.DeserializeObject(json);

            if (!String.IsNullOrEmpty(json) && (Boolean)container["success"])
            {
                try
                {
                    jsonLibrary libraries = JsonConvert.DeserializeObject<jsonLibrary>(json);

                    foreach (var library in libraries.data)
                    {
                        books.Add(
                            new Book()
                            {
                                Id_api = library._id_book._id,
                                Title = library._id_book.title,
                                Subtitle = library._id_book.subtitle,
                                Synopsis = library._id_book.synopsis,
                                Content = library._id_book.content
                            }
                        );
                    }
                }
                catch (Exception)
                {
                    books = null;
                }

            }
            else
                books = null;

            return books;
        }

        /// <summary>
        /// Get all books from user's library
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetUser()
        {
            User user = null;
            String path = String.Format(Url.USER, App.User.Username);
            String json = await Request.GetAsyncJson(path);

            if (!String.IsNullOrEmpty(json))
            {
                try
                {
                    jsonUser jUser = JsonConvert.DeserializeObject<jsonUser>(json);

                    if (jUser.data != null)
                    {
                        user = new User()
                        {
                            Id_api = jUser.data._id,
                            Name = jUser.data.name,
                            Username = jUser.data.username,
                            Email = jUser.data.email,
                            CoverImage = jUser.data.cover_image
                        };
                    }
                }
                catch (Exception)
                {
                    user = null;
                }
                
            }

            return user;
        }

        public async Task<String> Authenticate(Credentials credentials)
        {
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<String, String>("username", credentials.Username),
                new KeyValuePair<String, String>("email", credentials.Email),
                new KeyValuePair<String, String>("password", credentials.Password)
            });

            JContainer data = await Request.PostAsync(Url.AUTHENTICATE, content);

            if (data == null)
                return "*erro*";
            else if ((Boolean)data["success"])
                return (String)data["data"];
            else
                return "";

        }
    }
}