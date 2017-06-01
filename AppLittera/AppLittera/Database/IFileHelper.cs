using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppLittera.Database
{
    public interface IFileHelper
    {
        String GetLocalFilePath(String fileName);

        void WriteAllBytes(string filename, byte[] bytes/*, Action completed*/);

        Boolean DownloadFile(string url, string filename);

        ImageSource GetImageFile(string fileName);

        void OpenPdf(string filename);
    }
}
