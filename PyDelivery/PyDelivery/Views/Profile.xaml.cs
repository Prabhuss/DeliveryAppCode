using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public Profile()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Button_Clicked();
        }
        private async Task Button_Clicked()
        {
            var app = Application.Current as App;
            bool answer = await Application.Current.MainPage.DisplayAlert("Log Out", "Do you really want to log out?", "Yes", "no");
            if (answer)
            {
                app.IsLoggedIn = false;
                app.UserId = null;
                app.UserRole = null;
                app.UserName = null;
                app.UserPhoneNumber = null;
                Application.Current.MainPage = new LogInPage();
            }
            else
            {
                await RootPage.NavigateFromMenu(0);
            }
        }
    }
}