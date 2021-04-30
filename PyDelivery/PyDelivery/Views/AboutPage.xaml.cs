using Microsoft.AppCenter.Analytics;
using Plugin.Connectivity;
using PyDelivery.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
          
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

            var app = Application.Current as App;
            bool answer = await DisplayAlert("Log Out", "Do you really want to log out?", "Yes", "no");
            if (answer)
            {
                app.IsLoggedIn = false;
                app.UserId = null;
                app.UserRole = null;
                Application.Current.MainPage = new LogInPage();
            }

            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");

            }

        }

        [Obsolete]
        private void LearnMoreBtn(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

            Device.OpenUri(new Uri("https://docs.getpy.biz/PYdelivery"));
            var app = Application.Current as App;
            Analytics.TrackEvent("Learn More Button clicked", new Dictionary<string, string> {
                            { "UserPhoneNumber", app.UserPhoneNumber }
                            });

            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }

        }
    }
    }
