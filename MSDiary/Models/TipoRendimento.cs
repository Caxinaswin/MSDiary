using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSDiary.Models
{
    public class TipoRendimento
    {
        public int TipoRendimentoId { get; set; }
        [Display(Name = "Tipo de Rendimento")]
        [Required]
        public string TipoRendimentoNome { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }
    }
}