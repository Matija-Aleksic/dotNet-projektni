using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }

		public ICollection<Item> ShoppingListItems { get; set; }
	}

}
