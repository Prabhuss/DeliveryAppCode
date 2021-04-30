using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtpPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        private Dictionary<string, string> loginData;
        private const string logInStatus = "logInStatus";
        /*OTPVerificationPage vModel;*/
        private Page page;
        public OtpPage()
        {
            InitializeComponent();
           
            this.IsBusy = false;
            otpEntry.Completed += (s, e) => {
                veriftBtn_Clicked(s, e);
            };

            MessagingCenter.Subscribe<string>(this, "ReceivedOTP", (message) =>
            {
                string[] words = message.Split();
                foreach (string item in words.ToList())
                {
                    var isNumeric = int.TryParse(item, out int n);
                    if (isNumeric)
                    {
                        otpEntry.Text = item;
                        //DisplayAlert("Message", $"OTP is {item}", "Ok");
                        break;
                    }
                }
            });
            /* vModel = new OTPVerificationPage();
            BindingContext = new OTPVerificationPage();*/
        }

        public OtpPage(INavigation Navigation)
        {

        }
        private async void veriftBtn_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

            string UserOTP = otpEntry.Text;
            var app = Application.Current as App;
            this.IsBusy = true;
            veriftBtn.IsEnabled = false;
            if (UserOTP.Length != 6)
            {
                await DisplayAlert("Incorrect OTP", "Please enter a 6 digit OTP.", "OK");
                this.IsBusy = false;
                veriftBtn.IsEnabled = true;
                return;
            }
            Debug.WriteLine(@"login data " + loginData["phoneNumber"]);
            var Validateotp_response = await RestService.Instance.VerifyOtp(loginData["phoneNumber"], UserOTP);
            Debug.WriteLine(@"Response received " + Validateotp_response);
            if (Validateotp_response == "Success")
            {
                app.IsLoggedIn = true;
                app.UserId = loginData["Id"];
                app.UserKey = loginData["AccessKey"];
                app.UserRole = loginData["role"];
                app.UserName = loginData["name"];
                app.UserPhoneNumber = loginData["phoneNumber"];
                MainPage MyMainPage = new MainPage();
                Application.Current.MainPage = MyMainPage;
                this.IsBusy = false;
                veriftBtn.IsEnabled = true;
                /*await Navigation.PushAsync(new BottomNavigation());*/
            }
            else
            {
                this.IsBusy = false;
                veriftBtn.IsEnabled = true;
                await DisplayAlert("Warning", "OTP is invalid", "ok");
            }

            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");

            }

        }

        internal void Initialize(Dictionary<string, string> dictionary)
        {
            loginData = dictionary;
            try
            {
                otpCaption.Text += @" " + loginData["phoneNumber"];
            }
            catch
            {
            }
        }

        public void Initialize(object _loginData, Page page)
        {
            if (_loginData != null && _loginData is Dictionary<string, string>)
            {
                loginData = (Dictionary<string, string>)_loginData;
            }
            this.page = page;
        }

        private void Btn_changeNumber_Clicked(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
            
        }

    }
}