using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            var userId = User.Identity.GetUserId();
            var queryEntreDatas = db.SaldoUtilizadores.Where(d => d.ApplicationUserId == userId)
          .GroupBy(x => x.ApplicationUserId)
          .Select(x => new {
              biggestValor = x.Where(y => y.valor > 0).Max(y => (decimal?)y.valor)?? null,
              lowestValor = x.Where(y => y.valor < 0).Min(y => (decimal?)y.valor)?? null,
              expenses = x.Where(y => y.valor < 0).Sum(y => (decimal?)y.valor) ?? 0,
              earnings = x.Where(y => y.valor > 0).Sum(y => (decimal?)y.valor) ?? 0,
              lowestDate = x.Where(y => y.valor == x.Min(z => z.valor)).Where(y => y.valor < 0).Select(y => y.data).FirstOrDefault(),
              biggestDate = x.Where(y => y.valor == x.Max(z => z.valor)).Where(y => y.valor > 0).Select(y => y.data).FirstOrDefault()
          }).SingleOrDefault();

            if (userId != null && queryEntreDatas != null)
            {
                DadosEstatisticos modelo = new DadosEstatisticos()
                 {
                     utilizador = User.Identity.GetUserName(),
                     gastos = queryEntreDatas.expenses,
                     rendimentos = queryEntreDatas.earnings,
                     maiorValorDespesa = queryEntreDatas.lowestValor,
                     dataMaiorDespesa = queryEntreDatas.lowestDate,
                     dataMaiorRendimento = queryEntreDatas.biggestDate,
                     maiorValorRendimento = queryEntreDatas.biggestValor
                 };
                return View(modelo);
            }
            else
            {
                return View();
            }
           
        }

        [HttpPost]
        public ActionResult Index(string dataInicial, string dataFinal)
        {
            var userId = User.Identity.GetUserId();

            var queryDefault = db.SaldoUtilizadores.Where(d => d.ApplicationUserId == userId)
         .GroupBy(x => x.ApplicationUserId)
         .Select(x => new
         {
             biggestValor = x.Where(y => y.valor > 0).Max(y => (decimal?)y.valor) ?? null,
             lowestValor = x.Where(y => y.valor < 0).Min(y => (decimal?)y.valor) ?? null,
             expenses = x.Where(y => y.valor < 0).Sum(y => (decimal?)y.valor) ?? 0,
             earnings = x.Where(y => y.valor > 0).Sum(y => (decimal?)y.valor) ?? 0,
             lowestDate = x.Where(y => y.valor == x.Min(z => z.valor)).Where(y => y.valor < 0).Select(y => y.data).FirstOrDefault(),
             biggestDate = x.Where(y => y.valor == x.Max(z => z.valor)).Where(y => y.valor > 0).Select(y => y.data).FirstOrDefault()
         }).FirstOrDefault();
            if (queryDefault != null)
            {

                DadosEstatisticos modeloDefault = new DadosEstatisticos()
                {
                    utilizador = User.Identity.GetUserName(),
                    gastos = queryDefault.expenses,
                    rendimentos = queryDefault.earnings,
                    maiorValorDespesa = queryDefault.lowestValor,
                    dataMaiorDespesa = queryDefault.lowestDate,
                    dataMaiorRendimento = queryDefault.biggestDate,
                    maiorValorRendimento = queryDefault.biggestValor
                };


                System.Diagnostics.Debug.WriteLine(dataInicial);

                if (dataInicial != "" && dataFinal != "")
                {
                    DateTime data1 = DateTime.Parse(dataInicial);
                    DateTime data2 = DateTime.Parse(dataFinal);

                    var queryEntreDatas = db.SaldoUtilizadores.Where(d => d.ApplicationUserId == userId && d.data >= data1 && d.data <= data2)
                 .GroupBy(x => x.ApplicationUserId)
                 .Select(x => new
                 {
                      biggestValor = x.Where(y => y.valor > 0).Max(y => (decimal?)y.valor) ?? null,
             lowestValor = x.Where(y => y.valor < 0).Min(y => (decimal?)y.valor) ?? null,
             expenses = x.Where(y => y.valor < 0).Sum(y => (decimal?)y.valor) ?? 0,
             earnings = x.Where(y => y.valor > 0).Sum(y => (decimal?)y.valor) ?? 0,
             lowestDate = x.Where(y => y.valor == x.Min(z => z.valor)).Where(y => y.valor < 0).Select(y => y.data).FirstOrDefault(),
             biggestDate = x.Where(y => y.valor == x.Max(z => z.valor)).Where(y => y.valor > 0).Select(y => y.data).FirstOrDefault()
                 }).FirstOrDefault();



                    if (queryEntreDatas != null)
                    {
                        DadosEstatisticos modelo = new DadosEstatisticos()
                        {
                            utilizador = User.Identity.GetUserName(),
                            gastos = queryEntreDatas.expenses,
                            rendimentos = queryEntreDatas.earnings,
                            maiorValorDespesa = queryEntreDatas.lowestValor,
                            dataMaiorDespesa = queryEntreDatas.lowestDate,
                            dataMaiorRendimento = queryEntreDatas.biggestDate,
                            maiorValorRendimento = queryEntreDatas.biggestValor
                        };
                        return View(modelo);
                    }

                    else
                    {
                        return View(modeloDefault);
                    }

                }
                else
                {
                    return View(modeloDefault);
                }
            }
            else
            {
                return View();
            }

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


