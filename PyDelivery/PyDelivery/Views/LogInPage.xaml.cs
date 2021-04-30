using Newtonsoft.Json;
using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.Models;
using PyDelivery.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.FirebasePushNotification;
//using Plugin.Permissions;
//using Plugin.Permissions.Abstractions;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
                // Define the binding context
                this.BindingContext = this;

                // Set the IsBusy property
                this.IsBusy = false;
                sentOtpBtn.IsEnabled = true;
                phoneNumber.Completed += (s, e) => {
                    sentOtpBtn_Clicked(s, e);
                };
            
        }

        public async void sentOtpBtn_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

            var app = Application.Current as App;
            this.IsBusy = true;
            sentOtpBtn.IsEnabled = false;
            string PhoneNumber = phoneNumber.Text;
            if (PhoneNumber.Length != 10)
            {
                await DisplayAlert("Incorrect Number", "Please enter a 10 digit Phone Number", "OK");
                sentOtpBtn.IsEnabled = true;
                this.IsBusy = false;
                return;
            }
            // Firebase token generation
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"NEW TOKEN : {p.Token}");
            };
            string token = CrossFirebasePushNotification.Current.Token;
            app.FireBaseToken = token;
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
            Debug.WriteLine(@"Invoking the rest  generate otp APi ");
            MerchantApiResponse Merchantdata = await RestService.Instance.GenerateOtp(PhoneNumber, token);
            DependencyService.Get<IHashService>().StartSMSRetriverReceiver();
            Debug.WriteLine(@"Response received " + Merchantdata);
            Debug.WriteLine(@"AccessKey" + Merchantdata.AccessKey);
            if (Merchantdata.Status.ToLower() == "success")
            {
                //CommonServices.ListenToSmsRetriever();
                OtpPage view = new OtpPage();
                view.Initialize(new Dictionary<string, string>()
                {
                    { "phoneNumber", phoneNumber.Text },
                    { "AccessKey", Merchantdata.AccessKey },
                    { "Id", Merchantdata.Data.Id },
                    { "role", Merchantdata.Data.Role },
                    { "name", Merchantdata.Data.Name }
                });
                await Navigation.PushModalAsync(view);
                await Task.Delay(10);
                this.IsBusy = false;
                sentOtpBtn.IsEnabled = true;
            }
            else
            {
                this.IsBusy = false;
                sentOtpBtn.IsEnabled = true;
                await DisplayAlert("Alert", "Phone Number is not registered, please contact your merchant", "ok");
            }

            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }
        }
        /*
        protected async override void OnAppearing()
        {
            if (!Application.Current.Properties.ContainsKey("isSmsPermissionAsked"))
            {
                await CheckSmsPermissions();
            }
            if (!Application.Current.Properties.ContainsKey("isCameraPermissionAsked"))
            {
                await CheckCameraPermissions();
            }
            if (!Application.Current.Properties.ContainsKey("isLocationPermissionAsked"))
            {
                await CheckLocationPermissions();
            }
        }

        private async Task CheckSmsPermissions()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<SmsPermission>();
            if (status != PermissionStatus.Granted)
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<SmsPermission>();
            }
            switch (status)
            {
                case PermissionStatus.Denied:
                    await DisplayAlert("INFO", "SMS permissions are revoked for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Disabled:
                    await DisplayAlert("INFO", "SMS permissions are not enabled for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Restricted:
                    await DisplayAlert("INFO", "SMS permissions are restricted for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Unknown:
                    await DisplayAlert("INFO", "SMS permissions are not exist for this app. Please enter OTP manually", "Ok");
                    break;
            }
            Application.Current.Properties["isSmsPermissionAsked"] = status.ToString();

        }

        private async Task CheckCameraPermissions()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
            if (status != PermissionStatus.Granted)
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
            }
            switch (status)
            {
                case PermissionStatus.Denied:
                    await DisplayAlert("INFO", "Camera permission is revoked for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Disabled:
                    await DisplayAlert("INFO", "Camera permission is not enabled for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Restricted:
                    await DisplayAlert("INFO", "Camera permission is restricted for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Unknown:
                    await DisplayAlert("INFO", "Camera permission is not exist for this app. Please enter OTP manually", "Ok");
                    break;
            }
            Application.Current.Properties["isCameraPermissionAsked"] = status.ToString();
        }

        private async Task CheckLocationPermissions()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>();
            if (status != PermissionStatus.Granted)
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
            }
            switch (status)
            {
                case PermissionStatus.Denied:
                    await DisplayAlert("INFO", "LocationWhenInUse permission is revoked for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Disabled:
                    await DisplayAlert("INFO", "LocationWhenInUse permission is not enabled for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Restricted:
                    await DisplayAlert("INFO", "LocationWhenInUse permission is restricted for this app. Please enter OTP manually", "Ok");
                    break;
                case PermissionStatus.Unknown:
                    await DisplayAlert("INFO", "LocationWhenInUse permission is not exist for this app. Please enter OTP manually", "Ok");
                    break;
            }
            Application.Current.Properties["isLocationPermissionAsked"] = status.ToString();
        }
        */
    }
}