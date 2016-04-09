using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSDiary.Models
{
    public class viewModelTipoDespesas
    {
        public int CategoriaID { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}