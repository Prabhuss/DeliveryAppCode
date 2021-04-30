using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using PyDelivery.Models;
using PyDelivery.ViewModels;
using PyDelivery.Services;
using Microsoft.AppCenter.Analytics;
using Plugin.Connectivity;
using PyDelivery.Controls;

namespace PyDelivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public CustomerDetails CustomerInfo { get; set; }

        public AddressDetails AddressInfo { get; set; }

        public ObservableCollection<CustomerInvoiceDatum> OrderList { get; set; }

        public ObservableCollection<InvocieLineItem> ProductList { get; set; }

        public string InProgress = "inprogress";
        public string Accepted = "accepted";
        public string Packed = "packed";
        public string New = "new";
        public string OnTheWay = "on the way";
        public string Delivered = "delivered";
        


        public ItemDetailPage(CustomerInvoiceDatum InvoiceDetails)
        {
            InitializeComponent();
            var app = Application.Current as App;
            string MerchantStaffRole = app.UserRole;
            BindingContext = viewModel = new ItemDetailViewModel(InvoiceDetails);
            CustomerInfo = viewModel.CustomerInfo;
            AddressInfo = viewModel.AddressInfo;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
         
        }

        private async void UpdateBtn_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                string action ="";
            
                if(!string.IsNullOrEmpty(viewModel.OrderStatus))
                {
                    if ( Equals(viewModel.OrderStatus.ToLower(), New))
                    {
                        action = await DisplayActionSheet("Update Order Status", "Cancel", null,"Accepted", "Rejected");
                        Console.WriteLine("Action: " + action);
                    }
                    else if (Equals(viewModel.OrderStatus.ToLower(), Accepted))
                    {
                    action = await DisplayActionSheet("Update Order Status", "Cancel", null,"Packed","On The Way", "Delivered", "Completed","Cancelled");
                    Console.WriteLine("Action: " + action);
                    }
                    else if (Equals(viewModel.OrderStatus.ToLower(), Packed))
                    {
                        action = await DisplayActionSheet("Update Order Status", "Cancel", null, "On The Way", "Delivered", "Completed", "Cancelled");
                        Console.WriteLine("Action: " + action);
                    }
                    else if (Equals(viewModel.OrderStatus.ToLower(), OnTheWay))
                    {
                        action = await DisplayActionSheet("Update Order Status", "Cancel", null, "Delivered", "Completed", "Cancelled");
                        Console.WriteLine("Action: " + action);
                    }
                    else if (Equals(viewModel.OrderStatus.ToLower(), InProgress))
                    {
                        action = await DisplayActionSheet("Update Order Status", "Cancel", null, "On The Way", "Delivered", "Completed", "Cancelled");
                        Console.WriteLine("Action: " + action);
                    }
                    if (action != "Cancel" && action != null && action != "")
                    {
                        UpdateOderStatustoDataBase(action);
                    }
                }
            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }

        }

        private async void UpdateOderStatustoDataBase(string Status)
        {
            if (CrossConnectivity.Current.IsConnected)
            {


            bool answer = await DisplayAlert("Confirmation", "Click \"OK\" to change order status to \"" + Status + "\"", "ok", "cancel");
            if (answer)
            {
                Console.WriteLine(@"Invoking the rest updateStatus APi ");
                var update_status = await RestService.Instance.UpdateStatus(viewModel.BillNumber, Status, viewModel.OrderSource);
                if (update_status == "Success")
                {
                    UpdateBtn.IsEnabled = false;
                    await DisplayAlert("Status", "Updated Successfully", "ok");
                    var app = Application.Current as App;
                    Analytics.TrackEvent("Update Status Button clicked", new Dictionary<string, string> {
                            { "UserPhoneNumber", app.UserPhoneNumber },
                            { "Status changed to ", Status },
                            { "Bill Number ", viewModel.BillNumber}

                            });
                }
                else
                {
                    await DisplayAlert("Warning", "There was an error while updating the Order Status.Please try after sometime", "ok");
                }
                Console.WriteLine("update status :", update_status);
            }

            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }
        }

        async void OnPhoneTapped(object sender, EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {


            try
            {
                if (viewModel.PhoneNumberList != null)
                {
                    string action = await DisplayActionSheet("Select Number:", "Cancel", null, viewModel.PhoneNumberList.ToArray());
                    if (!string.IsNullOrEmpty(action))
                    {
                        if (action.ToLower() != "cancel")
                            Call(action);
                            
                    }
                    return;
                }
                await DisplayAlert("Error", "No Customer Number Available to Call", "ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somthing went wrong while opening the Dialer", "ok");
                throw ex;
            }


            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }
        }


        public void Call(string number)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

            try
            {
                PhoneDialer.Open(number);
                var app = Application.Current as App;
                Analytics.TrackEvent("Caller Icon clicked", new Dictionary<string, string> {
                            { "UserPhoneNumber", app.UserPhoneNumber },
                            { "Bill Number ", viewModel.BillNumber}
                            });
            }
            catch (FeatureNotSupportedException ex)
            {
                //txtNum.Text = "Phone Dialer is not supported on this device." + ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }
        }

        async void OnMapTapped(object sender, EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

            try
            {
                if (!(string.IsNullOrEmpty(AddressInfo.Longitude) || string.IsNullOrEmpty(AddressInfo.Latitude)))
                {
                    double longitudeval = Double.Parse(AddressInfo.Longitude);
                    double latitudeval = Double.Parse(AddressInfo.Latitude);
                    var location = new Location(latitudeval, longitudeval);
                    await Map.OpenAsync(location);
                    var app = Application.Current as App;
                    Analytics.TrackEvent("Map Icon clicked", new Dictionary<string, string> {
                            { "UserPhoneNumber", app.UserPhoneNumber },
                             { "Bill Number ", viewModel.BillNumber}
                            });
                }
                else
                { 
                    await DisplayAlert("Error", "No address to display on Maps", "ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Somthing went wrong while opening Maps", "ok");
            }


            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }
        }

        [Obsolete]
        public async void OnWhatsappTapped(object sender, EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                string action;
                try
                {
                    if (viewModel.PhoneNumberList != null)
                    {
                        action = await DisplayActionSheet("Select Number:", "Cancel", null, viewModel.PhoneNumberList.ToArray());
                        if (!string.IsNullOrEmpty(action))
                        {
                            if (action.ToLower() != "cancel")
                                Device.OpenUri(new Uri("https://api.whatsapp.com/send?phone=+91" + action));
                            var app = Application.Current as App;
                            Analytics.TrackEvent("WhatsApp Icon clicked", new Dictionary<string, string> {
                            { "UserPhoneNumber", app.UserPhoneNumber },
                            { "Bill Number ", viewModel.BillNumber}
                            });
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "No Customer Number Available", "ok");
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("Error", "Can not open WhatsApp", "ok");
                }

            }
            else
            {
                DependencyService.Get<IToastMessage>().ShortTime("Check your Internet Connection and try again");

            }
        }
}

}