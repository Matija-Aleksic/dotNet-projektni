using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Item
	{
		public int ItemId { get; set; }
		public string Name { get; set; }
		public int? Quantity { get; set; }
		public string Unit { get; set; }
		public bool IsPurchased { get; set; }

		public int ShoppingListId { get; set; }

		public int CategoryId { get; set; }
	}
}
