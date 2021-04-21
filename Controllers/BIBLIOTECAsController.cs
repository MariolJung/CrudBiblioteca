using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIBLIOTECA2.Models;

namespace BIBLIOTECA2.Controllers
{
    public class BIBLIOTECAsController : Controller
    {
        private BIBLIOTECA2Entities db = new BIBLIOTECA2Entities();

        // GET: BIBLIOTECAs
        public ActionResult Index()
        {
            var bIBLIOTECAs = db.BIBLIOTECAs.Include(b => b.LIBRO).Include(b => b.USUARIO);
            return View(bIBLIOTECAs.ToList());
        }

        // GET: BIBLIOTECAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            if (bIBLIOTECA == null)
            {
                return HttpNotFound();
            }
            return View(bIBLIOTECA);
        }

        // GET: BIBLIOTECAs/Create
        public ActionResult Create()
        {
            ViewBag.Id_Libro = new SelectList(db.LIBROes, "Id", "Nombre");
            ViewBag.Nombre_Usuario = new SelectList(db.USUARIOs, "Nombre", "Contraseña");
            return View();
        }

        // POST: BIBLIOTECAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre_Usuario,Id_Libro")] BIBLIOTECA bIBLIOTECA)
        {
            if (ModelState.IsValid)
            {
                db.BIBLIOTECAs.Add(bIBLIOTECA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Libro = new SelectList(db.LIBROes, "Id", "Nombre", bIBLIOTECA.Id_Libro);
            ViewBag.Nombre_Usuario = new SelectList(db.USUARIOs, "Nombre", "Contraseña", bIBLIOTECA.Nombre_Usuario);
            return View(bIBLIOTECA);
        }

        // GET: BIBLIOTECAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            if (bIBLIOTECA == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Libro = new SelectList(db.LIBROes, "Id", "Nombre", bIBLIOTECA.Id_Libro);
            ViewBag.Nombre_Usuario = new SelectList(db.USUARIOs, "Nombre", "Contraseña", bIBLIOTECA.Nombre_Usuario);
            return View(bIBLIOTECA);
        }

        // POST: BIBLIOTECAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre_Usuario,Id_Libro")] BIBLIOTECA bIBLIOTECA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bIBLIOTECA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Libro = new SelectList(db.LIBROes, "Id", "Nombre", bIBLIOTECA.Id_Libro);
            ViewBag.Nombre_Usuario = new SelectList(db.USUARIOs, "Nombre", "Contraseña", bIBLIOTECA.Nombre_Usuario);
            return View(bIBLIOTECA);
        }

        // GET: BIBLIOTECAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            if (bIBLIOTECA == null)
            {
                return HttpNotFound();
            }
            return View(bIBLIOTECA);
        }

        // POST: BIBLIOTECAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BIBLIOTECA bIBLIOTECA = db.BIBLIOTECAs.Find(id);
            db.BIBLIOTECAs.Remove(bIBLIOTECA);
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
