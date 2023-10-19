    using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebThueXe.Models;

namespace WebThueXe.Controllers
{
    public class AccountController : Controller
    {
        private ThueXeEntities db = new ThueXeEntities();
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        // GET: KhachHang/Create
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "maKH,matKhau,hoTenKH,sdtKH,diaChiKH,emailKH")] KhachHang kh)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(kh.maKH) ||
                    string.IsNullOrWhiteSpace(kh.matKhau) ||
                    string.IsNullOrWhiteSpace(kh.hoTenKH) ||
                    string.IsNullOrWhiteSpace(kh.sdtKH) ||
                    string.IsNullOrWhiteSpace(kh.diaChiKH) ||
                    string.IsNullOrWhiteSpace(kh.emailKH))
                {
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                    return View(kh);
                }
                if (db.KhachHangs.Any(k => k.maKH == kh.maKH))
                {
                    ModelState.AddModelError("", "Tên tài khoản đã được sử dụng!");
                    return View(kh);
                }

                if (kh.matKhau.Length < 6)
                {
                    ModelState.AddModelError("matKhau", "Mật khẩu phải chứa ít nhất 6 kí tự.");
                    return View(kh);
                }

                if (!kh.emailKH.EndsWith("@gmail.com"))
                {
                    ModelState.AddModelError("emailKH", "Email phải kết thúc bằng '@gmail.com'.");
                    return View(kh);
                }

                // kiểm tra số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số từ 0 -> 9
                if (!Regex.IsMatch(kh.sdtKH, "^0\\d{8,}$"))
                {
                    ModelState.AddModelError("sdtKH", "Số điện thoại phải bắt đầu bằng số 0 và có ít nhất 9 chữ số.");
                    return View(kh);
                }

                db.KhachHangs.Add(kh);
                db.SaveChanges();
                Session["KH"] = kh;
                return RedirectToAction("Login", "Account");
            }

            return View(kh);

        }


        public ActionResult CaNhan()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                kh = (KhachHang)Session["KH"];
            }
            return View(kh);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaNhan([Bind(Include = "maKH,matKhau,hoTenKH,sdtKH,diaChiKH,emailKH")] KhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKhachHang).State = EntityState.Modified;
                db.SaveChanges();
                Session["KH"] = tblKhachHang;
                return RedirectToAction("Index", "Home");
            }
            return View(tblKhachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang objUser)
        {
            if (ModelState.IsValid)
            {
                var obj = db.KhachHangs.Where(a => a.maKH.Equals(objUser.maKH) && a.matKhau.Equals(objUser.matKhau)).FirstOrDefault();

                if (obj != null)
                {
                    Session["KH"] = obj;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["KH"] = null;
            KhachHang kh = (KhachHang)Session["KH"];
            if (kh != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult Logout()
        {
            Session["KH"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}