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
using MSDiary.Helper;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace MSDiary.Controllers
{
    public class TipoPagamentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoPagamentos
        public async Task<ActionResult> Index()
        {
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoPagamentos");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoPagamento =
                JsonConvert.DeserializeObject<IEnumerable<TipoPagamento>>(content);
                return View(tipoPagamento);
            }
            else
            {
                return Content("Ocorreu um erro: " + response.StatusCode);
            }
        }

        // GET: TipoPagamentos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoPagamentos/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoPagamento = JsonConvert.DeserializeObject<TipoPagamento>(content);
                if (tipoPagamento == null) return HttpNotFound();
                return View(tipoPagamento);
            }
            else
            {
                return Content("Ocorreu um erro: " + response.StatusCode);
            }
        }

        // GET: TipoPagamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
        [Bind(Include = "TipoPagamentoId,TipoPagamentoNome")] TipoPagamento tipoPagamento)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(tipoPagamento);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response = await client.PostAsync("api/TipoPagamentos", content);
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


        // GET: TipoPagamentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoPagamentos/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoPagamento = JsonConvert.DeserializeObject<TipoPagamento>(content);
                if (tipoPagamento == null) return HttpNotFound();
                return View(tipoPagamento);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }

        // POST: TipoPagamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoPagamentoId,TipoPagamentoNome")] TipoPagamento tipoPagamento)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(tipoPagamento);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response =
                await client.PutAsync("api/TipoPagamentos/" + tipoPagamento.TipoPagamentoId, content);
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

        // GET: TipoPagamentos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoPagamentos/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoPagamento = JsonConvert.DeserializeObject<TipoPagamento>(content);
                if (tipoPagamento == null) return HttpNotFound();
                return View(tipoPagamento);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }

        // POST: TipoPagamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                var response = await client.DeleteAsync("api/TipoPagamentos/" + id);
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
