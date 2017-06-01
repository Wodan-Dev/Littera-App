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
using AppLittera;
using Xamarin.Forms;

[assembly: Dependency(typeof(Message))]
namespace AppLittera.Droid
{
    public class Message : IMessage
    {
        public void ShowShortMessage(string text)
        {
            Toast.MakeText(Android.App.Application.Context, text, ToastLength.Short).Show();
        }
    }
}