using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HaiStore.Models;

namespace HaiStore.Areas.Admin.Controllers
{
    public class HedieuhanhsController : Controller
    {
        private Qlbanhang db = new Qlbanhang();

        // GET: Admin/Hedieuhanhs
        public ActionResult Index()
        {
            return View(db.Hedieuhanhs.ToList());
        }

        // GET: Admin/Hedieuhanhs/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hedieuhanh hedieuhanh = db.Hedieuhanhs.Find(id);
            if (hedieuhanh == null)
            {
                return HttpNotFound();
            }
            return View(hedieuhanh);
        }

        // GET: Admin/Hedieuhanhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hedieuhanhs/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mahdh,Tenhdh")] Hedieuhanh hedieuhanh)
        {
            if (ModelState.IsValid)
            {
                db.Hedieuhanhs.Add(hedieuhanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hedieuhanh);
        }

        // GET: Admin/Hedieuhanhs/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hedieuhanh hedieuhanh = db.Hedieuhanhs.Find(id);
            if (hedieuhanh == null)
            {
                return HttpNotFound();
            }
            return View(hedieuhanh);
        }

        // POST: Admin/Hedieuhanhs/Edit
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mahdh,Tenhdh")] Hedieuhanh hedieuhanh)
        {
            if (ModelState.IsValid)
            {
                //Thuộc tính "State" = "EntityState.Modified"  đối tượng "hedieuhanh" đã được sửa đổi  
                db.Entry(hedieuhanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hedieuhanh);
        }

        // GET: Admin/Hedieuhanhs/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hedieuhanh hedieuhanh = db.Hedieuhanhs.Find(id);
            if (hedieuhanh == null)
            {
                return HttpNotFound();
            }
            return View(hedieuhanh);
        }

        // POST: Admin/Hedieuhanhs/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hedieuhanh hedieuhanh = db.Hedieuhanhs.Find(id);
            db.Hedieuhanhs.Remove(hedieuhanh);
            db.SaveChanges();
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
