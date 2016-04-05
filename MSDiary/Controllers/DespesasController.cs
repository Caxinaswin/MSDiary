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
using System.Web.Security;

namespace MSDiary.Controllers
{
    public class DespesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();    

        // GET: Despesas
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var despesas = db.Despesas.Include(d => d.TipoDespesa).Include(d => d.TipoPagamento).Where(d => d.ApplicationUserId == userId);
            ViewBag.TipoDespesa = db.TipoDespesas.Where(d => d.ApplicationUserId == userId).Select(
            e => new SelectListItem
             {
                Text = e.TipoDespesaNome,
                Value = e.TipoDespesaId.ToString()
             });
            return View(despesas.ToList());
        }
        [HttpPost]
        public ActionResult Index(string pesquisa)
        {
            var userId = User.Identity.GetUserId();
            var resultado = from r in db.Despesas.Include(r => r.TipoDespesa).Include(r => r.TipoPagamento).Where(r => r.ApplicationUserId == userId)
                            select r;

            if (!String.IsNullOrEmpty(pesquisa))
            {
                DateTime d = Convert.ToDateTime(pesquisa);
                resultado = resultado.Where(r => r.Data == d);
            }

            return View(resultado);
        }

        // GET: Despesas/Details/5
        public ActionResult Details(int? id)
        {
            var modelo = db.Despesas.Include("TipoDespesa").Include("TipoPagamento").Where(p => p.DespesaId == id).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewModelDetalhes model = new ViewModelDetalhes()
            {
                TipoDespesa = modelo.TipoDespesa.TipoDespesaNome,
                TipoPagamento = modelo.TipoPagamento.TipoPagamentoNome,
                Descricao = modelo.DespesaDescricao,
                Data = modelo.Data,
                Valor = modelo.DespesaValor,
                comentario = modelo.Comentario,

            };
            return View(model);
        }

        // GET: Despesas/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.TipoDespesaId = new SelectList(db.TipoDespesas.Where(m => m.ApplicationUserId == userId), "TipoDespesaId", "TipoDespesaNome");
            ViewBag.TipoPagamentoId = new SelectList(db.TipoPagamentos.Where(m => m.ApplicationUserId == userId), "TipoPagamentoId", "TipoPagamentoNome");
            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DespesaId,TipoDespesaId,DespesaDescricao,DespesaValor,TipoPagamentoId,Data,Comentario")] Despesa despesa)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {

                var saldo = db.Saldo.Where(d => d.ApplicationUserId == userId).FirstOrDefault();

                despesa.ApplicationUserId = userId;

                if (saldo == null)
                {
                    var s = new Saldo();
                    s.Despesas.Add(despesa);
                    s.ApplicationUserId = userId;
                    db.Saldo.Add(s);
   
                }
                else
                {
                    saldo.Despesas.Add(despesa);
                }

                SaldoUtilizador saldoUtilizador = new SaldoUtilizador()
                {
                    ApplicationUserId = userId,
                    data = despesa.Data,
                    despesaId = despesa.DespesaId,
                    valor = -despesa.DespesaValor,
                };

                db.SaldoUtilizadores.Add(saldoUtilizador);
                db.Despesas.Add(despesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDespesaId = new SelectList(db.TipoDespesas.Where(m => m.ApplicationUserId == userId), "TipoDespesaId", "TipoDespesaNome", despesa.TipoDespesaId);
            ViewBag.TipoPagamentoId = new SelectList(db.TipoPagamentos.Where(m => m.ApplicationUserId == userId), "TipoPagamentoId", "TipoPagamentoNome", despesa.TipoPagamentoId);
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = db.Despesas.Find(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDespesaId = new SelectList(db.TipoDespesas.Where(m => m.ApplicationUserId == userId), "TipoDespesaId", "TipoDespesaNome", despesa.TipoDespesaId);
            ViewBag.TipoPagamentoId = new SelectList(db.TipoPagamentos.Where(m => m.ApplicationUserId == userId), "TipoPagamentoId", "TipoPagamentoNome", despesa.TipoPagamentoId);
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DespesaId,TipoDespesaId,DespesaDescricao,DespesaValor,TipoPagamentoId,Data,Comentario")] Despesa despesa)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(despesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoDespesaId = new SelectList(db.TipoDespesas, "TipoDespesaId", "TipoDespesaNome", despesa.TipoDespesaId);
            ViewBag.TipoPagamentoId = new SelectList(db.TipoPagamentos, "TipoPagamentoId", "TipoPagamentoNome", despesa.TipoPagamentoId);
            return View(despesa);
        }

        // GET: Despesas/Delete/5
        public ActionResult Delete(int? id)
        {
            var modelo = db.Despesas.Include("TipoDespesa").Include("TipoPagamento").Where(p => p.DespesaId == id).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewModelDetalhes model = new ViewModelDetalhes()
            {
                TipoDespesa = modelo.TipoDespesa.TipoDespesaNome,
                TipoPagamento = modelo.TipoPagamento.TipoPagamentoNome,
                Descricao = modelo.DespesaDescricao,
                Data = modelo.Data,
                Valor = modelo.DespesaValor,
                comentario = modelo.Comentario,

            };
            return View(model);
        }

        // POST: Despesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Despesa despesa = db.Despesas.Find(id);
            db.Despesas.Remove(despesa);
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
