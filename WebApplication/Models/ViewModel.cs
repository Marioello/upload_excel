using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication.Models
{
    public class ViewModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}