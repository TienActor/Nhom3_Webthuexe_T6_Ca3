using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThueXe.Models;

namespace WebThueXe.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ThueXeEntities db = new ThueXeEntities();
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }


        public ActionResult AddCart(int id)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<CartItem>();
            }

            List<CartItem> giohang = Session["giohang"] as List<CartItem>;

            if (giohang.FirstOrDefault(m => m.id_cars == id) == null) // ko co sp nay trong gio hang
            {
                car sp = db.cars.Find(id);

                CartItem newItem = new CartItem()
                {
                    id_cars = id,
                    name = sp.name,
                    SoLuong = 1,
                    image = sp.image,
                    price = (int)sp.price,

                };  // Tạo ra 1 CartItem mới

                giohang.Add(newItem);  // Thêm CartItem vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                CartItem cardItem = giohang.FirstOrDefault(m => m.id_cars == id);
                cardItem.SoLuong++;
            }
            return Redirect(url: Request.UrlReferrer.ToString());
            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            //return RedirectToAction("Details", "Cars", new { idx = id });
        }
        public RedirectToRouteResult SuaSoLuong(int SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemSua = giohang.FirstOrDefault(m => m.id_cars == SanPhamID);
            if (itemSua != null)
            {
                itemSua.SoLuong = soluongmoi;
            }
            return RedirectToAction("Index");

        }

        public RedirectToRouteResult XoaKhoiGio(int SanPhamID)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem itemXoa = giohang.FirstOrDefault(m => m.id_cars == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection frm)
        {
            // Kiểm tra giỏ hàng không trống
            if (Session["giohang"] == null || !(Session["giohang"] is List<CartItem> carts) || !carts.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng không tồn tại hoặc đã trống.");
                return View();
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    rent rent = new rent()
                    {
                        name = frm["inputUsername"],
                        phone = frm["inputPhone"],
                        mail = frm["inputEmail"],
                        note = frm["inputNote"],
                        date = DateTime.Now,
                        // Thêm mã KH từ session nếu người dùng đã đăng nhập
                        maKH = (Session["KH"] as KhachHang)?.maKH
                    };
                    db.rents.Add(rent);
                    db.SaveChanges();
                    foreach (CartItem item in carts)
                    {
                        rentDetail rentDetail = new rentDetail()
                        {
                            id_rent = rent.id_rent,
                            id_cars = item.id_cars,
                            amount = item.SoLuong,
                            maKH = rent.maKH // Sử dụng maKH từ đối tượng rent
                        };
                        db.rentDetails.Add(rentDetail);
                    }
                    db.SaveChanges();
                    dbContextTransaction.Commit();


                    Session.Remove("giohang");
                    return RedirectToAction("RentSuccess");
                }
                catch (Exception ex)
                {
                    // Log the error
                    dbContextTransaction.Rollback();
                    ModelState.AddModelError("", "Có lỗi xảy ra khi xử lý đơn hàng của bạn: " + ex.Message);
                    // Optionally, pass the exception data to the Error view
                    return View("Error", ex);
                }
            }
        }

    }
}
