using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceV2.Models
{
    public class ProductModel
    {
        // properties
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductInfo { get; set; }
        public string CategoryName { get; set; }
        public int UserId { get; set; }
        public string Language { get; set; }
        public int StockQuantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
