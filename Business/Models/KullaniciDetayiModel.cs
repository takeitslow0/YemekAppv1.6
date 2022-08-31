using AppCore.Records.Bases;
using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciDetayiModel : RecordBase
    {
        public Cinsiyet Cinsiyet { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(200, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("E-Posta")]
        public string Eposta { get; set; }

        [DisplayName("Ülke")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        public int? UlkeId { get; set; }

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        public int? SehirId { get; set; }


        public string UlkeAdiDisplay { get; set; }
        public string SehirAdiDisplay { get; set; }
    }
}
