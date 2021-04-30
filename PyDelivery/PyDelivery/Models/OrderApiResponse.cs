using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PyDelivery.Models
{
    public class OrderApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public OrderApiData Data { get; set; }
    }

    public partial class OrderApiData
    {
        [JsonProperty("customerInvoiceData")]
        public List<CustomerInvoiceDatum> CustomerInvoiceData { get; set; }

        [JsonProperty("invocieLineItems")]
        public List<InvocieLineItem> InvocieLineItems { get; set; }
        [JsonProperty("woocommerceorders")]
        public List<CustomerInvoiceDatum> WooCommerceOrders { get; set; }
    }

    public partial class CustomerInvoiceDatum
    {
        [JsonProperty("CustomerInvoiceId")]
        public long CustomerInvoiceId { get; set; }

        [JsonProperty("StoreCustomerId")]
        public string StoreCustomerId { get; set; }

        [JsonProperty("MerchantBranchId")]
        public long MerchantBranchId { get; set; }

        [JsonProperty("CustomerVehicleId")]
        public object CustomerVehicleId { get; set; }

        [JsonProperty("PosId")]
        public object PosId { get; set; }

        [JsonProperty("BranchId")]
        public object BranchId { get; set; }

        [JsonProperty("InvoiceId")]
        public string InvoiceId { get; set; }

        [JsonProperty("InvoiceDate")]
        public DateTimeOffset InvoiceDate { get; set; }

        [JsonProperty("LabourAmount")]
        public object LabourAmount { get; set; }

        [JsonProperty("PartsAmount")]
        public object PartsAmount { get; set; }

        [JsonProperty("TotalInvoiceAmount")]
        public string TotalInvoiceAmount { get; set; }

        [JsonProperty("DiscountAmount")]
        public string DiscountAmount { get; set; }

        [JsonProperty("TaxAmount")]
        public string TaxAmount { get; set; }

        [JsonProperty("CouponCode")]
        public string CouponCode { get; set; }

        [JsonProperty("TypeOfRoom")]
        public string TypeOfRoom { get; set; }

        [JsonProperty("TotalDays")]
        public string TotalDays { get; set; }

        [JsonProperty("PDFPath")]
        public object PdfPath { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("CreatedBy")]
        public object CreatedBy { get; set; }

        [JsonProperty("InvoiceType")]
        public string InvoiceType { get; set; }

        [JsonProperty("PayableAmount")]
        public string PayableAmount { get; set; }

        [JsonProperty("ModifiedDate")]
        public DateTimeOffset ModifiedDate { get; set; }

        [JsonProperty("ModifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("EarnedPoints")]
        public string EarnedPoints { get; set; }

        [JsonProperty("RedeemedPoints")]
        public object RedeemedPoints { get; set; }

        [JsonProperty("Deliveryboy_id")]
        public string DeliveryboyId { get; set; }

        [JsonProperty("OrderStatus")]
        public string OrderStatus { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("PrimaryPhone")]
        public string PrimaryPhone { get; set; }

        [JsonProperty("Address1")]
        public string Address1 { get; set; }

        [JsonProperty("DeliverAddressId")]
        public string DeliverAddressId { get; set; }

        [JsonProperty("PaymentMode")]
        public string PaymentMode { get; set; }
        public string AddressTagName { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("Address2")]
        public string Address2 { get; set; }

        [JsonProperty("OrderSource")]
        public string OrderSource { get; set; }
    }

}
