//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Commercial
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_MasterDetail
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public int SupplierId { get; set; }
        public string LCNo { get; set; }
        public System.DateTime LCDate { get; set; }
        public string Address { get; set; }
        public string IRCNo { get; set; }
        public string PINo { get; set; }
        public string PIValue { get; set; }
        public Nullable<decimal> LCAmount { get; set; }
        public Nullable<decimal> TTAmount { get; set; }
        public Nullable<decimal> FOBPrice { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> FreightCharge { get; set; }
        public string ContainerQuantity { get; set; }
        public Nullable<decimal> CustomsDuty { get; set; }
        public int DetailId { get; set; }
        public int ProductId { get; set; }
        public string HSCode { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> KG { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string BankName { get; set; }
        public string Name { get; set; }
        public string SupplierName { get; set; }
    }
}
