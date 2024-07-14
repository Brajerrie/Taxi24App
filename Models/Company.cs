using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi24App.Enums;

namespace Taxi24App.Models
{
    public class Company : BaseModel
    {
        public string Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public CompanyType? Type { get; set; }
        public string? Location { get; set; }
        public string? DigitalAddress { get; set; }
        public string? TaxNumber { get; set; }
    }
}