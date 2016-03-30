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
    public class RendimentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rendimentos
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var rendimentos = db.Rendimentos.Include(r => r.TipoRendimento).Where(d => d.ApplicationUserId == userId);
            return View(rendimentos.ToList());
        }

        [HttpPost]
        public ActionResult Index(string pesquisa)
        {
            var userId = User.Identity.GetUserId();
            var resultado = from r in db.Rendimentos.Include(r => r.TipoRendimento).Where(r => r.ApplicationUserId == userId)
            select r;

            if (!String.IsNullOrEmpty(pesquisa))
            {
                DateTime d = Convert.ToDateTime(pesquisa);
                resultado = resultado.Where(r => r.Data == d);
            }

            return View(resultado);
        }

        // GET: Rendimentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendimento rendimento = db.Rendimentos.Find(id);
            if (rendimento == null)
            {
                return HttpNotFound();
            }
            return View(rendimento);
        }

        // GET: Rendimentos/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.TipoRendimentoId = new SelectList(db.TipoRendimentos.Where(m => m.ApplicationUserId == userId), "TipoRendimentoId", "TipoRendimentoNome");
            return View();
        }

        // POST: Rendimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RendimentoId,TipoRendimentoId,RendimentoDescricao,RendimentoValor,Data,Comentario")] Rendimento rendimento)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                rendimento.ApplicationUserId = User.Identity.GetUserId();

                var saldo = db.Saldo.Where(d => d.ApplicationUserId == userId).FirstOrDefault();

                if (saldo == null)
                {
                    var s = new Saldo();
                    s.Rendimentos.Add(rendimento);
                    s.ApplicationUserId = userId;
                    db.Saldo.Add(s);
                }
                else
                {
                    saldo.Rendimentos.Add(rendimento);
                }

                SaldoUtilizador saldoUtilizador = new SaldoUtilizador()
                {
                    ApplicationUserId = userId,
                    data = rendimento.Data,
                    rendimentoId = rendimento.RendimentoId,
                    valor = rendimento.RendimentoValor,
                };

                db.SaldoUtilizadores.Add(saldoUtilizador);
                db.Rendimentos.Add(rendimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoRendimentoId = new SelectList(db.TipoRendimentos.Where(m => m.ApplicationUserId == userId), "TipoRendimentoId", "TipoRendimentoNome", rendimento.TipoRendimentoId);
            return View(rendimento);
        }

        // GET: Rendimentos/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendimento rendimento = db.Rendimentos.Find(id);
            if (rendimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoRendimentoId = new SelectList(db.TipoRendimentos.Where(m => m.ApplicationUserId == userId), "TipoRendimentoId", "TipoRendimentoNome", rendimento.TipoRendimentoId);
            return View(rendimento);
        }

        // POST: Rendimentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RendimentoId,TipoRendimentoId,RendimentoDescricao,RendimentoValor,Data,Comentario")] Rendimento rendimento)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(rendimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoRendimentoId = new SelectList(db.TipoRendimentos.Where(m => m.ApplicationUserId == userId), "TipoRendimentoId", "TipoRendimentoNome", rendimento.TipoRendimentoId);
            return View(rendimento);
        }

        // GET: Rendimentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendimento rendimento = db.Rendimentos.Find(id);
            if (rendimento == null)
            {
                return HttpNotFound();
            }
            return View(rendimento);
        }

        // POST: Rendimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rendimento rendimento = db.Rendimentos.Find(id);
            db.Rendimentos.Remove(rendimento);
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
