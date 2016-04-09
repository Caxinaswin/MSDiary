﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class SaldoUtilizador
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? data { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? TipoDespesaId { get; set; }
        
        public TipoDespesa tipoDespesa { get; set; }

        public int? TipoRendimentoId { get; set; }

        public TipoPagamento tipoPagamento { get; set; }

        public string ApplicationUserId { get; set; }

        public int? despesaId { get; set; }

        public int? rendimentoId { get; set; }

        public decimal valor { get; set; }
    }
}