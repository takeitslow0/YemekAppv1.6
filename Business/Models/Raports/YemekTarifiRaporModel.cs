using System.ComponentModel;

namespace Business.Models.Raports
{
    public class YemekTarifiRaporModel
    {
        public int? MalzemeId { get; set; }

        [DisplayName("Malzeme")]
        public string MalzemeAdi { get; set; }


        [DisplayName("Yemek")]
        public string YemekAdi { get; set; }

        public string YemekTarifi { get; set; }
    }
}
