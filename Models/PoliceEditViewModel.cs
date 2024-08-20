using DalbudakSigorta.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DalbudakSigorta.Models
{
    public class PoliceEditViewModel
    {
        public Police? Police { get; set; }
        public AracKayit? AracKayit { get; set; }

        public List<SelectListItem> StatusList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "T", Text = "Teklif (T)" },
            new SelectListItem { Value = "P", Text = "Poliçe (P)" }
        };
    }
}
