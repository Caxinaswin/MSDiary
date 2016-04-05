using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class DadosEstatisticos
    {
        public string utilizador { get; set; }

        [Display(Name = "gastos totais:")]
        public decimal gastos { get; set; }
        [Display(Name = "rendimentos totais:")]
        public decimal rendimentos { get; set; }
        [Display(Name = "data do maior rendimento")]
        [DataType(DataType.Date)]
        public DateTime dataMaiorRendimento { get; set; }
        [Display(Name = "Maior rendimento")]
    
        public decimal maiorValorRendimento { get; set; }
        [Display(Name = "data da maior despesa:")]
        [DataType(DataType.Date)]
        public DateTime dataMaiorDespesa { get; set; }
        [Display(Name = "Maior despesa:")]
        public decimal maiorValorDespesa { get; set; }
       
    }
}