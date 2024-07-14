using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi24App.Enums;

namespace Taxi24App.Models
{
    public class Driver : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OtherNames { get; set; }
        public string? EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string? AlternatePhoneNumber { get; set; }
        public string? Driverlicence { get; set; }
        public string? Location { get; set; }
        public DriverStatus? Status { get; set; }
        public string? DigitalAddress { get; set; }
    }
}