using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyDelivery.Models
{
    class AddressApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public AddressApiData Data { get; set; }
    }

    public partial class AddressApiData
    {
        [JsonProperty("CustAddress")]
        public AddressDetails CustAddress { get; set; }
    }

    public partial class AddressDetails
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("StoreCustomerId")]
        public string StoreCustomerId { get; set; }

        [JsonProperty("MerchantBranchId")]
        public string MerchantBranchId { get; set; }

        [JsonProperty("Address1")]
        public string Address1 { get; set; }

        [JsonProperty("Address2")]
        public string Address2 { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("TagName")]
        public string TagName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("PrimaryPhone")]
        public string PrimaryPhone { get; set; }

        [JsonProperty("Society/BuildingNo")]
        public string SocietyBuildingNo { get; set; }

        [JsonProperty("FlatNo/DoorNo")]
        public string FlatNoDoorNo { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Area")]
        public string Area { get; set; }

        [JsonProperty("PostalCode/ZipCode")]
        public string PostalCodeZipCode { get; set; }


        [JsonProperty("SecondaryPhone")]
        public string AlternatePhone { get; set; }
    }
}
