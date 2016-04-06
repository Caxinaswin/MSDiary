using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class TipoDespesa
    {
        public int TipoDespesaId { get; set; }
        [Display(Name = "Tipo de Despesa")]
        [Required]
        public string TipoDespesaNome { get; set; }
       
        public virtual  TipoDespesa subTipo { get; set; }
        [Display(Name = "Subtipo de Despesa")]
        [ForeignKey("subTipo")]
        public int? subTipoDespesaId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }
    }
}