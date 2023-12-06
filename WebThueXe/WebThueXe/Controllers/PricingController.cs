using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThueXe.Models;
namespace WebThueXe.Controllers
{
	public class PricingController : Controller
	{
		// GET: Pricing
		ThueXeEntities db = new ThueXeEntities();

		public ActionResult Index()
		{
			var items = db.types.ToList();
			return View(items);
		}
	}

}