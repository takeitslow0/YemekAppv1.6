using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class SehirModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(150, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Şehir Adı")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Ülke Adı")]
        public int? UlkeId { get; set; }

        public UlkeModel Ulke { get; set; }
    }
}
