using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MSDiary.Models;
using Microsoft.AspNet.Identity;

namespace MSDiary.Controllers
{
    public class TipoDespesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoDespesas
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var TipoDespesas = db.TipoDespesas.Where(d => d.ApplicationUserId == userId);
            return View(TipoDespesas.ToList());
        }

        // GET: TipoDespesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            if (tipoDespesa == null)
            {
                return HttpNotFound();
            }
            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDespesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoDespesaId,TipoDespesaNome")] TipoDespesa tipoDespesa)
        {
            if (ModelState.IsValid)
            {
                tipoDespesa.ApplicationUserId = User.Identity.GetUserId();
                db.TipoDespesas.Add(tipoDespesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            if (tipoDespesa == null)
            {
                return HttpNotFound();
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoDespesaId,TipoDespesaNome")] TipoDespesa tipoDespesa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDespesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            if (tipoDespesa == null)
            {
                return HttpNotFound();
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDespesa tipoDespesa = db.TipoDespesas.Find(id);
            db.TipoDespesas.Remove(tipoDespesa);
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
