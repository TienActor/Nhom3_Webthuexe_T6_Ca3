using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;

using WebThueXe.Models;
using System.Data.Entity.Migrations;


namespace WebThueXe.Areas.Admin.Controllers
{
    public class XeController : Controller
    {
        private ThueXeEntities db = new ThueXeEntities();
        // GET: Admin/cars


        // GET: Admin/bikes
        public ActionResult Index()
        {
            var bikes = db.cars.Include(b => b.type);
            return View(bikes.ToList());
        }

        // GET: Admin/bikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car bike = db.cars.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // GET: Admin/bikes/Create
        public ActionResult Create()
        {
            ViewBag.id_type = new SelectList(db.types, "id_type", "type1");
            return View();
        }

        // POST: Admin/bikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id_car,name,price,IsActive,id_type,id_seat,Hot,describe,consume,status")] car bike, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                string _fn = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("/Content/images/xe/"), _fn);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    image.SaveAs(path);
                }
                else
                {
                    image.SaveAs(path);
                }
               bike.image = "/Content/images/xe/" + _fn;
            }
            if (ModelState.IsValid)
            {
                db.cars.Add(bike);
                db.SaveChanges();
                //ThongBao("Thêm thành công!!!", "success");
                return RedirectToAction("Index");
            }

            ViewBag.id_type = new SelectList(db.types, "id_type", "type1", bike.id_type);
            return View(bike);
        }

        // GET: Admin/bikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car bike = db.cars.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_type = new SelectList(db.types, "id_type", "type1", bike.id_type);
            return View(bike);
        }

        // POST: Admin/bikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id_cars,name,price,IsActive,id_type,IsHot,describe,mass,volumn,size,consume,status")] car bike, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                string _fn = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("/Content/images/xe/"), _fn);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                //bike.image = "/Content/images/xe/" + _fn;
                image.SaveAs(path);
                bike.image = "/Content/images/xe/" + _fn; // Sửa đường dẫn ảnh
            }
            else if (image == null)
            {
                car bikes = db.cars.Where(i => i.id_cars == bike.id_cars).FirstOrDefault();
                bike.image = bikes.image;
            }
            if (ModelState.IsValid)
            {
                db.Set<car>().AddOrUpdate(bike);
                db.SaveChanges();
                //ThongBao("Sửa thành công!!!", "success");
                return RedirectToAction("Index");
            }
            ViewBag.id_type = new SelectList(db.types, "id_type", "type1", bike.id_type);
            return View(bike);
        }

        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car bike = db.cars.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            db.cars.Remove(bike);
            db.SaveChanges();
            //ThongBao("Xoá thành công!!!", "success");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}