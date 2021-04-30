using Microsoft.AppCenter.Analytics;
using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.Models;
using PyDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InProgressPage : ContentPage
    {
        ItemsViewModel viewModel;
        public InProgressPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel("OnTheWay");
        }
        protected override void OnAppearing()
        {
                base.OnAppearing();
                viewModel.OrderList.Clear();
                viewModel.IsBusy = true;
        }

    }
}