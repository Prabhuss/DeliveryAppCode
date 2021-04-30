using PyDelivery.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PyDelivery.Services
{
    class AddressApiService : BaseService
    {

        private static AddressApiService instance;
        public static AddressApiService Instance => instance ?? (instance = new AddressApiService());
        public AddressApiData ResponseData { get; set; }
        public async Task<AddressApiData> FetchAddressUsingApiAsync(string DeliverAddressId)
        {
            var app = Application.Current as App;
            string DeliveryBoyId = app.UserId;
            string AccessKey = app.UserKey;
            string PhoneNumber = app.UserPhoneNumber;
            Dictionary<string, dynamic> payload = new Dictionary<string, dynamic>();
            payload.Add("phone_number", PhoneNumber);
            payload.Add("access_key", AccessKey);
            payload.Add("deliveraddress_id", DeliverAddressId);
            AddressApiResponse ApiResponseObject = await this.Post<AddressApiResponse>(this.getAuthUrl("getDeliverAddress"), payload, null);
            
            Console.WriteLine("Response Status is : " + ApiResponseObject.Status);
            if (ApiResponseObject.Status.ToLower() != "success")
            {
                System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received non-success " + ApiResponseObject.Status);
                System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received Response: " + ApiResponseObject.Data);
                return null;
            }
            ResponseData = ApiResponseObject.Data;
            return ResponseData;
        }

        public async Task<AddressApiData> RefreshAddressList(string addressId)
        {
            return await FetchAddressUsingApiAsync( addressId);
        }

    }
}
