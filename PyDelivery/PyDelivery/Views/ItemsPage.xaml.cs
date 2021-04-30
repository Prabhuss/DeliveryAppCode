using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PyDelivery.Models;
using PyDelivery.Views;
using PyDelivery.ViewModels;
using Microsoft.AppCenter.Analytics;
using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.Behaviors;
using System.Collections.ObjectModel;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel("NewOrder");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OrderList.Clear();
            viewModel.IsBusy = true;
        }

    }
}