using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace dotNet_projektni.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<ShoppingList> ShoppingLists { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<User> ApplicationUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ShoppingList>()
				.HasOne(s => s.User)
				.WithMany(u => u.ShoppingLists)
				.HasForeignKey(s => s.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Item>()
				.HasOne<ShoppingList>()
				.WithMany(s => s.ShoppingListItems)
				.HasForeignKey(i => i.ShoppingListId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Item>()
				.HasOne<Category>()
				.WithMany(c => c.ShoppingListItems)
				.HasForeignKey(i => i.CategoryId)
				.OnDelete(DeleteBehavior.SetNull);

			SeedData(builder);
		}

		private void SeedData(ModelBuilder builder)
		{
			var now = DateTime.UtcNow;

			var categories = new Category[]
			{
				new Category { CategoryId = 1, Name = "Groceries" },
				new Category { CategoryId = 2, Name = "Fruits & Vegetables" },
				new Category { CategoryId = 3, Name = "Dairy & Eggs" },
				new Category { CategoryId = 4, Name = "Meat & Seafood" },
				new Category { CategoryId = 5, Name = "Bakery" },
				new Category { CategoryId = 6, Name = "Beverages" },
				new Category { CategoryId = 7, Name = "Pantry Staples" },
				new Category { CategoryId = 8, Name = "Frozen Foods" },
				new Category { CategoryId = 9, Name = "Snacks" },
				new Category { CategoryId = 10, Name = "Health & Beauty" }
			};

			builder.Entity<Category>().HasData(categories);

			var users = new User[]
			{
				new User { UserId = 1, Username = "john_doe", Email = "john@example.com", CreatedAt = now },
				new User { UserId = 2, Username = "jane_smith", Email = "jane@example.com", CreatedAt = now },
				new User { UserId = 3, Username = "mike_wilson", Email = "mike@example.com", CreatedAt = now }
			};

			builder.Entity<User>().HasData(users);

			var shoppingLists = new ShoppingList[]
			{
				new ShoppingList { ShoppingListId = 1, Name = "Weekly Groceries", Description = "Essential items for the week", UserId = 1, CreatedAt = now, UpdatedAt = now },
				new ShoppingList { ShoppingListId = 2, Name = "Dinner Party Supplies", Description = "Items needed for Saturday dinner", UserId = 1, CreatedAt = now, UpdatedAt = now },
				new ShoppingList { ShoppingListId = 3, Name = "Office Supplies", Description = "Snacks and drinks for office", UserId = 2, CreatedAt = now, UpdatedAt = now },
				new ShoppingList { ShoppingListId = 4, Name = "Breakfast Items", Description = "Morning essentials", UserId = 2, CreatedAt = now, UpdatedAt = now },
				new ShoppingList { ShoppingListId = 5, Name = "Camping Trip", Description = "Non-perishable foods for camping", UserId = 3, CreatedAt = now, UpdatedAt = now },
				new ShoppingList { ShoppingListId = 6, Name = "Healthy Eating", Description = "Organic and healthy foods", UserId = 3, CreatedAt = now, UpdatedAt = now }
			};

			builder.Entity<ShoppingList>().HasData(shoppingLists);

			var items = new Item[]
			{
				new Item { ItemId = 1, Name = "Milk", Quantity = 2, Unit = "liters", IsPurchased = false, ShoppingListId = 1, CategoryId = 3 },
				new Item { ItemId = 2, Name = "Bread", Quantity = 1, Unit = "loaf", IsPurchased = false, ShoppingListId = 1, CategoryId = 5 },
				new Item { ItemId = 3, Name = "Apples", Quantity = 6, Unit = "pieces", IsPurchased = false, ShoppingListId = 1, CategoryId = 2 },
				new Item { ItemId = 4, Name = "Chicken Breast", Quantity = 1, Unit = "kg", IsPurchased = false, ShoppingListId = 1, CategoryId = 4 },
				new Item { ItemId = 5, Name = "Tomatoes", Quantity = 3, Unit = "pieces", IsPurchased = false, ShoppingListId = 1, CategoryId = 2 },
				
				new Item { ItemId = 6, Name = "Red Wine", Quantity = 2, Unit = "bottles", IsPurchased = false, ShoppingListId = 2, CategoryId = 6 },
				new Item { ItemId = 7, Name = "Salmon Fillets", Quantity = 4, Unit = "pieces", IsPurchased = false, ShoppingListId = 2, CategoryId = 4 },
				new Item { ItemId = 8, Name = "Pasta", Quantity = 2, Unit = "boxes", IsPurchased = false, ShoppingListId = 2, CategoryId = 7 },
				new Item { ItemId = 9, Name = "Fresh Basil", Quantity = 1, Unit = "bunch", IsPurchased = false, ShoppingListId = 2, CategoryId = 2 },
				new Item { ItemId = 10, Name = "Cheese", Quantity = 500, Unit = "grams", IsPurchased = false, ShoppingListId = 2, CategoryId = 3 },
				
				new Item { ItemId = 11, Name = "Coffee Beans", Quantity = 1, Unit = "kg", IsPurchased = false, ShoppingListId = 3, CategoryId = 6 },
				new Item { ItemId = 12, Name = "Crackers", Quantity = 3, Unit = "boxes", IsPurchased = false, ShoppingListId = 3, CategoryId = 9 },
				new Item { ItemId = 13, Name = "Cookies", Quantity = 2, Unit = "boxes", IsPurchased = false, ShoppingListId = 3, CategoryId = 9 },
				new Item { ItemId = 14, Name = "Nuts Mix", Quantity = 500, Unit = "grams", IsPurchased = false, ShoppingListId = 3, CategoryId = 9 },
				
				new Item { ItemId = 15, Name = "Eggs", Quantity = 12, Unit = "pieces", IsPurchased = false, ShoppingListId = 4, CategoryId = 3 },
				new Item { ItemId = 16, Name = "Yogurt", Quantity = 4, Unit = "cups", IsPurchased = false, ShoppingListId = 4, CategoryId = 3 },
				new Item { ItemId = 17, Name = "Granola", Quantity = 1, Unit = "box", IsPurchased = false, ShoppingListId = 4, CategoryId = 9 },
				new Item { ItemId = 18, Name = "Orange Juice", Quantity = 1, Unit = "liter", IsPurchased = false, ShoppingListId = 4, CategoryId = 6 },
				
				new Item { ItemId = 19, Name = "Canned Beans", Quantity = 6, Unit = "cans", IsPurchased = false, ShoppingListId = 5, CategoryId = 7 },
				new Item { ItemId = 20, Name = "Trail Mix", Quantity = 3, Unit = "bags", IsPurchased = false, ShoppingListId = 5, CategoryId = 9 },
				new Item { ItemId = 21, Name = "Peanut Butter", Quantity = 2, Unit = "jars", IsPurchased = false, ShoppingListId = 5, CategoryId = 7 },
				new Item { ItemId = 22, Name = "Granola Bars", Quantity = 10, Unit = "pieces", IsPurchased = false, ShoppingListId = 5, CategoryId = 9 },
				
				new Item { ItemId = 23, Name = "Quinoa", Quantity = 1, Unit = "kg", IsPurchased = false, ShoppingListId = 6, CategoryId = 7 },
				new Item { ItemId = 24, Name = "Almond Milk", Quantity = 2, Unit = "liters", IsPurchased = false, ShoppingListId = 6, CategoryId = 3 },
				new Item { ItemId = 25, Name = "Spinach", Quantity = 200, Unit = "grams", IsPurchased = false, ShoppingListId = 6, CategoryId = 2 },
				new Item { ItemId = 26, Name = "Blueberries", Quantity = 500, Unit = "grams", IsPurchased = false, ShoppingListId = 6, CategoryId = 2 }
			};

			builder.Entity<Item>().HasData(items);
		}
	}
}
