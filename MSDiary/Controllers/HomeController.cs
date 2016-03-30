using Microsoft.AspNet.Identity;
using MSDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSDiary.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult _ObtemSaldo()
        {
            var userId = User.Identity.GetUserId();
            var saldo = db.Saldo.Where(d => d.ApplicationUserId == userId).FirstOrDefault();
            decimal valor = 0;
            if(saldo != null)
            { 
                foreach (var item in saldo.Despesas)
                {
                 valor -= item.DespesaValor;
                }
                foreach (var item in saldo.Rendimentos)
                {
                  valor += item.RendimentoValor;
                }
            }

            return PartialView(valor);
        }

    }
}