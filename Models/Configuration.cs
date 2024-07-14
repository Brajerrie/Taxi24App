using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi24App.Models
{
    public class Configuration : BaseModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public string? Description { get; set; }
        public string? ValueType { get; set; } //Bool, string or numeric
    }
}