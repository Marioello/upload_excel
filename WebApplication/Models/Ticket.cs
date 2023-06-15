using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Ticket
    {
        [DisplayName("Ticket code")]
        public string Ticket_code { get; set; }

        [DisplayName("Ticket date")]
        public DateTime Ticket_date { get; set; }

        [DisplayName("Customer id")]
        public int Customer_id { get; set; }

        [DisplayName("Subject")]
        public string Subject { get; set; }

        [DisplayName("Id product")]
        public string Id_product { get; set; }

        [DisplayName("Issue")]
        public string Issue { get; set; }
    }
}