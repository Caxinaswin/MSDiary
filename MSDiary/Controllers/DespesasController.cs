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
using System.Threading.Tasks;
using MSDiary.Helper;
using System.Net.Http;
using Newtonsoft.Json;

namespace MSDiary.Controllers
{
    public class DespesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Despesas
        /*public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var despesas = db.Despesas.Include(d => d.TipoDespesa).Include(d => d.TipoPagamento).Include(d => d.TipoDespesa.subTipo).Where(d => d.ApplicationUserId == userId);
            System.Diagnostics.Debug.WriteLine(userId);
            ViewBag.TipoDespesa = new SelectList(db.TipoDespesas, "TipoDespesaId", "TipoDespesaNome");
            return View(despesas.ToList());
        }
        */
        public async Task<ActionResult> Index()
        {
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/Despesas");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var despesa =
                JsonConvert.DeserializeObject<IEnumerable<Despesa>>(content);
                return View(despesa);
            }
            else
            {
                return Content("Ocorreu um erro: " + response.StatusCode);
            }
        }
        [HttpPost]
        public ActionResult Index(string pesquisa,string tipoDespesa)
        {            ViewBag.TipoDespesa = new SelectList(db.TipoDespesas, "TipoDespesaId", "TipoDespesaNome",tipoDespesa);
            var userId = User.Identity.GetUserId();
            var resultado = from r in db.Despesas.Include(r => r.TipoDespesa).Include(r => r.TipoPagamento).Where(r => r.ApplicationUserId == userId)
                            select r;

            if (!String.IsNullOrEmpty(pesquisa))
            {
                DateTime d = Convert.ToDateTime(pesquisa);
                resultado = resultado.Where(r => r.Data == d);
            }

            if(tipoDespesa != "")
            {
                int tipo = Convert.ToInt32(tipoDespesa);
               resultado = resultado.Where(r => r.TipoDespesaId == tipo);
            }

            return View(resultado);
        }

        // GET: Despesas/Details/5
        /*public ActionResult Details(int? id)
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
            ViewModelDetalhesDespesa model = new ViewModelDetalhesDespesa()
            {
                TipoDespesa = modelo.TipoDespesa.TipoDespesaNome,
                subtTipoDespesa = modelo.TipoDespesa.subTipo,
                TipoPagamento = modelo.TipoPagamento.TipoPagamentoNome,
                Descricao = modelo.DespesaDescricao,
                Data = modelo.Data,
                Valor = modelo.DespesaValor,
                comentario = modelo.Comentario,


            };
            return View(model);
        }*/

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/Despesas/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var despesa = JsonConvert.DeserializeObject<Despesa>(content);
                if (despesa == null) return HttpNotFound();
                return View(despesa);
            }
            else
            {
                return Content("Ocorreu um erro: " + response.StatusCode);
            }
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
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DespesaId,TipoDespesaId,subTipo,DespesaDescricao,DespesaValor,TipoPagamentoId,Data,Comentario,ApplicationUserId")] Despesa despesa)
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

                db.Despesas.Add(despesa);
                db.SaveChanges();

                SaldoUtilizador saldoUtilizador = new SaldoUtilizador()
                {
                    ApplicationUserId = userId,
                    data = despesa.Data,
                    despesaId = despesa.DespesaId,
                    valor = -despesa.DespesaValor,
                    TipoDespesaId = despesa.TipoDespesaId,
                };

                db.SaldoUtilizadores.Add(saldoUtilizador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDespesaId = new SelectList(db.TipoDespesas.Where(m => m.ApplicationUserId == userId), "TipoDespesaId", "TipoDespesaNome", despesa.TipoDespesaId);
            ViewBag.TipoPagamentoId = new SelectList(db.TipoPagamentos.Where(m => m.ApplicationUserId == userId), "TipoPagamentoId", "TipoPagamentoNome", despesa.TipoPagamentoId);
            return View(despesa);
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DespesaId,TipoDespesaId,subTipo,DespesaDescricao,DespesaValor,TipoPagamentoId,Data,Comentario,ApplicationUserId")] Despesa despesa)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(despesa);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response = await client.PostAsync("api/Despesas", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Ocorreu um erro: " + response.StatusCode);
                }
            }
            catch
            {
                return Content("Ocorreu um erro.");
            }
        }

        // GET: Despesas/Edit/5
        /*public ActionResult Edit(int? id)
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
        }*/

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/Produtos/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var despesa = JsonConvert.DeserializeObject<Despesa>(content);
                if (despesa == null) return HttpNotFound();
                return View(despesa);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DespesaId,TipoDespesaId,DespesaDescricao,DespesaValor,TipoPagamentoId,Data,Comentario")] Despesa despesa)
        {
            var userId = User.Identity.GetUserId();
            despesa.ApplicationUserId = userId;
            if (ModelState.IsValid)
            {
                var despesaUtilizador = db.SaldoUtilizadores.Where(x => x.despesaId == despesa.DespesaId).Where(x => x.ApplicationUserId == userId).First();
                despesaUtilizador.valor = despesa.DespesaValor;
                despesaUtilizador.data = despesa.Data;
                db.Entry(despesa).State = EntityState.Modified;
                db.Entry(despesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoDespesaId = new SelectList(db.TipoDespesas, "TipoDespesaId", "TipoDespesaNome", despesa.TipoDespesaId);
            ViewBag.TipoPagamentoId = new SelectList(db.TipoPagamentos, "TipoPagamentoId", "TipoPagamentoNome", despesa.TipoPagamentoId);
            return View(despesa);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdutoId,Nome,Descricao")] Despesa despesa)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(despesa);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response =
                await client.PutAsync("api/Despesas/" + despesa.DespesaId, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Ocorreu um erro: " + response.StatusCode);
                }
            }
            catch
            {
                return Content("Ocorreu um erro.");
            }
        }


        // GET: Despesas/Delete/5
        /*public ActionResult Delete(int? id)
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
            ViewModelDetalhesDespesa model = new ViewModelDetalhesDespesa()
            {
                TipoDespesa = modelo.TipoDespesa.TipoDespesaNome,
                TipoPagamento = modelo.TipoPagamento.TipoPagamentoNome,
                Descricao = modelo.DespesaDescricao,
                Data = modelo.Data,
                Valor = modelo.DespesaValor,
                comentario = modelo.Comentario,

            };
            return View(model);
        }*/

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/Despesas/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var despesa = JsonConvert.DeserializeObject<Despesa>(content);
                if (despesa == null) return HttpNotFound();
                return View(despesa);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }



        // POST: Despesas/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Despesa despesa = db.Despesas.Find(id);
            var userId = User.Identity.GetUserId();
            var despesaUtilizador = db.SaldoUtilizadores.Where(x => x.despesaId == id).Where(x => x.ApplicationUserId == userId).First();
            db.Despesas.Remove(despesa);
            db.SaldoUtilizadores.Remove(despesaUtilizador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
