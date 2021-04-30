using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PyDelivery.Droid.FCMDetails))]

namespace PyDelivery.Droid
{

    public class FCMDetails : IFCMDetails
    {
        public async Task<string> GetAppToken()
        {
            IInstanceIdResult result = await FirebaseInstanceId.Instance.GetInstanceId().AsAsync<IInstanceIdResult>();
            int i = 10;
            return result.Token;
        }
    }
}