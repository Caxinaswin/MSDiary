using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class Saldo
    {
        public Saldo()
        {
            Despesas = new List<Despesa>();
            Rendimentos = new List<Rendimento>();
            valor = 0;
        }

        public int SaldoId { get; set; }
        public virtual List<Despesa> Despesas { get; set; }
        public virtual List<Rendimento> Rendimentos { get; set; }
        public string ApplicationUserId { get; set; }

        public decimal valor { get; set; }

    }
}