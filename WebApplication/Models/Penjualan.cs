using System;
using System.ComponentModel;

namespace WebApplication.Models
{
    public class Penjualan
    {
        [DisplayName("Id trx")]
        public int Id_trx { get; set; }

        [DisplayName("No Invoice")]
        public string No_invoice { get; set; }

        [DisplayName("Total Berat")]
        public int Total_berat { get; set; }

        [DisplayName("Ongkos Kirim")]
        public int Ongkos_kirim { get; set; }

        [DisplayName("Total harga")]
        public int Total_harga { get; set; }

        [DisplayName("Total harga beli")]
        public int Total_harga_beli { get; set; }

        [DisplayName("Kode user")]
        public int Kode_user { get; set; }

        [DisplayName("Alamat penerima")]
        public string Alamat_penerima { get; set; }

        [DisplayName("Tgl kirim")]
        public DateTime Tgl_kirim { get; set; }

        [DisplayName("Id Ekspedisi")]
        public int Id_ekspedisi { get; set; }

        [DisplayName("Jenis pengiriman")]
        public string Jenis_pengiriman { get; set; }

        [DisplayName("Tgl trx")]
        public DateTime Tgl_trx { get; set; }

    }
}