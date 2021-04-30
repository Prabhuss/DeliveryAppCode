using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyDelivery.Models
{
    public partial class MerchantApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("data")]
        public MerchantApiData Data { get; set; }
    }

    public partial class MerchantApiData
    {
        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("MerchantBranchId")]
        public string MerchantBranchId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("CreatePermission")]
        public string CreatePermission { get; set; }

        [JsonProperty("DeletePermission")]
        public string DeletePermission { get; set; }

        [JsonProperty("CreatedDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("ModifiedDate")]
        public string ModifiedDate { get; set; }

        [JsonProperty("IsActive")]
        public string IsActive { get; set; }

        [JsonProperty("Role")]
        public string Role { get; set; }
    }

}
