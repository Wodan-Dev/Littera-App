using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using AppLittera;
using Xamarin.Forms.Platform.Android;
using AppLittera.Droid;
using System.Net;

[assembly: ExportRenderer(typeof(EbookViewer), typeof(EbookViewerRenderer))]
namespace AppLittera.Droid
{
    public class EbookViewerRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as EbookViewer;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.LoadUrl(
                    string.Format("file:///android_asset/epubjs/epub.html?file={0}", string.Format("file://{0}", WebUtility.UrlEncode(customWebView.Uri))));
            }
        }
    }
}