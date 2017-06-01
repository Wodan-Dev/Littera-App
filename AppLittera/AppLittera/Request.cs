using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppLittera
{
    public static class Request
    {
        #region Return Request Async
        public static async Task<JContainer> GetAsync(string path)
        {
            HttpResponseMessage respMessage = null;
            JContainer data = null;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                client.DefaultRequestHeaders.Add("x-access-token", App.User.Token);
                try
                {
                    respMessage = await client.GetAsync(path);
                }
                catch (System.Exception)
                {
                    DependencyService.Get<IMessage>().ShowShortMessage("Não foi possível conectar ao Littera");
                }
                if (respMessage != null)
                {
                    var json = respMessage.Content.ReadAsStringAsync().Result;
                    data = (JContainer)JsonConvert.DeserializeObject(json);
                }
            }

            return data;
        }
        #endregion

        #region Return Request Async
        public static async Task<string> GetAsyncJson(string path)
        {
            HttpResponseMessage respMessage = null;
            string json = "";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                client.DefaultRequestHeaders.Add("x-access-token", App.User.Token);

                try
                {
                    respMessage = await client.GetAsync(path);
                }
                catch (System.Exception)
                {
                    DependencyService.Get<IMessage>().ShowShortMessage("Não foi possível conectar ao Littera");
                }

                if (respMessage != null)
                {
                    json = respMessage.Content.ReadAsStringAsync().Result;
                }
            }

            return json;
        }
        #endregion

        #region Return Post Async
        public static async Task<JContainer> PostAsync(string path, HttpContent content)
        {
            HttpResponseMessage respMessage = null;
            JContainer data = null;

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = new System.TimeSpan(0, 0, 10);
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

                try
                {
                    respMessage = await client.PostAsync(path, content);

                    if (respMessage != null)
                    {
                        var json = respMessage.Content.ReadAsStringAsync().Result;
                        data = (JContainer)JsonConvert.DeserializeObject(json);
                    }
                }
                catch (System.Exception)
                {
                    //await App.Current.MainPage.DisplayAlert("Aviso", "Não foi possível conectar ao Littera.", "Ok");
                }
            }

            return data;
        }
        #endregion
    }
}
