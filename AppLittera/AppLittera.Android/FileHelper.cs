using System;
using System.IO;
using Xamarin.Forms;
using AppLittera.Droid;
using AppLittera.Database;
using System.Net;
using Android.Content;

[assembly: Dependency(typeof(FileHelper))]
namespace AppLittera.Droid
{
    public class FileHelper : IFileHelper
    {
        public String GetLocalFilePath(String fileName)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
        

        public void WriteAllBytes(string filename, byte[] bytes/*, Action completed*/)
        {
            File.WriteAllBytes(GetLocalFilePath(filename), bytes);

            WebClient client = new WebClient();

            client.DownloadFile("https://bytes.com/images/bytes_logo_O1ZMV80SgZEMWccSaEXSPk.png", "teste.pdf");

            /*if (completed != null)
                completed();*/
        }
        
        public bool DownloadFile(string url, string fileName)
        {
            string path = GetLocalFilePath(fileName);
            Boolean retorno = true;

            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, path);
                }
                catch (Exception)
                {
                    retorno = false;
                }
            }

            return retorno;
        }

        public ImageSource GetImageFile(string fileName)
        {
            return ImageSource.FromFile(GetLocalFilePath(fileName));
        }

        public void OpenPdf(string fileName)
        {
            Android.Net.Uri uri = Android.Net.Uri.Parse(GetLocalFilePath(fileName));
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, "application/pdf");
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

            try
            {
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("No APP", "SEM PDF", "ok");
                //Toast.MakeText(Xamarin.Forms.Forms.Context, "No Application Available to View PDF", ToastLength.Short).Show();
            }
        }

        public Boolean FileExists(string fileName)
        {
            return File.Exists(GetLocalFilePath(fileName));
        }
    }
}