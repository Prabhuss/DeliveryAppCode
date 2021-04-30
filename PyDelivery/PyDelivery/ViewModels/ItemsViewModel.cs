using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using PyDelivery.Models;
using PyDelivery.Services;
using Color = Xamarin.Forms.Color;
using Plugin.LatestVersion;
using Plugin.Connectivity;
using PyDelivery.Controls;
using Xamarin.Forms.Extended;
using System.Collections.Generic;

namespace PyDelivery.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public OrderApiData OrderData { get; set; }
        public CustomerDetails CustomerInfo { get; set; }
        public AddressDetails AddressInfo { get; set; }
        public InfiniteScrollCollection<CustomerInvoiceDatum> OrderList { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command RefreshBtnCommand { get; set; }
        public Color OrderStatusColor { get; set; }
        public string _listFooterText;
        public string ListFooterText
        {
            get
            {
                if (_listFooterText == null)
                    return "Loading...";
                else
                    return _listFooterText;}
            set
            {
                if (_listFooterText != value)
                {
                    _listFooterText = value;
                    OnPropertyChanged();
                }
            }
        }

        public int pageNumber;

        public ItemsViewModel(string menuPage)
        {
            OrderData = new OrderApiData();
            OrderList = new InfiniteScrollCollection<CustomerInvoiceDatum>()
            {
                OnLoadMore = async () =>
                {
                    // load the next page 
                    var items = await ExecuteLoadItemsCommand( menuPage);
                    //Iswaiting = false;
                    return null; 
                }
            };
            LoadItemsCommand = new Command(async () => await FirstExecuteLoadItemsCommand(menuPage));
            RefreshBtnCommand = new Command( () => refreshBtm());
            pageNumber = 1;
        }
        public async Task FirstExecuteLoadItemsCommand(string menuPage)
        {
            OrderList.Clear();
            await ExecuteLoadItemsCommand(menuPage);
            if (OrderList.Count == 0)
                ListFooterText = "Order list is empty";
        }
        public async Task<ObservableCollection<CustomerInvoiceDatum>> ExecuteLoadItemsCommand(string menuPage)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ObservableCollection<CustomerInvoiceDatum> tempItems = new ObservableCollection<CustomerInvoiceDatum>();
                    if (OrderList.Count == 0)
                        pageNumber = 1;
                    int frameSize = 10;
                    var items = await OrderApiService.Instance.RefreshOrderList((frameSize / 2), pageNumber, menuPage);
                    if (!(items.CustomerInvoiceData.Count == 0 && items.WooCommerceOrders.Count == 0))
                    {
                        ListFooterText = null;
                        pageNumber++;
                        foreach (var item in items.CustomerInvoiceData)
                        {
                            try
                            {
                                CustomerInfo = new CustomerDetails();
                                AddressInfo = new AddressDetails();
                                CustomerApiData CustData = await CustomerApiService.Instance.FetchCustomerList(item.StoreCustomerId);
                                AddressApiData AddressItem = await AddressApiService.Instance.RefreshAddressList(item.DeliverAddressId);
                                try
                                {
                                    CustomerInfo = CustData.CustomerInfo;
                                    item.PrimaryPhone = CustomerInfo.PrimaryPhone;
                                    item.AddressTagName = AddressItem.CustAddress.TagName;
                                    item.FirstName = CustomerInfo.FirstName;
                                }
                                catch { }
                                item.OrderSource = "App";
                                OrderList.Add(item);
                            }
                            catch
                            {
                            }
                        }

                        foreach (var item in items.WooCommerceOrders)
                        {
                            try
                            {
                                item.OrderSource = "Website";
                                OrderList.Add(item);
                            }
                            catch
                            { }
                        }
                    }
                    else
                        ListFooterText = "No more orders to load";
                    return tempItems;
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IToastMessage>().LongTime("Server Error E01: Please try again after sometime.");
                    ListFooterText = "Unable to load the orders";
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                DependencyService.Get<IToastMessage>().LongTime("Check your Internet Connection and try again");
                ListFooterText = "Unable to load the orders";
            }
            return null;
        }

        void refreshBtm()
        {
            IsBusy = true;
        }

       
    }
}