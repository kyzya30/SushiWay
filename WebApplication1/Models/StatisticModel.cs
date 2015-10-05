using System.Collections.Generic;

namespace WebApplication1.Models
{
    using System;
    public class StatisticModel
    {


        public IEnumerable<ShowUnprocessedOrders_Result> ShowUnprocessedOrders { get; set; }
        public MostPopularDishesModel mostPopularDishesModel { get; set; }

    }
}