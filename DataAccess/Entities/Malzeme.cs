using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Malzeme : RecordBase
    {
        [Required]
        [StringLength(4000)]
        public string Adi { get; set; }

        public List<YemekTarifi> YemekTarifleri { get; set; }
    }
}
