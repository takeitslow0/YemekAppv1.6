using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class YemekTarifiModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(40, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [StringLength(1000, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Tarif")]
        public string Tarif { get; set; }

        [DisplayName("Malzeme")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        public int? MalzemeId { get; set; }


        [DisplayName("Malzeme")]
        public string MalzemeAdiDisplay { get; set; }
    }
}
