using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Toasts;

namespace AppLittera.Droid
{
    [Activity(Label = "Littera", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //DependencyService.Register<ToastNotification>();
            //ToastNotification.Init(this, new PlatformOptions() { SmallIconDrawable = Android.Resource.Drawable.IcDialogInfo });

            DependencyService.Register<Message>();

            LoadApplication(new App());
            
            //Toast.MakeText(this, "Teste", ToastLength.Short);

            //Window.SetStatusBarColor(Color.White);
        }
    }   
}

