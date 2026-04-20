using System.ComponentModel.DataAnnotations;

namespace Model
{
	public class Item
	{
		public int ItemId { get; set; }
		[Required]
		public string Name { get; set; }
		public int? Quantity { get; set; }
		public string? Unit { get; set; }
		public bool IsPurchased { get; set; }
		[Required]
		public int ShoppingListId { get; set; }

		[Required]
		public int CategoryId { get; set; }
	}
}
