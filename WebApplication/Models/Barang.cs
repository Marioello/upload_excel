using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Barang
    {
        [DisplayName("Id produk")]
        public int Id_produk { get; set; }

        [DisplayName("Namea")]
        public int Nama { get; set; }

        [DisplayName("Id kategori")]
        public string Id_kategori { get; set; }

        [DisplayName("Berat")]
        public int Berat { get; set; }

        [DisplayName("Harga beli")]
        public int Harga_beli { get; set; }

        [DisplayName("Stok")]
        public int Stok { get; set; }

        [DisplayName("Harga jual")]
        public int Harga_jual { get; set; }
    }
}