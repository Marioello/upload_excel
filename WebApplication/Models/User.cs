using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class User
    {
        [Required(ErrorMessage = "Email harus diisi")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Format email salah.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password harus diisi")]
        public string Password { get; set; }
    }
}