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
using PyDelivery.Controls;
using PyDelivery.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Message_Droid))]
namespace PyDelivery.Droid
{

    public class Message_Droid : IToastMessage
    {

        public void LongTime(string ToastMessage)
        {
            Toast.MakeText(Android.App.Application.Context, ToastMessage, ToastLength.Long).Show();
        }
        public void ShortTime(string ToastMessage)
        {
            Toast.MakeText(Android.App.Application.Context, ToastMessage, ToastLength.Short).Show();
        }
    }
}