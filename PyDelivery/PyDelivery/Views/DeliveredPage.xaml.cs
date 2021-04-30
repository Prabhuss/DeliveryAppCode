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
    public partial class DeliveredPage : ContentPage
    {
        public ItemsViewModel viewModel;
        public DeliveredPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel("Delivered");

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OrderList.Clear();
            viewModel.IsBusy = true;
        }

    }
}