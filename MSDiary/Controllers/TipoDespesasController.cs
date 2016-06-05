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
using System.Threading.Tasks;
using MSDiary.Helper;
using System.Net.Http;
using Newtonsoft.Json;

namespace MSDiary.Controllers
{
    public class TipoDespesasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoDespesas
        public async Task<ActionResult> Index()
        {
            
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoDespesas");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoDespesa =
                JsonConvert.DeserializeObject<IEnumerable<TipoDespesa>>(content);
                return View(tipoDespesa);
            }
            else
            {

                return Content("Ocorreu um erro: " + response.StatusCode);
            }
        }


        // GET: TipoDespesas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoDespesas/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipodespesa = JsonConvert.DeserializeObject<TipoDespesa>(content);
                if (tipodespesa == null) return HttpNotFound();
                return View(tipodespesa);
            }
            else
            {
                return Content("Ocorreu um erro: " + response.StatusCode);
            }
        }

        // GET: TipoDespesas/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            return View();
        }

        // POST: TipoDespesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoDespesaId,TipoDespesaNome,subTipoDespesaId")] TipoDespesa tipoDespesa)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(tipoDespesa);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response = await client.PostAsync("api/TipoDespesas", content);
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

        // GET: TipoDespesas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoDespesas/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoDespesa = JsonConvert.DeserializeObject<TipoDespesa>(content);
                if (tipoDespesa == null) return HttpNotFound();
                return View(tipoDespesa);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }

        // POST: TipoDespesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoDespesaId,TipoDespesaNome,subTipoDespesaId")] TipoDespesa tipoDespesa)
        {
            ViewBag.subTipoDespesaId = new SelectList(db.TipoDespesas, "TipoDespesaId", "TipoDespesaNome", tipoDespesa.subTipoDespesaId);
            try
            {
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(tipoDespesa);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response =
                await client.PutAsync("api/TipoDespesas/" + tipoDespesa.TipoDespesaId, content);
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



        // GET: TipoDespesas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoDespesas/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoDespesa = JsonConvert.DeserializeObject<TipoDespesa>(content);
                if (tipoDespesa == null) return HttpNotFound();
                return View(tipoDespesa);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }

        // POST: TipoDespesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                var response = await client.DeleteAsync("api/TipoDespesas/" + id);
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
