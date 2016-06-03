using SellYourStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellYourStuff.ModelViews
{
    public class SearchView
    {
        public IQueryable<Product> products { get; set; }
        public SelectList dropDown { get; set; }
    }
}