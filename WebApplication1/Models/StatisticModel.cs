using System.Collections.Generic;

namespace WebApplication1.Models
{
    using System;
    public class StatisticModel
    {
        public IEnumerable<ShowUnprocessedOrders_Result> showUnprocessedOrders { get; set; }
        public IEnumerable<MostPopularDishes_Result> mostPopularDishes { get; set; }

    }
}