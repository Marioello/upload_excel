using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Ticket_process
    {
        [DisplayName("Ticket code")]
        public string Ticket_code { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("User id")]
        public string User_id { get; set; }

        [DisplayName("Update date")]
        public DateTime Update_date { get; set; }
    }
}