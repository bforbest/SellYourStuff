using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SellYourStuff.Models
{
	public class Product
	{
		public int Id { get; set; }
		[Required]
		[StringLength(100,ErrorMessage ="Title should be between 2-100. ",MinimumLength=2)]
		public string Title { get; set; }
		[Required]
		[StringLength(1000, ErrorMessage = "Title should be between 100-1000.", MinimumLength = 100)]
		public string Description { get; set; }
		[Required]
		
		public int Price { get; set; }
		public string Image { get; set; }
		public DateTime PublishedDate { get; set; }/* = DateTime.Now;*/
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public int CategoryId { get; set; }
		public virtual ICollection<Category> Categories { get; set; }

	}
}