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
using PyDelivery.Controls;
using Android.Gms.Auth.Api.Phone;
using PyDelivery.Droid;

[assembly: Dependency(typeof(HashKey_Android))]
namespace PyDelivery.Droid
{
    class HashKey_Android : IHashService
    {

        public string GenerateHashkey()
        {
            return new AppHashKeyHelper().GetAppHashKey(global::Android.App.Application.Context);
        }
        public void StartSMSRetriverReceiver()
        {
            SmsRetrieverClient _client = SmsRetriever.GetClient(global::Android.App.Application.Context);
            // Starts SmsRetriever, which waits for ONE matching SMS message until timeout
            // (5 minutes). The matching SMS message will be sent via a Broadcast Intent with
            // action SmsRetriever#SMS_RETRIEVED_ACTION.
            var task = _client.StartSmsRetriever();

            // You could also Listen for success/failure of StartSmsRetriever initiation
            //task.AddOnSuccessListener(new SuccessListener());
            //task.AddOnFailureListener(new FailureListener());
        }
    }
}