using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
	public class ShoppingList
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ShoppingListId { get; set; }
		[Column(TypeName = "text")]
		[Required]
		public string Name { get; set; }
		[Column(TypeName = "text")]
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
		public ICollection<Item>? ShoppingListItems { get; set; }


	}

}
