using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaiStore.Models;
using PagedList;

namespace HaiStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
        
    {
        Qlbanhang db = new Qlbanhang();
       
        // GET: Admin/Home
        
        public ActionResult Index(int ?page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Sanphams.OrderBy(x => x.Masp);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));

        }

        // Xem chi tiết người dùng GET: Admin/Home/Details
        public ActionResult Details(int id)
        {
            var dt = db.Sanphams.Find(id);
            return View(dt);
        }

        // Tạo sản phẩm mới phương thức GET: Admin/Home/Create
        public ActionResult Create()
        {
            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh");
            return View();
        }

        // POST: Admin/Sanphams/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Masp,Tensp,Giatien,Soluong,Mota,Thesim,Bonhotrong,Sanphammoi,Ram,Anhbia,Mahang,Mahdh")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Sanphams.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang", sanpham.Mahang);
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh", sanpham.Mahdh);
            return View(sanpham);
        }


        public ActionResult Edit(int id)
        {
            // Hiển thị dropdownlist
            var dt = db.Sanphams.Find(id);
            var hangselected = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang",dt.Mahang);
            ViewBag.Mahang = hangselected;
            var hdhselected = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh",dt.Mahdh);
            ViewBag.Mahdh = hdhselected;
           // 
            return View(dt);
        }

        // POST: Admin/Home/Edit
        [HttpPost]
        public ActionResult Edit(Sanpham sanpham)
        {
            try
            {
                var oldItem = db.Sanphams.Find(sanpham.Masp);
                oldItem.Tensp = sanpham.Tensp;
                oldItem.Giatien = sanpham.Giatien;
                oldItem.Soluong = sanpham.Soluong;
                oldItem.Mota = sanpham.Mota;
                oldItem.Anhbia = sanpham.Anhbia;
                oldItem.Bonhotrong = sanpham.Bonhotrong;
                oldItem.Ram = sanpham.Ram;
                oldItem.Thesim = sanpham.Thesim;
                oldItem.Mahang = sanpham.Mahang;
                oldItem.Mahdh = sanpham.Mahdh;
                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var dt = db.Sanphams.Find(id);
            return View(dt);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var dt = db.Sanphams.Find(id);
                db.Sanphams.Remove(dt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
