﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi24App.ModelViews
{
    public class RiderViewModel
    {
        public int RiderId { get; set; }

         public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OtherNames { get; set; }
        public string? EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? AlternatePhoneNumber { get; set; }
        public string? NationalId { get; set; }
        public string? Location { get; set; }
        public string? DigitalAddress { get; set; }
        public string? ReferralCode { get; set; }
        public bool IsActive { get; set; }
    }
}