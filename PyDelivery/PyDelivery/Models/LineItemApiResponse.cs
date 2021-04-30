using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PyDelivery.Models
{
    class LineItemApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public LineItemApiData Data { get; set; }
    }

    public partial class LineItemApiData
    {
        [JsonProperty("invocieLineItems")]
        public List<InvocieLineItem> InvocieLineItems { get; set; }
    }
    public partial class InvocieLineItem
    {
        public static string product_url { get; } = "https://getpytaskintg.blob.core.windows.net/productpics"; //product image

        [JsonProperty("CustomerInvoiceLineItemId")]
        public string CustomerInvoiceLineItemId { get; set; }

        [JsonProperty("CustomerInvoiceId")]
        public long CustomerInvoiceId { get; set; }

        [JsonProperty("ProductImage")]
        public string ProductImage { get; set; }

        [JsonProperty("MerchantBranchId")]
        public long MerchantBranchId { get; set; }

        [JsonProperty("ProductId")]
        public string ProductId { get; set; }

        [JsonProperty("ProductName")]
        public object ProductName { get; set; }

        [JsonProperty("UnitPrice")]
        public object UnitPrice { get; set; }

        [JsonProperty("Quantity")]
        public string Quantity { get; set; }

        [JsonProperty("Discount")]
        public object Discount { get; set; }

        [JsonProperty("UnitPriceAfterDiscount")]
        public object UnitPriceAfterDiscount { get; set; }

        [JsonProperty("TotalPrice")]
        public object TotalPrice { get; set; }

        [JsonProperty("CouponCode")]
        public object CouponCode { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("CreatedBy")]
        public object CreatedBy { get; set; }

        [JsonProperty("ProductName2")]
        public object ProductName2 { get; set; }

        [JsonProperty("category")]
        public object category { get; set; }



        [JsonProperty("ImageLinkFlag")]
        private string ImageLinkFlag;


        [JsonProperty("previewimage")]
        public string PreviewImage
        {
            get
            {
                if (string.IsNullOrEmpty(ImageLinkFlag))
                {
                    return product_url + this.ProductImage;
                }
                if (ImageLinkFlag.ToLower() == "f")
                {
                    return this.ProductImage;
                }
                else
                {
                    return product_url + this.ProductImage;
                }
            }
            set { this.ProductImage = value; }

        }
    }
}
