using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taxi24App.Models
{
    public class User
    {
        [Column]
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string Name { get; set; }
        public string Userrname { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }

        public bool ForcePasswordReset { get; set; }
    }
}