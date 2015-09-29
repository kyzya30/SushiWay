using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class NewDishModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string NameRus { get; set; }
        public string NameUkr { get; set; }
        public string NumberOfOrders { get; set; }
        public string Energy { get; set; }
        public string Price { get; set; }
        public int? Priority { get; set; }
        public bool Sale { get; set; }
        public bool IsHided { get; set; }
        public string IngridientsRus { get; set; }
        public string IngridientsUkr { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<ProductWeightDetail> productWeightDetails { get; set; }
    }
}
