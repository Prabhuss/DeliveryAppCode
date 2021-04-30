using PyDelivery.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using PyDelivery.Services;
using Plugin.Connectivity;
using PyDelivery.Controls;

namespace PyDelivery.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Role { get; set; }
        public AboutViewModel()
        {

            var app = Application.Current as App;
            Role = app.UserRole;
            Title = "GetPY";

        }
        public ICommand OpenWebCommand { get; }

    }
}