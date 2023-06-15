using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Resources;

namespace WebApplication.Controllers
{
    public class UploadController : Controller
    {
        private readonly SqlConnection dbConnection = new SqlConnection();
        private readonly SqlCommand cmd = new SqlCommand();
        // GET: Upload
        public ActionResult Index()
        {
            dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["WebApplication"].ConnectionString;

            List<Penjualan> list = new List<Penjualan>();
            cmd.Connection = dbConnection;
            cmd.Connection.Open();
            cmd.CommandText = string.Format("SELECT TOP 10 {0} FROM penjualan", UploadResource.field_penjualan);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Penjualan p = new Penjualan
                {
                    Id_trx = int.Parse(dr.GetValue(0).ToString()),
                    No_invoice = dr.GetValue(1).ToString(),
                    Total_berat = int.Parse(dr.GetValue(2).ToString()),
                    Ongkos_kirim = int.Parse(dr.GetValue(3).ToString()),
                    Total_harga = int.Parse(dr.GetValue(4).ToString()),
                    Total_harga_beli = int.Parse(dr.GetValue(5).ToString()),
                    Kode_user = int.Parse(dr.GetValue(6).ToString()),
                    Alamat_penerima = dr.GetValue(7).ToString(),
                    Tgl_kirim = DateTime.Parse(dr.GetValue(8).ToString()),
                    Id_ekspedisi = int.Parse(dr.GetValue(9).ToString()),
                    Jenis_pengiriman = dr.GetValue(10).ToString(),
                    Tgl_trx = DateTime.Parse(dr.GetValue(11).ToString())
                };

                list.Add(p); ;
            }

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Upload/Create
        [HttpPost]
        public ActionResult Upload(ViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string saveFolder = UploadResource.temp_folder; //Pick a folder on your machine to store the uploaded files

                    string filePath = Path.Combine(saveFolder, model.File.FileName);

                    model.File.SaveAs(filePath);

                    GetFileFromExcel(filePath);
                }
                Console.Write("Success");
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }

            return RedirectToAction("Index");
        }

        private void GetFileFromExcel(string filePath)
        {
            string excelConnString = string.Format(ConfigurationManager.ConnectionStrings["ExcelConnectionString"].ConnectionString, filePath, "Excel 12.0");

            //Create Connection to Excel work book 
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
            {
                excelConnection.Open();
                DataTable activityDataTable = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (activityDataTable != null)
                {
                    foreach (DataRow itm in activityDataTable.Rows)
                    {
                        bool isDefault = false;
                        string tableName = itm.ItemArray[2].ToString().Replace("$", "");
                        string queryText = UploadResource.BaseQuery;

                        switch (tableName)
                        {
                            case "penjualan":
                                queryText = string.Format(UploadResource.BaseQuery, UploadResource.field_penjualan, tableName);
                                break;
                            case "penjualan_detail":
                                queryText = string.Format(UploadResource.BaseQuery, UploadResource.field_penjualan_detail, tableName);
                                break;
                            case "barang":
                                queryText = string.Format(UploadResource.BaseQuery, UploadResource.field_barang, tableName);
                                break;
                            case "ticket":
                                queryText = string.Format(UploadResource.BaseQuery, UploadResource.field_ticket, tableName);
                                break;
                            case "ticket_process":
                                queryText = string.Format(UploadResource.BaseQuery, UploadResource.field_ticket_process, tableName);
                                break;
                            default:
                                isDefault = true;
                                break;
                        }

                        // If not default then save to DB
                        if (!isDefault)
                        {
                            SaveToDatabase(excelConnection, queryText, tableName);
                        }
                    }
                }
            }
        }

        private void SaveToDatabase(OleDbConnection excelConnection, string queryText, string tableName)
        {
            //Create OleDbCommand to fetch data from Excel 
            using (OleDbCommand cmd = new OleDbCommand(queryText, excelConnection))
            {
                using (OleDbDataReader dReader = cmd.ExecuteReader())
                {
                    dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["WebApplication"].ConnectionString;
                    dbConnection.Open();
                    using (SqlBulkCopy sqlBulk = new SqlBulkCopy(dbConnection))
                    {
                        //Give your Destination table name 
                        sqlBulk.DestinationTableName = tableName;
                        sqlBulk.WriteToServer(dReader);
                    }
                    dbConnection.Close();
                }
            }
        }
    }
}
