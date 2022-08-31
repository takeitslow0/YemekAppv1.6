using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class YemekTarifi : RecordBase
    {
        [Required]
        [StringLength(400)]
        public string Adi { get; set; }

        [StringLength(10000)]
        public string Tarif { get; set; }

        public int MalzemeId { get; set; }
        public Malzeme Malzemeler { get; set; }
    }
}
