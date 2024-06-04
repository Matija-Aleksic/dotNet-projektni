using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Model
{
	public class User
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string? Email { get; set; }
		public string? PasswordHash { get; set; }
		public DateTime? CreatedAt { get; set; }
		public ICollection<ShoppingList>? ShoppingLists { get; set; }
	}

}
