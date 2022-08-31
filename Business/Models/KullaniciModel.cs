using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [MaxLength(20, ErrorMessage = "{0} en çok {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [MaxLength(20, ErrorMessage = "{0} en çok {1} karakter olmalıdır!")]
        [DisplayName("Soyadı")]
        public string Soyadi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [MinLength(3, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [MaxLength(15, ErrorMessage = "{0} en çok {1} karakter olmalıdır!")]
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(10, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }

        [DisplayName("Aktif")]
        public bool AktifMi { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        public int? RolId { get; set; }

        public KullaniciDetayiModel KullaniciDetayi { get; set; }

        [DisplayName("Rol")]
        public string RolAdiDisplay { get; set; }

        [DisplayName("Aktif")]
        public string AktifDisplay { get; set; }
    }
}
