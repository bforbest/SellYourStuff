using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SellYourStuff.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}