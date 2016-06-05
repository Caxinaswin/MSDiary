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
    public class TipoRendimentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /*
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var TipoRendimentos = db.TipoRendimentos.Where(d => d.ApplicationUserId == userId);
            return View(TipoRendimentos.ToList());
        }
        */

        public async Task<ActionResult> Index()
        {
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoRendimentos");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var TipoRendimentos =
                JsonConvert.DeserializeObject<IEnumerable<TipoRendimento>>(content);
                return View(TipoRendimentos);
            }
            else
            {
                return Content("Ocorreu um erro: " + response.StatusCode);
            }
        }

        // GET: TipoRendimentos/Details/5
        /*public ActionResult Details(int? id)
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
        }*/

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoRendimentos/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var TipoRendimentos = JsonConvert.DeserializeObject<TipoRendimento>(content);
                if (TipoRendimentos == null) return HttpNotFound();
                return View(TipoRendimentos);
            }
            else
            {
                return Content("Ocorreu um erro: " + response.StatusCode);
            }
        }


        // GET: TipoRendimentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoRendimentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
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
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoRendimentoId,TipoRendimentoNome,ApplicationUserId")] TipoRendimento tipoRendimento)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(User.Identity.GetUserId()); 
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(tipoRendimento);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response = await client.PostAsync("api/TipoRendimentos", content);
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

        // GET: TipoRendimentos/Edit/5
        /*public ActionResult Edit(int? id)
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
        }*/

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoRendimentos/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoRendimento = JsonConvert.DeserializeObject<TipoRendimento>(content);
                if (tipoRendimento == null) return HttpNotFound();
                return View(tipoRendimento);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }


        // POST: TipoRendimentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit([Bind(Include = "TipoRendimentoId,TipoRendimentoNome")] TipoRendimento tipoRendimento)
         {
             if (ModelState.IsValid)
             {
                 tipoRendimento.ApplicationUserId = User.Identity.GetUserId();
                 db.Entry(tipoRendimento).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             return View(tipoRendimento);
         }
         */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoRendimentoId,TipoRendimentoNome")] TipoRendimento tipoRendimento)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                string produtoJSON = JsonConvert.SerializeObject(tipoRendimento);
                HttpContent content = new StringContent(produtoJSON,
                System.Text.Encoding.Unicode, "application/json");
                var response =
                await client.PutAsync("api/TipoRendimentos/" + tipoRendimento.TipoRendimentoId, content);
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


        // GET: TipoRendimentos/Delete/5
        /*public ActionResult Delete(int? id)
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
        */

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = WebApiHttpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/TipoRendimentos/" + id);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var tipoRendimento = JsonConvert.DeserializeObject<TipoRendimento>(content);
                if (tipoRendimento == null) return HttpNotFound();
                return View(tipoRendimento);
            }
            return Content("Ocorreu um erro: " + response.StatusCode);
        }
        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = WebApiHttpClient.GetClient();
                var response = await client.DeleteAsync("api/TipoRendimentos/" + id);
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
