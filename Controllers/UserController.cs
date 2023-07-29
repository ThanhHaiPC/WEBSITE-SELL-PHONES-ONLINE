using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HaiStore.Models;

namespace HaiStore.Controllers
{
    public class UserController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // ĐĂNG KÝ
        public ActionResult Dangky()
        {
            return View();
        }

        // ĐĂNG KÝ PHƯƠNG THỨC POST
        [HttpPost]
        public ActionResult Dangky(Nguoidung nguoidung,string email)
        {
            try
            {
                Session["userReg"] = nguoidung;            
                db.Nguoidungs.Add(nguoidung);              
                db.SaveChanges();             
                if (ModelState.IsValid)
                {
                    if (db.Nguoidungs.Any(x => x.Email == nguoidung.Email))
                    {
                        ViewBag.BiTrungEmail = "Email nay da ton tai!!!";
                        return View("DangKy", nguoidung);
                    }
                    ViewBag.RegOk = "Đăng kí thành công. Đăng nhập ngay";
                    ViewBag.isReg = true;
                    return View("Dangky");
                }
                else
                {
                    return View("Dangky");
                }

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Dangnhap()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Dangnhap(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();
            var islogin = db.Nguoidungs.SingleOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password));

            if (islogin != null)
            {
                if (userMail == "Admin@gmail.com")
                {
                    Session["use"] = islogin;
                    return RedirectToAction("Index", "Admin/Home");
                }
                else
                {
                    Session["use"] = islogin;
                    Session["Nguoidung"] = islogin.Hoten;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Dangnhap");
            }

        }
        public bool IsEmailExists(string email)
        {
            return db.Nguoidungs.Any(u => u.Email == email);
        }

       


       
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nguoidung nguoiDung = db.Nguoidungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nguoidung nguoidung = db.Nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nguoidung.IDQuyen);
            return View(nguoidung);
        }

        // POST: Admin/Nguoidungs/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,Hoten,Email,Dienthoai,Matkhau,IDQuyen, Anhdaidien,Diachi")] Nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoidung).State = EntityState.Modified;
                db.SaveChanges();              
                return RedirectToAction("Profile", new { id = nguoidung.MaNguoiDung });

            }
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nguoidung.IDQuyen);
            return View(nguoidung);
        }
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
    }
}