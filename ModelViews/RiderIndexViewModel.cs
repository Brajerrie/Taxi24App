using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Taxi24App.Enums;

namespace Taxi24App.ModelViews
{
    public class RiderIndexViewModel
    {
        public int? RiderId { get; set; }
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

    public class CreateRiderViewModel
    {
        public int? RiderId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? OtherNames { get; set; }
        public string? EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? AlternatePhoneNumber { get; set; }
        public string? NationalId { get; set; }
        [Required]
        public string? Location { get; set; }
        public string? DigitalAddress { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateRiderViewModel
    {
        [Required]
        public int RiderId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? OtherNames { get; set; }
        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? AlternatePhoneNumber { get; set; }

        public string? NationalId { get; set; }
        public string? Location { get; set; }
        public string? DigitalAddress { get; set; }
        public string? ReferralCode { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }

    public class RiderLocationViewModel
    {
        public RiderLocationViewModel()
        {
            Rider = new RiderViewModel();
            Drivers = new List<DriverViewModel>();
        }
        public RiderViewModel Rider { get; set; }
        public IList<DriverViewModel> Drivers { get; set; }
        
    }
}