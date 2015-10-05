using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MostPopularDishesModel
    {
        public IEnumerable<MostPopularDishes_Result> mostPopularDishes { get; set; }
        public IEnumerable<Category> category { get; set; }
        public string selectedCategory { get; set; }
        public int[] arrayTopVal { get; set; }
        public int? selectedTopval { get; set; }
    }
}