using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class Rendimento
    {

        public int RendimentoId { get; set; }

        public int TipoRendimentoId { get; set; }

        [Display(Name = "Descrição do Rendimento")]
        [Required]
        public string RendimentoDescricao { get; set; }

        [Display(Name = "Valor")]
        [Required]
        public decimal RendimentoValor { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Data { get; set; }

        [Display(Name = "Comentário")]
        public string Comentario { get; set; }

        public TipoRendimento TipoRendimento { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }
    }
}