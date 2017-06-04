using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppLittera
{
    public class EbookViewer : WebView
    {
        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(
                propertyName: "Uri",
                returnType: typeof(string),
                declaringType: typeof(EbookViewer),
                defaultValue: default(string)
            );

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        
    }
}
