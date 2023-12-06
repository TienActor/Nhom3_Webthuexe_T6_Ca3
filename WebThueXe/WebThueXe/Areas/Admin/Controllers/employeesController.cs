using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebThueXe.Models;

namespace WebThueXe.Areas.Admin.Controllers
{
    public class employeesController : BaseController
    {

        // GET: Admin/employees
        private ThueXeEntities db = new ThueXeEntities();
        public ActionResult Index()
        {
            return View(db.employees.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_employee,account,pass,name,fulControl")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employees.Add(employee);
                db.SaveChanges();
                ThongBao("Thêm thành công!!!", "success");
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            db.employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            db.employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult changeControl(int? id)
        {
            employee employee = db.employees.Find(id);
            if (employee.fulControl == true)
            {
                employee.fulControl = false;
            }
            else
            {
                employee.fulControl = true;
            }
            db.SaveChanges();
            return Json(new
            {
                status = employee.fulControl
            });

        }
        public ActionResult rename(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee nv = db.employees.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View("Rename", nv);
        }
        [HttpPost]
        public ActionResult rename(int? id, string namenew)
        {
            try
            {
                employee nv = db.employees.Find(id);
                nv.name = namenew;
                db.SaveChanges();
                ThongBao("Đổi tên thành công :v", "success");
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult AdminName()
        {
            var admin = Session["UserAdmin"] as WebThueXe.Models.employee;
            int id = admin.id_employee;
            var item = db.employees.Find(id);
            return PartialView("AdminName", item);
        }

        public ActionResult ChangePsw(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee nv = db.employees.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }
        [HttpPost]
        public ActionResult ChangePsw(int? id, string pswOld, string pswNew)
        {
            try
            {
                employee nv = db.employees.Find(id);
                if (nv.pass == pswOld)
                {
                    // Kiểm tra mật khẩu mới
                    var hasNumber = new Regex(@"[0-9]+");
                    var hasUpperChar = new Regex(@"[A-Z]+");
                    var hasMinimum6Chars = new Regex(@".{6,}");
                    var hasSpecialChar = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                    if (!hasNumber.IsMatch(pswNew) || !hasUpperChar.IsMatch(pswNew) || !hasMinimum6Chars.IsMatch(pswNew) || !hasSpecialChar.IsMatch(pswNew))
                    {
                        ThongBao("Mật khẩu phải chứa ít nhất 6 ký tự, bao gồm 1 chữ in hoa và 1 ký tự đặc biệt :v", "error");
                        return RedirectToAction("ChangePsw", new { id = id });
                    }

                    nv.pass = pswNew;
                    db.SaveChanges();
                    ThongBao("Đổi mật khẩu thành công :v", "success");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ThongBao("Mật khẩu hiện tại chưa chính xác :v", "error");
                    return RedirectToAction("ChangePsw", new { id = id });
                }
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public JsonResult VerifyCurrentPassword(int id, string currentPassword)
        {
            var employee = db.employees.Find(id);
            if (employee != null && employee.pass == currentPassword)
            {
                return Json(true);
            }
            return Json(false);
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