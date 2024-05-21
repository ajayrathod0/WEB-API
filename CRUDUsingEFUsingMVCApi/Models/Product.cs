using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDUsingEFUsingMVCApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> AvailableQuantity { get; set; }
        public Nullable<int> CategoryId { get; set; }
    }
}