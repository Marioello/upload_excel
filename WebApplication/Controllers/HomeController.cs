using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Resources;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string ValidationSummary = "")
        {
            if (Session["USER"] != null)
            {
                return RedirectToAction("Index", "Upload");
            }
            else
            {
                return RedirectToAction("Login", new { ValidationSummary });
            }
        }

        public ActionResult Login(string ValidationSummary = "")
        {
            ViewBag.ValidationSummaryStatus = string.IsNullOrEmpty(ValidationSummary);
            ViewBag.ValidationSummary = ValidationSummary;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User usr)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["WebApplication"].ConnectionString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;
            cmd.CommandText = string.Format(UploadResource.login_email_password, usr.Email, usr.Password);

            try
            {
                cmd.Connection.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                User user = null;
                if (dr.HasRows)
                {
                    user = new User();
                    while (dr.Read())
                    {
                        user.Email = dr["email"].ToString();
                        user.Password = dr["password"].ToString();
                    }
                }

                if (user != null)
                {
                    Session["USER"] = user;
                }
                else
                {
                    // throw error to catch
                    throw new Exception("User tidak ditemukan. Silahkan mendaftarkan diri");
                }
            }
            catch (Exception ex)
            {
                // Close connection
                cmd.Connection.Close();
                // Set error message
                ViewBag.ValidationSummaryStatus = false;
                ViewBag.ValidationSummary = ex.Message.ToString();
                // Return to view
                return View();
            }

            // Redirect back to Index
            return RedirectToAction("Index", new { ValidationSummary = "Login berhasil" });
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User usr)
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = ConfigurationManager.ConnectionStrings["WebApplication"].ConnectionString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;
            cmd.CommandText = string.Format(UploadResource.register_email_password, usr.Email, usr.Password);

            try
            {
                // open connection
                cmd.Connection.Open();
                // exec
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Close connection
                cmd.Connection.Close();
                // Set error message
                ViewBag.ValidationSummaryStatus = false;
                ViewBag.ValidationSummary = ex.Message.ToString();
                // Return to view
                return View();
            }

            // Redirect back to Index
            return RedirectToAction("Index", new { ValidationSummary = "Registrasi berhasil" });
        }

        public ActionResult Logout()
        {
            // Remove USER session
            Session.Remove("USER");

            // Redirect back to Index
            return RedirectToAction("Index", new { ValidationSummary = "Logout berhasil" });
        }
    }
}