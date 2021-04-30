using Newtonsoft.Json;
using PyDelivery.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PyDelivery.Services
{
    class CustomerApiService
    {
        
        private static CustomerApiService instance;
        public static CustomerApiService Instance => instance ?? (instance = new CustomerApiService());
        public CustomerApiData ResponseData { get; set; }
        public async Task<CustomerApiData> FetchCustomerList(string storecust_id)
        {
            var app = Application.Current as App;
            string AccessKey = app.UserKey;
            string PhoneNumber = app.UserPhoneNumber;
            string baseUrl = "https://getpymobileapp.azurewebsites.net/api/v2/merchant/";
            string url = string.Format("{0}{1}", baseUrl, "StoreCustomer_Details");
            using (HttpClient Webclient = new HttpClient())
            {
                var uri = new Uri(url);
                Dictionary<string, dynamic> payload = new Dictionary<string, dynamic>();
                payload.Add("storecustomer_id", storecust_id);
                payload.Add("phone_number", PhoneNumber);
                payload.Add("access_key", AccessKey);

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Webclient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var rcontent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        CustomerApiResponse customData = JsonConvert.DeserializeObject<CustomerApiResponse>(rcontent);
                        Console.WriteLine("Response of custdetails Status is : " + customData.Status);
                        if (customData.Status.ToLower() != "success")
                        {
                            System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received non-success " + customData.Status);
                            System.Diagnostics.Debug.WriteLine("[BuildRequestNDisplay] Received Response: " + content);
                            return null;
                        }
                        ResponseData = customData.Data;
                        return ResponseData;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Error in customer API " + e.Message);
                    }
                }
                return null;
            }
        }
    }
}
