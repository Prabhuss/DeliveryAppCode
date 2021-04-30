using Microsoft.AppCenter.Analytics;
using Plugin.Connectivity;
using PyDelivery.Controls;
using PyDelivery.Models;
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
    public partial class OrderListTemplate : ContentView
    {
        public OrderListTemplate()
        {
            InitializeComponent();
        }

        private async void OrderView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                indicator.IsRunning = true;
                await Task.Delay(1500);
                var invoicedetails = e.Item as CustomerInvoiceDatum;
                await Navigation.PushAsync(new ItemDetailPage(invoicedetails));
                var app = Application.Current as App;

                Analytics.TrackEvent("Delivered Page Order clicked", new Dictionary<string, string> {
                                { "UserPhoneNumber", app.UserPhoneNumber },
                                { "Bill Number ", invoicedetails.InvoiceId},
                                { "MerchantBranchId", "180"}

                                });
                indicator.IsRunning = false;
                await Task.Delay(1500);
            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");
            }
        }
    }
}