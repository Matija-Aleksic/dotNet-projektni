using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace dotNet_projektni.Controllers
{
	public class ListController : Controller
	{
        public IActionResult Index()
        {
            User testUser = new User
            {
                UserId = 1,
                Username = "testuser"
            };
            var testLists = new List<ShoppingList>
            {
                new ShoppingList
                {
                    UserId = 1,
                    User = testUser,
                    ShoppingListId = 1,
                    Name = "Weekly Groceries",
                    Description = "A list of items to buy for the week",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ShoppingListItems = new List<Item>
                    {
                        new Item { ItemId = 1, Name = "Apples", Quantity = 5, Unit = "pcs", IsPurchased = false, CategoryId = 1 },
                        new Item { ItemId = 2, Name = "Milk", Quantity = 2, Unit = "liters", IsPurchased = false, CategoryId = 2 },
                        new Item { ItemId = 3, Name = "Bread", Quantity = 1, Unit = "loaf", IsPurchased = false, CategoryId = 3 },
                        new Item { ItemId = 4, Name = "Chicken Breast", Quantity = 1, Unit = "kg", IsPurchased = false, CategoryId = 4 },
                        new Item { ItemId = 5, Name = "Orange Juice", Quantity = 1, Unit = "liter", IsPurchased = false, CategoryId = 5 }
                    }
                },
                new ShoppingList
                {
                    UserId = 1,
                    User = testUser,

                    ShoppingListId = 2,
                    Name = "Party Supplies",
                    Description = "Items needed for the weekend party",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ShoppingListItems = new List<Item>
                    {
                        new Item { ItemId = 6, Name = "Chips", Quantity = 3, Unit = "bags", IsPurchased = false, CategoryId = 6 },
                        new Item { ItemId = 7, Name = "Soda", Quantity = 6, Unit = "bottles", IsPurchased = false, CategoryId = 5 },
                        new Item { ItemId = 8, Name = "Cake", Quantity = 1, Unit = "pcs", IsPurchased = false, CategoryId = 3 },
                        new Item { ItemId = 9, Name = "Plastic Cups", Quantity = 50, Unit = "pcs", IsPurchased = false, CategoryId = 8 },
                        new Item { ItemId = 10, Name = "Napkins", Quantity = 2, Unit = "packs", IsPurchased = false, CategoryId = 8 }
                    }
                },
                new ShoppingList
                {
                    UserId = 1,
                    User = testUser,

                    ShoppingListId = 3,
                    Name = "Office Supplies",
                    Description = "Supplies needed for the office",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ShoppingListItems = new List<Item>
                    {
                        new Item { ItemId = 11, Name = "Printer Paper", Quantity = 5, Unit = "reams", IsPurchased = false, CategoryId = 8 },
                        new Item { ItemId = 12, Name = "Pens", Quantity = 10, Unit = "pcs", IsPurchased = false, CategoryId = 8 },
                        new Item { ItemId = 13, Name = "Staples", Quantity = 1, Unit = "box", IsPurchased = false, CategoryId = 8 },
                        new Item { ItemId = 14, Name = "Paper Clips", Quantity = 100, Unit = "pcs", IsPurchased = false, CategoryId = 8 },
                        new Item { ItemId = 15, Name = "Notebooks", Quantity = 3, Unit = "pcs", IsPurchased = false, CategoryId = 8 }
                    }
                }
            };

            return View(testLists);
        }
        [Route("List/Details/{id}")]  
        public IActionResult DetailedList(int id)
        {
            User testUser = new User
            {
                UserId = 1,
                Username = "testuser"
            };
            var ShoppingList = new ShoppingList
            {
                User = testUser,
                ShoppingListId = 1,
                Name = "Weekly Groceries",
                Description = "A list of items to buy for the week",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ShoppingListItems = new List<Item>
                {
                    new Item { ItemId = 1, Name = "Apples", Quantity = 5, Unit = "pcs", IsPurchased = false, CategoryId = 1 },
                    new Item { ItemId = 2, Name = "Milk", Quantity = 2, Unit = "liters", IsPurchased = false, CategoryId = 2 },
                    new Item { ItemId = 3, Name = "Bread", Quantity = 1, Unit = "loaf", IsPurchased = false, CategoryId = 3 },
                    new Item { ItemId = 4, Name = "Chicken Breast", Quantity = 1, Unit = "kg", IsPurchased = false, CategoryId = 4 },
                    new Item { ItemId = 5, Name = "Orange Juice", Quantity = 1, Unit = "liter", IsPurchased = false, CategoryId = 5 }
                }
            };
        
            return View(ShoppingList);
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}
