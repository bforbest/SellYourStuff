using SellYourStuff.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellYourStuff.ModelViews
{
        public class ProfileViewModel
        {
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }
            [Phone]
            [Display(Name = "Phone Number")]
            public string Number { get; set; }
            public ICollection<Product> Products { get; set; }
            public Region Region { get; set; }
        }
}