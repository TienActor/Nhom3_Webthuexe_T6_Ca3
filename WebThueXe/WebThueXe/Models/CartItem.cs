using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThueXe.Models
{
    public class CartItem
    {
        public string image { get; set; }
        public int id_cars { get; set; }
        public string name { get; set; }
        public int SoLuong { get; set; }

        public int price { get; set; }
        public int TongTien
        {
            get
            {
                return SoLuong * price;
            }
        }

    }

}