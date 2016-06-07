using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellYourStuff.Models
{
    public class FavList
    {
        public int FavListId { get; set; }
       
        public virtual ICollection<Product> Products { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}