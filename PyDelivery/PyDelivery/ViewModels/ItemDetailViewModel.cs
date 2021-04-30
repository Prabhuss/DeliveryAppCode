using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PyDelivery.Models;
using PyDelivery.Services;
using System.Drawing;
using Color = Xamarin.Forms.Color;
using Plugin.Connectivity;
using PyDelivery.Controls;

namespace PyDelivery.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public CustomerDetails CustomerInfo { get; set; }
        public AddressDetails AddressInfo { get; set; }
        public string BillNumber { get; set; }
        public string OrderSource { get; set; }
        public string OrderStatus { get; set; }
        public int TotalItems { get; set; }
        public int ProdListHeight { get; set; }
        public string PayableAmount { get; set; }
        public string OrderDate { get; set; }
        public bool DisplayAltNumber { get; set; }
        public bool DisplayFlatNumber { get; set; }
        public bool DisplaySociety { get; set; }

        

        // Public Commands
        public Command LoadItemsCommand { get; set; }
        public Command OnActionSheetCancelDeleteClicked { get; set; }
        //UI Variables
        public Color OrderStatusColor { get; set; }
        public bool UpdateBtnStatus { get; set; }

        public ObservableCollection<string> phoneNumberList;
        public ObservableCollection<string> PhoneNumberList
        {
            get { return phoneNumberList; }
            set
            {
                if (phoneNumberList != value)
                {
                    phoneNumberList = value;
                    OnPropertyChanged();
                }
            }
        }
        //LineItems List
        public ObservableCollection<InvocieLineItem> LineItemList { get; set; } = new ObservableCollection<InvocieLineItem>();


        //Constructor
        public ItemDetailViewModel(CustomerInvoiceDatum InvoiceDetails)
        {
            if (CrossConnectivity.Current.IsConnected)
            {

            //CustomerInfo.FirstName = "yoyo honey singh";
            //CustomerInfo.PrimaryPhone = "998898989";
            phoneNumberList = new ObservableCollection<string>();
            var storecust_id = string.IsNullOrEmpty(InvoiceDetails.StoreCustomerId) ? "" : InvoiceDetails.StoreCustomerId;
            if (InvoiceDetails.CreatedDate != null)
            {
                var date = InvoiceDetails.CreatedDate;
                    var convertedDate = date.ToString("MMM dd, yyyy hh:mm tt");
                //var convertedDate = date.ToLocalTime().ToString("MMM dd, yyyy hh:mm tt");
                Console.WriteLine("Converted date :", convertedDate);
                OrderDate = convertedDate;
            }
            else
            {
                OrderDate = "";
            }

            BillNumber = string.IsNullOrEmpty(InvoiceDetails.InvoiceId) ? "" : InvoiceDetails.InvoiceId;
            OrderSource = InvoiceDetails.OrderSource;
            PayableAmount = string.IsNullOrEmpty(InvoiceDetails.PayableAmount) ? "" : InvoiceDetails.PayableAmount.ToString();
            OrderStatus = string.IsNullOrEmpty(InvoiceDetails.OrderStatus) ? "" : InvoiceDetails.OrderStatus;
            UpdateBtnStatus = false;
            SetOrderStatusFrameColor(OrderStatus);
            Task.Run(async () => await GetLineItemsFromCloudAsync(InvoiceDetails.InvoiceId)).Wait();
            Task.Run(async () => { await ExecuteLoadCustomerCommand(storecust_id, InvoiceDetails.DeliverAddressId, InvoiceDetails); }).Wait();
            int i = 10;
                //ResetBtnStatus(InvoiceDetails.OrderStatus);
            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");

            }
        }

        public void SetOrderStatusFrameColor(string OrderStatus)
        {
            OrderStatusColor = new Color();
            OrderStatusColor = Color.CadetBlue;
            UpdateBtnStatus = true;

            if (OrderStatus.ToLower() == "delivered")
            {
                OrderStatusColor = Color.Green;
            }
            else if (OrderStatus.ToLower() == "cancelled")
            {
                OrderStatusColor = Color.DarkRed;
                UpdateBtnStatus = false;
            }
            else if (OrderStatus.ToLower() == "completed")
            {
                OrderStatusColor = Color.Green;
                UpdateBtnStatus = false;
            }
            else if (OrderStatus.ToLower() == "rejected")
            {
                OrderStatusColor = Color.DarkRed;
                UpdateBtnStatus = false;
            }
        }
        public async Task GetLineItemsFromCloudAsync(string InvoiceId)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                LineItemApiData items = await OrderApiService.Instance.FetchLineItemsUsingApiAsync(InvoiceId);
                try
                {
                    if(items != null)
                    {
                        LineItemList.Clear();
                        int itemCount = 0;
                        foreach (var lineitem in items.InvocieLineItems)
                        {
                            LineItemList.Add(lineitem);
                            itemCount++;
                        }
                        TotalItems = itemCount;
                        ProdListHeight = TotalItems * 112 +10;
                        Console.WriteLine("Total items :" + TotalItems);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Exception raised while retrieving product's details : " + e.Message);
                    try
                    {
                        DependencyService.Get<IToastMessage>().LongTime("Exception raised while retrieving product's details : " + e.Message);
                    }
                    catch { }
                }
            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");
            }
        }

        public async Task ExecuteLoadCustomerCommand(string storecust_id,string DeliverAddressId, CustomerInvoiceDatum InvoiceDetails)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IsBusy = true;
                try
                {
                    if (!string.IsNullOrEmpty(storecust_id))
                    {
                        CustomerInfo = new CustomerDetails();
                        AddressInfo = new AddressDetails();
                        CustomerApiData CustomerItems = await CustomerApiService.Instance.FetchCustomerList(storecust_id);
                        AddressApiData AddressItem = await AddressApiService.Instance.RefreshAddressList(DeliverAddressId);
                        CustomerInfo = CustomerItems.CustomerInfo;
                        AddressInfo = AddressItem.CustAddress;
                        phoneNumberList.Add(AddressInfo.PrimaryPhone);
                        phoneNumberList.Add(AddressInfo.AlternatePhone);
                        if (String.IsNullOrEmpty(AddressInfo.AlternatePhone))
                        {
                            DisplayAltNumber = false;
                        }
                        else
                        {
                            DisplayAltNumber = true;
                        }
                        if (String.IsNullOrEmpty(AddressInfo.FlatNoDoorNo))
                        {
                            DisplayFlatNumber = false;
                        }
                        else
                        {
                            DisplayFlatNumber = true;
                        }
                        if (String.IsNullOrEmpty(AddressInfo.SocietyBuildingNo))
                        {
                            DisplaySociety = false;
                        }
                        else
                        {
                            DisplaySociety = true;
                        }
                    }
                    else
                    {
                        AddressInfo = new AddressDetails();
                        AddressInfo.FirstName = InvoiceDetails.FirstName;
                        AddressInfo.PrimaryPhone = InvoiceDetails.PrimaryPhone;
                        AddressInfo.AlternatePhone = null;
                        DisplayAltNumber = false;
                        DisplayFlatNumber = false;
                        AddressInfo.Address1 = InvoiceDetails.Address1;
                        AddressInfo.PostalCodeZipCode = InvoiceDetails.ZipCode;
                        AddressInfo.Address2 = InvoiceDetails.Address2;
                        AddressInfo.TagName = null;
                        phoneNumberList.Add(AddressInfo.PrimaryPhone);
                        phoneNumberList.Add(AddressInfo.AlternatePhone);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");

            }

        }
    }
}
