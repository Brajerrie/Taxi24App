using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taxi24App.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            IsActive = true;
        }

        [Column]
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime? DeletedAt { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }
    }
}