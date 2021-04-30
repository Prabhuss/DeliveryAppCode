using Android.App;
using Android.Content;
using Android.Gms.Auth.Api.Phone;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using PyDelivery.Controls;
using PyDelivery.Droid;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(ListenToSms))]
namespace PyDelivery.Droid
{
    public class ListenToSms : IListenToSmsRetriever
    {
        public void ListenToSmsRetriever()
        {

            SmsRetrieverClient client = SmsRetriever.GetClient(Application.Context);
            var task = client.StartSmsRetriever();
            task.AddOnSuccessListener(new SuccessListener());
            task.AddOnFailureListener(new FailureListener());
        }
        private class SuccessListener : Object, IOnSuccessListener
        {
            public void OnSuccess(Object result)
            {
            }
        }
        private class FailureListener : Object, IOnFailureListener
        {
            public void OnFailure(Exception e)
            {
            }
        }
    }
}