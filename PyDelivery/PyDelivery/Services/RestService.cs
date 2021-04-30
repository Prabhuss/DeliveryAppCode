using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PyDelivery.Controls;
using PyDelivery.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PyDelivery.Services
{
    public interface IOTPRestService
    {
        /*        Task<MerchantApiResponse> GetPermission(string merchant_id, string phone_number);*/

   
        Task<MerchantApiResponse> GenerateOtp(string phone_number, string reg_token);


        Task<string> VerifyOtp(string phone_number, string otp);

        Task<string> UpdateStatus(string invoice_id, string order_status, string order_source);


    }


    public class RestService : BaseService, IOTPRestService 
    {
        #region fields
        private static RestService instance;
        MerchantApiData ResponseData { get; set; }
        HttpClient _client;

       /* string baseUrl = "https://getpymobileapp.azurewebsites.net/api/v1/auth/";*/
        string baseUrl = "https://getpymobileapp.azurewebsites.net/api/v2/auth/";
        string merchantUrl = "https://getpymobileapp.azurewebsites.net/api/v2/merchant/";

        #endregion

        #region Properties
        /// <summary>
        /// Gets an instance of the <see cref="PhotosDataService"/>.
        /// </summary>
        public static RestService Instance => instance ?? (instance = new RestService());



        #endregion

        public RestService()
        {
            _client = new HttpClient();
        }


        public MerchantApiResponse RegToken { get; set; }

        public async Task<MerchantApiResponse> GenerateOtp(string phone_number, string reg_token)
        {
            string hashKey = System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
            string url = string.Format("{0}{1}", baseUrl, "generateOtp_deliveryboy");
            var uri = new Uri(url);
            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("phone_number", phone_number);
            payload.Add("reg_token", reg_token);
            payload.Add("hash_key", hashKey);
            var json = JsonConvert.SerializeObject(payload);
            Console.WriteLine("GenerateOtp", @"\t GenerateOtp Payload is " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            var rcontent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("GenerateOtp", @"\tGenerate OTP response is " + rcontent);
            MerchantApiResponse robject = JsonConvert.DeserializeObject<MerchantApiResponse>(rcontent);
            Console.WriteLine(robject.Status);
            RegToken = robject;
            return RegToken;
        }

        public async Task<string> VerifyOtp(string phone_number, string otp)
        {
            string url = string.Format("{0}{1}", baseUrl, "otpValidateForDeliveryboy");
            var uri = new Uri(url);
            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("phone_number", phone_number);
            payload.Add("otp", otp);
            var json = JsonConvert.SerializeObject(payload);
            Console.WriteLine("ValidateOtp", @"\t GenerateOtp Payload is " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            var rcontent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("ValidateOtp", @"\tValidate OTP response is " + rcontent);
            Dictionary<string, string> robject = JsonConvert.DeserializeObject<Dictionary<string, string>>(rcontent);
            Console.WriteLine(robject["status"]);
            return robject["status"];

        }

        public async Task<string> UpdateStatus(string invoice_id, string order_status, string order_source)
        {
            string url = string.Format("{0}{1}", merchantUrl, "updateOrderStatus");
            var uri = new Uri(url);
            var app = Application.Current as App;
            string AccessKey = app.UserKey;
            string PhoneNumber = app.UserPhoneNumber;
            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("invoice_id", invoice_id);
            payload.Add("order_status", order_status);
            payload.Add("access_key", AccessKey);
            payload.Add("phone_number", PhoneNumber);
            payload.Add("order_source", order_source);
            var json = JsonConvert.SerializeObject(payload);
            Console.WriteLine("updateStatus", @"\t updateStatus Payload is " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            var rcontent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("updateStatus", @"\t updateStatus response is " + rcontent);
            Dictionary<string, string> robject = JsonConvert.DeserializeObject<Dictionary<string, string>>(rcontent);
            Console.WriteLine(robject["status"]);
            if (robject["status"] == "SUCCESS")
            {
                var update_status = "Success";
                return update_status;
            }
            else
            {
                var update_status = "Failure";
                return update_status;
            }

        }

        
    }
}
