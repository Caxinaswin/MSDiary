using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class ViewModelDetalhes
    {
        public string TipoDespesa { get; set; }
        public string TipoPagamento { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public string comentario { get; set; }


    }
}