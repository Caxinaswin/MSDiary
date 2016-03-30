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
    public class TipoRendimentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoRendimentos
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var TipoRendimentos = db.TipoRendimentos.Where(d => d.ApplicationUserId == userId);
            return View(TipoRendimentos.ToList());
        }

        // GET: TipoRendimentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRendimento tipoRendimento = db.TipoRendimentos.Find(id);
            if (tipoRendimento == null)
            {
                return HttpNotFound();
            }
            return View(tipoRendimento);
        }

        // GET: TipoRendimentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoRendimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoRendimentoId,TipoRendimentoNome")] TipoRendimento tipoRendimento)
        {
            if (ModelState.IsValid)
            {
                tipoRendimento.ApplicationUserId = User.Identity.GetUserId();
                db.TipoRendimentos.Add(tipoRendimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoRendimento);
        }

        // GET: TipoRendimentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRendimento tipoRendimento = db.TipoRendimentos.Find(id);
            if (tipoRendimento == null)
            {
                return HttpNotFound();
            }
            return View(tipoRendimento);
        }

        // POST: TipoRendimentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoRendimentoId,TipoRendimentoNome")] TipoRendimento tipoRendimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoRendimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoRendimento);
        }

        // GET: TipoRendimentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoRendimento tipoRendimento = db.TipoRendimentos.Find(id);
            if (tipoRendimento == null)
            {
                return HttpNotFound();
            }
            return View(tipoRendimento);
        }

        // POST: TipoRendimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoRendimento tipoRendimento = db.TipoRendimentos.Find(id);
            db.TipoRendimentos.Remove(tipoRendimento);
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
