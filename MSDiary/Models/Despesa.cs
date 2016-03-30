using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class Despesa
    {
        public int TipoDespesaId { get; set; }

        public int DespesaId { get; set; }

        [Display(Name = "Descrição da Despesa")]
        [Required]
        public string DespesaDescricao { get; set; }

        [Display(Name = "Valor")]
        [Required]
        public decimal DespesaValor { get; set; }

        public int TipoPagamentoId { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Data { get; set; }

        public TipoDespesa TipoDespesa { get; set; }

        public TipoPagamento TipoPagamento { get; set; }

        [Display(Name = "Comentário")]
        public string Comentario { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }

    }
}