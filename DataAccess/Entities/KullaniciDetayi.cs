﻿using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class KullaniciDetayi
    {
        [Key]
        public int KullaniciId { get; set; }

        public Kullanici Kullanici { get; set; }

        public Cinsiyet Cinsiyet { get; set; }

        [Required]
        [StringLength(40)]
        public string Eposta { get; set; }

        public int UlkeId { get; set; }
        public Ulke Ulke { get; set; }

        public int SehirId { get; set; }
        public Sehir Sehir { get; set; }
    }
}
