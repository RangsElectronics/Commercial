using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Commercial.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }
        
        public int MasterId { get; set; }
        [DisplayName("Product")]
        public int? ProductId { get; set; }
        [DisplayName("HS Code")]
        public string HSCode { get; set; }
        [DisplayName("Quantity")]
        public Nullable<int> Qty { get; set; }
        [Range(0,999999999)]
        public Nullable<decimal> KG { get; set; }
        [DisplayName("Unit Price")]
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Total { get; set; }

        public string ProductName { get; set; }
        public IList<tblProduct> ProductList { get; set; }

        public DetailViewModel()
        {
            ProductList = new List<tblProduct>();
        }
    }
}