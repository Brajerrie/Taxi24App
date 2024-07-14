using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi24App.Enums;

namespace Taxi24App.Models
{
    public class Trip : BaseModel
    {
        public string? CompanyName { get; set; }
        public string? DocumentNumber { get; set; }
        public string Refereence { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalPaid { get; set; }
        public int? RiderId { get; set; }
        public int? DriverId { get; set; }
        public DateTime? StartTIme { get; set; }
        public DateTime? EndTIme { get; set; }
        public string? Duration { get; set; }
        public TripStatus? Status { get; set; }
        public string Origin { get; set; }
        public string? Destination { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Rider Rider { get; set; }
    }
}