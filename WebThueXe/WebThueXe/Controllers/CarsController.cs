using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebThueXe.Models;

namespace WebThueXe.Controllers
{
    public class CarsController : Controller
    {
        ThueXeEntities db = new ThueXeEntities();
        // GET: Cars
        public ActionResult Index(string Name, int Type = 0)
        {
            var typees = from i in db.types select i;
            ViewBag.Type = new SelectList(typees, "id_type", "type1");
            var names = db.cars.Include(j => j.type);
            if (!string.IsNullOrEmpty(Name))
            {
                names = names.Where(i => i.name.Contains(Name));
            }
            if (Type != 0)
            {
                names = names.Where(j => j.id_type == Type);
            }
            return View(names.ToList());
        }
        public ActionResult CarList_Home()
        {
            var items = db.cars.Include(j => j.type);
            items = items.Where(i => (bool)i.IsActive).Where(j => (bool)j.Hot);
            return PartialView("CarsList", items.ToList());
        }
        public ActionResult Details(int? idx)
        {
            if (idx == null) return HttpNotFound();
            var currentCar = db.cars.Find(idx);

            if (currentCar == null) return HttpNotFound();

            // Lấy danh sách xe liên quan (cùng loại nhưng không phải là xe hiện tại)
            var relatedCars = db.cars.Where(c => c.id_type == currentCar.id_type && c.id_cars != currentCar.id_cars).ToList();

            ViewBag.RelatedProducts = relatedCars;

            return View("Detail", currentCar);

        }
    }
}