using System.ComponentModel;

namespace WebApplication.Models
{
    public class Penjualan_detail
    {
        [DisplayName("Id trx detail")]
        public int Id_trx_detail { get; set; }

        [DisplayName("Id trx")]
        public int Id_trx { get; set; }

        [DisplayName("No invoice")]
        public string No_invoice { get; set; }

        [DisplayName("Id produk")]
        public int Id_produk { get; set; }

        [DisplayName("Jumlah barang")]
        public int Jml_barang { get; set; }

        [DisplayName("Berat")]
        public int Berat { get; set; }

        [DisplayName("Harga satuan")]
        public int Harga_satuan { get; set; }

        [DisplayName("Diskon")]
        public float Diskon { get; set; }

        [DisplayName("Harga")]
        public int Harga { get; set; }
    }
}