using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThueXe.Models;

namespace WebThueXe.Controllers
{
    public class HomeController : Controller
    {
        ThueXeEntities db = new ThueXeEntities();
        public ActionResult Index()
        {
            return View();
        }

       
    }
}