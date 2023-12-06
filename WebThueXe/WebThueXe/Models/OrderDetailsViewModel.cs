using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThueXe.Models
{
    public class OrderDetailsViewModel
    {
        public rent Rent { get; set; }
        public IEnumerable<rentDetail> RentDetails { get; set; }
        public string RentalStatus { get; set; }
    }
}