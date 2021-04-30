using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PyDelivery.Models
{
    class CustomerApiResponse
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public CustomerApiData Data { get; set; }
    }

    public partial class CustomerApiData
    {
        [JsonProperty("CustomerInfo")]
        public CustomerDetails CustomerInfo { get; set; }
    }

    public partial class CustomerDetails
    {
        [JsonProperty("StoreCustomerId")]
        public string StoreCustomerId { get; set; }

        [JsonProperty("MerchantBranchId")]
        public long MerchantBranchId { get; set; }

        [JsonProperty("Source")]
        public object Source { get; set; }

        [JsonProperty("CustomerType")]
        public string CustomerType { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("CompanyName")]
        public object CompanyName { get; set; }

        [JsonProperty("CompanyWebsite")]
        public object CompanyWebsite { get; set; }

        [JsonProperty("PrimaryPhone")]
        public string PrimaryPhone { get; set; }

        [JsonProperty("SecondaryPhone")]
        public object SecondaryPhone { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Address1")]
        public string Address1 { get; set; }

        [JsonProperty("Address2")]
        public string Address2 { get; set; }

        [JsonProperty("Country")]
        public object Country { get; set; }

        [JsonProperty("State")]
        public object State { get; set; }

        [JsonProperty("City")]
        public object City { get; set; }

        [JsonProperty("ZipCode")]
        public object ZipCode { get; set; }

        [JsonProperty("BirthMonth")]
        public object BirthMonth { get; set; }

        [JsonProperty("BirthDay")]
        public object BirthDay { get; set; }

        [JsonProperty("AnnivMonth")]
        public object AnnivMonth { get; set; }

        [JsonProperty("AnnivDay")]
        public object AnnivDay { get; set; }

        [JsonProperty("PosId")]
        public object PosId { get; set; }

        [JsonProperty("BranchId")]
        public object BranchId { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("CreatedBy")]
        public object CreatedBy { get; set; }

        [JsonProperty("ModifiedDate")]
        public object ModifiedDate { get; set; }

        [JsonProperty("ModifiedBy")]
        public object ModifiedBy { get; set; }

        [JsonProperty("ExternalCustomerId")]
        public object ExternalCustomerId { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("GMAP_Address1")]
        public object GmapAddress1 { get; set; }

        [JsonProperty("GMAP_Address2")]
        public object GmapAddress2 { get; set; }

        [JsonProperty("GMAP_Country")]
        public object GmapCountry { get; set; }

        [JsonProperty("GMAP_City")]
        public object GmapCity { get; set; }

        [JsonProperty("GMAP_ZipCode")]
        public object GmapZipCode { get; set; }

        [JsonProperty("OTP")]
        public long Otp { get; set; }

        [JsonProperty("TotalLoyaltyPoints")]
        public object TotalLoyaltyPoints { get; set; }

        [JsonProperty("OTPNum")]
        public object OtpNum { get; set; }

        [JsonProperty("NextDayTotalLoyaltyPoint")]
        public object NextDayTotalLoyaltyPoint { get; set; }

        [JsonProperty("FirstRegistrationDate")]
        public object FirstRegistrationDate { get; set; }

        [JsonProperty("SecondRegistrationDate")]
        public object SecondRegistrationDate { get; set; }

        [JsonProperty("ThirdRegistrationDate")]
        public object ThirdRegistrationDate { get; set; }

        [JsonProperty("Device")]
        public string Device { get; set; }
    }
}
