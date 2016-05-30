using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SellYourStuff.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageRecieved { get; set; }
        public DateTime DateRecieved { get; set; }
        public bool IsSeen { get; set; }
    }
}