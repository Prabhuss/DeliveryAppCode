using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyDelivery.Models
{
    class VersionApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public VersionApiData Data { get; set; }

    }
        public partial class VersionApiData
        {
            [JsonProperty("AppInfo")]
            public VersionAppInfo AppInfo { get; set; }
        }

        public partial class VersionAppInfo
        {
            [JsonProperty("ID")]
            public long Id { get; set; }

            [JsonProperty("MerchantBranchId")]
            public long MerchantBranchId { get; set; }

        [JsonProperty("AppName")]
        public string AppName { get; set; }

        [JsonProperty("AppVersion")]
        public long AppVersion { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("MessageForUpdateVersion")]
        public string MessageForUpdateVersion { get; set; }

        [JsonProperty("PlayStoreURL")]
        public string PlayStoreURL { get; set; }

        [JsonProperty("UpdateType")]
        public string UpdateType { get; set; }
    }
    
}
