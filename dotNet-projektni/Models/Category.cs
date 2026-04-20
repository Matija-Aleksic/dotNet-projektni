using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class Category
	{
		public int CategoryId { get; set; }
		[Required]
		public string Name { get; set; }
		public ICollection<Item>? ShoppingListItems { get; set; }
	}

}
