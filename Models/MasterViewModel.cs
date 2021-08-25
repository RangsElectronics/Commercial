using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Commercial.Models
{
    public class MasterViewModel
    {
        public int Id { get; set; }
        [DisplayName("Bank")]
        public int BankId { get; set; }
        [Required]
        [DisplayName("Supplier")]
        public int SupplierId { get; set; }
        [DisplayName("LC No")]
        public string LCNo { get; set; }
        [DisplayName("LC Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LCDate { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [DisplayName("IRC No")]
        public string IRCNo { get; set; }
        [DisplayName("PIN No")]
        public string PINo { get; set; }
        [DisplayName("PI Value")]
        public string PIValue { get; set; }
        [DisplayName("LC Amount")]
        public Nullable<decimal> LCAmount { get; set; }
        [DisplayName("TT Amount")]
        public Nullable<decimal> TTAmount { get; set; }
        [DisplayName("FOB Price")]
        public Nullable<decimal> FOBPrice { get; set; }
        [DisplayName("Total Amount")]
        [Required]
        public decimal TotalAmount { get; set; }
        [DisplayName("Freight Charge")]
        public Nullable<decimal> FreightCharge { get; set; }
        [DisplayName("Container Quantity")]
        public string ContainerQuantity { get; set; }
        [DisplayName("Customs Duty")]
        public Nullable<decimal> CustomsDuty { get; set; }

        public string BankName { get; set; }
        public string SupplierName { get; set; }
        public DetailViewModel DetailModel { get; set; }
        public IList<tblBank> BankList { get; set; }
        public IList<tblSupplier> SupplierList { get; set; }
        public IList<DetailViewModel> DetailList { get; set; }

        public MasterViewModel()
        {
            LCDate = DateTime.Now;
            DetailModel = new DetailViewModel();
            BankList = new List<tblBank>();
            SupplierList = new List<tblSupplier>();
            DetailList = new List<DetailViewModel>();
        }
    }
}