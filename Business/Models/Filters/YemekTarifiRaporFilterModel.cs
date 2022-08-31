using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.Filters
{
    public class YemekTarifiRaporFilterModel
    {
        [DisplayName("Malzeme")]
        public int? MalzemeId { get; set; }

        [StringLength(40, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Yemek Adı")]
        public string YemekAdi { get; set; }
    }
}
