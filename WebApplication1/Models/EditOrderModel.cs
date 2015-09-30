using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EditOrderModel
    {
        public int? OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Room { get; set; }

    }
}