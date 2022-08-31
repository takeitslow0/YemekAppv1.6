using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Kullanici : RecordBase
    {
        [Required]
        [MinLength(3)]
        [MaxLength(18)]
        public string Adi { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(18)]
        public string Soyadi { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string KullaniciAdi { get; set; }

        [Required]
        [StringLength(20)]
        public string Sifre { get; set; }

        public bool AktifMi { get; set; }

        public KullaniciDetayi KullaniciDetayi { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}
