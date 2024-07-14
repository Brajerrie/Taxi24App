using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Taxi24App.Enums;

namespace Taxi24App.ModelViews
{
    public class DriverIndexViewModel
    {
        public int? DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }

        public int? Status { get; set; }
        public string Driverlicence { get; set; }
        public string Location { get; set; }
        public string Distance { get; set; }
        public string DigitalAddress { get; set; }
        public bool IsActive { get; set; }      
    }


    public class CreateDriverViewModel
    {
        public int? DriverId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Other names can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Other names can only contain letters and spaces.")]
        public string OtherNames { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email address can't be longer than 100 characters.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 characters.")]
        public string PhoneNumber { get; set; }

        [Phone(ErrorMessage = "Invalid alternate phone number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Alternate phone number must be between 10 and 15 characters.")]
        public string AlternatePhoneNumber { get; set; }

        public int? Status { get; set; }

        [Required(ErrorMessage = "Driver license is required.")]
        [StringLength(20, ErrorMessage = "Driver license can't be longer than 20 characters.")]
        public string Driverlicence { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location can't be longer than 100 characters.")]
        public string Location { get; set; }

        [StringLength(100, ErrorMessage = "Distance can't be longer than 100 characters.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Distance must be a valid decimal number with up to two decimal places.")]
        public string Distance { get; set; }

        [StringLength(20, ErrorMessage = "Digital address can't be longer than 20 characters.")]
        public string DigitalAddress { get; set; }

        public bool IsActive { get; set; }
    }



    public class UpdateDriverViewModel
    {
        [Required(ErrorMessage = "Driver ID is required.")]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Other names can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Other names can only contain letters and spaces.")]
        public string OtherNames { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email address can't be longer than 100 characters.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 characters.")]
        public string PhoneNumber { get; set; }

        [Phone(ErrorMessage = "Invalid alternate phone number format.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Alternate phone number must be between 10 and 15 characters.")]
        public string AlternatePhoneNumber { get; set; }

        public int? Status { get; set; }

        [Required(ErrorMessage = "Driver license is required.")]
        [StringLength(20, ErrorMessage = "Driver license can't be longer than 20 characters.")]
        public string Driverlicence { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location can't be longer than 100 characters.")]
        public string Location { get; set; }

        [StringLength(100, ErrorMessage = "Distance can't be longer than 100 characters.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Distance must be a valid decimal number with up to two decimal places.")]
        public string Distance { get; set; }

        [StringLength(20, ErrorMessage = "Digital address can't be longer than 20 characters.")]
        public string DigitalAddress { get; set; }

        public bool IsActive { get; set; }
    }



}