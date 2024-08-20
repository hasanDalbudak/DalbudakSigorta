using DalbudakSigorta.Data;

namespace DalbudakSigorta.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Police> LatestPoliceler { get; set; }
        public IEnumerable<Musteri> LatestMusteriler { get; set; }
    }
}