using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Sehir : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(150, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Şehir Adı")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Ülke Adı")]
        public int UlkeId { get; set; }
        public Ulke Ulke { get; set; }

        public List<KullaniciDetayi> KullaniciDetaylari { get; set; }
    }
}
