using Newtonsoft.Json;
using PyDelivery.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PyDelivery.Services
{
    class OrderApiService  : BaseService
    {


        private static OrderApiService instance;
        public static OrderApiService Instance => instance ?? (instance = new OrderApiService());
        public OrderApiData ResponseData { get; set; }
        public async Task<LineItemApiData> FetchLineItemsUsingApiAsync(string InvoiceId)
        {
            try
            {
                var app = Application.Current as App;
                string AccessKey = app.UserKey;
                string PhoneNumber = app.UserPhoneNumber;
                Dictionary<string, dynamic> payload = new Dictionary<string, dynamic>();
                payload.Add("access_key", AccessKey);
                payload.Add("phone_number", PhoneNumber);
                payload.Add("invoice_id", InvoiceId);
                LineItemApiResponse ApiResponseObject = await this.Post<LineItemApiResponse>(this.getAuthUrl("invoiceLineItems"), payload, null);

                Console.WriteLine("Response Status is : " + ApiResponseObject.Status);
                if (ApiResponseObject.Status.ToUpper() != "SUCCESS")
                {
                    System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received non-success " + ApiResponseObject.Status);
                    System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received Response: " + ApiResponseObject.Data);
                    return null;
                }
                return ApiResponseObject.Data;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return null;
            }
        }
        public async Task<OrderApiData> FetchOrderUsingApiAsync(int FrameSize, int PageNumber, string menuPage)
        {
            var app = Application.Current as App;
            string DeliveryBoyId= app.UserId;
            string AccessKey = app.UserKey;
            string PhoneNumber = app.UserPhoneNumber;
            Dictionary<string, dynamic> payload = new Dictionary<string, dynamic>();
            payload.Add("access_key", AccessKey);
            payload.Add("phone_number", PhoneNumber);
            payload.Add("page_number", PageNumber);
            payload.Add("page_size", FrameSize);
            payload.Add("menu_page", menuPage);
            OrderApiResponse ApiResponseObject = await this.Post<OrderApiResponse>(this.getMerchantUrl("Deliver_OrderDetails"), payload, null);
            
            Console.WriteLine("Response Status is : " + ApiResponseObject.Status);
            if (ApiResponseObject.Status.ToUpper() != "SUCCESS")
            {
                System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received non-success " + ApiResponseObject.Status);
                System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received Response: " + ApiResponseObject.Data);
                return null;
            }
            ResponseData = ApiResponseObject.Data;
            return ResponseData;
        }

        public async Task<OrderApiData> FetchOrderList()
        {
            OrderApiData ResponseDataObject = ResponseData;
            //if (ResponseDataObject != null)
                return ResponseDataObject;
            //else
                //return await FetchOrderUsingApiAsync();
        }

        public async Task<OrderApiData> RefreshOrderList(int FrameSize, int PageNumber, string menuPage)
        {
            return await FetchOrderUsingApiAsync(FrameSize, PageNumber, menuPage);
        }

    }
}
