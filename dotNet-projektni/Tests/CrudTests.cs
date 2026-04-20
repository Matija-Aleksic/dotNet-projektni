using System;
using System.Collections.Generic;
using System.Linq;
using dotNet_projektni.Data;
using Microsoft.EntityFrameworkCore;
using Model;

namespace dotNet_projektni.Tests
{
    public class CrudTests
    {
        private readonly ApplicationDbContext _context;

        public CrudTests(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RunAllTests()
        {
            try
            {
                Console.WriteLine("=== COMPREHENSIVE CRUD TESTS ===\n");

                // Test Categories
                if (!await TestCategoryOperations())
                    return false;

                // Test Users
                if (!await TestUserOperations())
                    return false;

                // Test ShoppingLists
                if (!await TestShoppingListOperations())
                    return false;

                // Test Items
                if (!await TestItemOperations())
                    return false;

                Console.WriteLine("\n=== ALL TESTS PASSED SUCCESSFULLY ===");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Test Failed with Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return false;
            }
        }

        private async Task<bool> TestCategoryOperations()
        {
            Console.WriteLine("\n--- Testing Category Operations ---");

            try
            {
                // CREATE
                Console.WriteLine("✓ Testing CREATE...");
                var newCategory = new Category
                {
                    Name = "Test Category - Books"
                };
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
                var createdCategoryId = newCategory.CategoryId;
                Console.WriteLine($"  - Created Category ID: {createdCategoryId}");

                // READ
                Console.WriteLine("✓ Testing READ...");
                var readCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == createdCategoryId);
                if (readCategory == null)
                    throw new Exception("Failed to read created category");
                Console.WriteLine($"  - Read Category: {readCategory.Name}");

                // UPDATE
                Console.WriteLine("✓ Testing UPDATE...");
                readCategory.Name = "Updated Test Category - Books & Media";
                _context.Categories.Update(readCategory);
                await _context.SaveChangesAsync();
                var updatedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == createdCategoryId);
                if (updatedCategory?.Name != "Updated Test Category - Books & Media")
                    throw new Exception("Failed to update category");
                Console.WriteLine($"  - Updated Category Name: {updatedCategory.Name}");

                // DELETE
                Console.WriteLine("✓ Testing DELETE...");
                _context.Categories.Remove(updatedCategory);
                await _context.SaveChangesAsync();
                var deletedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == createdCategoryId);
                if (deletedCategory != null)
                    throw new Exception("Failed to delete category");
                Console.WriteLine("  - Deleted Category Successfully");

                // Verify seed data is intact
                Console.WriteLine("✓ Verifying seed data...");
                var seedCategories = await _context.Categories.ToListAsync();
                if (seedCategories.Count < 10)
                    throw new Exception($"Expected at least 10 seed categories, found {seedCategories.Count}");
                Console.WriteLine($"  - Verified {seedCategories.Count} categories in database");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Category Test Failed: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TestUserOperations()
        {
            Console.WriteLine("\n--- Testing User Operations ---");

            try
            {
                // CREATE
                Console.WriteLine("✓ Testing CREATE...");
                var newUser = new User
                {
                    Username = "test_user_001",
                    Email = "testuser@example.com",
                    CreatedAt = DateTime.UtcNow
                };
                _context.ApplicationUsers.Add(newUser);
                await _context.SaveChangesAsync();
                var createdUserId = newUser.UserId;
                Console.WriteLine($"  - Created User ID: {createdUserId}, Username: {newUser.Username}");

                // READ
                Console.WriteLine("✓ Testing READ...");
                var readUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserId == createdUserId);
                if (readUser == null)
                    throw new Exception("Failed to read created user");
                Console.WriteLine($"  - Read User: {readUser.Username} ({readUser.Email})");

                // UPDATE
                Console.WriteLine("✓ Testing UPDATE...");
                readUser.Email = "newemail@example.com";
                _context.ApplicationUsers.Update(readUser);
                await _context.SaveChangesAsync();
                var updatedUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserId == createdUserId);
                if (updatedUser?.Email != "newemail@example.com")
                    throw new Exception("Failed to update user");
                Console.WriteLine($"  - Updated User Email: {updatedUser.Email}");

                // DELETE
                Console.WriteLine("✓ Testing DELETE...");
                _context.ApplicationUsers.Remove(updatedUser);
                await _context.SaveChangesAsync();
                var deletedUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserId == createdUserId);
                if (deletedUser != null)
                    throw new Exception("Failed to delete user");
                Console.WriteLine("  - Deleted User Successfully");

                // Verify seed data is intact
                Console.WriteLine("✓ Verifying seed data...");
                var seedUsers = await _context.ApplicationUsers.ToListAsync();
                if (seedUsers.Count < 3)
                    throw new Exception($"Expected at least 3 seed users, found {seedUsers.Count}");
                Console.WriteLine($"  - Verified {seedUsers.Count} users in database");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ User Test Failed: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TestShoppingListOperations()
        {
            Console.WriteLine("\n--- Testing ShoppingList Operations ---");

            try
            {
                // Get a test user
                var testUser = await _context.ApplicationUsers.FirstOrDefaultAsync();
                if (testUser == null)
                    throw new Exception("No test user found for ShoppingList operations");

                // CREATE
                Console.WriteLine("✓ Testing CREATE...");
                var newList = new ShoppingList
                {
                    Name = "Test Shopping List",
                    Description = "This is a test shopping list",
                    UserId = testUser.UserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.ShoppingLists.Add(newList);
                await _context.SaveChangesAsync();
                var createdListId = newList.ShoppingListId;
                Console.WriteLine($"  - Created ShoppingList ID: {createdListId}, Name: {newList.Name}");

                // READ
                Console.WriteLine("✓ Testing READ...");
                var readList = await _context.ShoppingLists
                    .Include(l => l.User)
                    .FirstOrDefaultAsync(l => l.ShoppingListId == createdListId);
                if (readList == null)
                    throw new Exception("Failed to read created shopping list");
                Console.WriteLine($"  - Read ShoppingList: {readList.Name} (User: {readList.User?.Username})");

                // UPDATE
                Console.WriteLine("✓ Testing UPDATE...");
                readList.Name = "Updated Test Shopping List";
                readList.Description = "This is an updated test shopping list";
                readList.UpdatedAt = DateTime.UtcNow;
                _context.ShoppingLists.Update(readList);
                await _context.SaveChangesAsync();
                var updatedList = await _context.ShoppingLists.FirstOrDefaultAsync(l => l.ShoppingListId == createdListId);
                if (updatedList?.Name != "Updated Test Shopping List")
                    throw new Exception("Failed to update shopping list");
                Console.WriteLine($"  - Updated ShoppingList Name: {updatedList.Name}");

                // DELETE
                Console.WriteLine("✓ Testing DELETE...");
                _context.ShoppingLists.Remove(updatedList);
                await _context.SaveChangesAsync();
                var deletedList = await _context.ShoppingLists.FirstOrDefaultAsync(l => l.ShoppingListId == createdListId);
                if (deletedList != null)
                    throw new Exception("Failed to delete shopping list");
                Console.WriteLine("  - Deleted ShoppingList Successfully");

                // Verify seed data is intact
                Console.WriteLine("✓ Verifying seed data...");
                var seedLists = await _context.ShoppingLists.ToListAsync();
                if (seedLists.Count < 6)
                    throw new Exception($"Expected at least 6 seed shopping lists, found {seedLists.Count}");
                Console.WriteLine($"  - Verified {seedLists.Count} shopping lists in database");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ShoppingList Test Failed: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TestItemOperations()
        {
            Console.WriteLine("\n--- Testing Item Operations ---");

            try
            {
                // Get test data
                var testList = await _context.ShoppingLists.FirstOrDefaultAsync();
                var testCategory = await _context.Categories.FirstOrDefaultAsync();

                if (testList == null || testCategory == null)
                    throw new Exception("Required test data (ShoppingList or Category) not found");

                // CREATE
                Console.WriteLine("✓ Testing CREATE...");
                var newItem = new Item
                {
                    Name = "Test Item - Premium Coffee",
                    Quantity = 2,
                    Unit = "bags",
                    IsPurchased = false,
                    ShoppingListId = testList.ShoppingListId,
                    CategoryId = testCategory.CategoryId
                };
                _context.Items.Add(newItem);
                await _context.SaveChangesAsync();
                var createdItemId = newItem.ItemId;
                Console.WriteLine($"  - Created Item ID: {createdItemId}, Name: {newItem.Name}");

                // READ
                Console.WriteLine("✓ Testing READ...");
                var readItem = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == createdItemId);
                if (readItem == null)
                    throw new Exception("Failed to read created item");
                Console.WriteLine($"  - Read Item: {readItem.Name} (Qty: {readItem.Quantity} {readItem.Unit})");

                // UPDATE
                Console.WriteLine("✓ Testing UPDATE...");
                readItem.Name = "Updated Test Item - Premium Coffee Beans";
                readItem.Quantity = 3;
                readItem.IsPurchased = true;
                _context.Items.Update(readItem);
                await _context.SaveChangesAsync();
                var updatedItem = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == createdItemId);
                if (updatedItem?.Name != "Updated Test Item - Premium Coffee Beans" || updatedItem.IsPurchased != true)
                    throw new Exception("Failed to update item");
                Console.WriteLine($"  - Updated Item: {updatedItem.Name}, Purchased: {updatedItem.IsPurchased}");

                // DELETE
                Console.WriteLine("✓ Testing DELETE...");
                _context.Items.Remove(updatedItem);
                await _context.SaveChangesAsync();
                var deletedItem = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == createdItemId);
                if (deletedItem != null)
                    throw new Exception("Failed to delete item");
                Console.WriteLine("  - Deleted Item Successfully");

                // Verify seed data is intact
                Console.WriteLine("✓ Verifying seed data...");
                var seedItems = await _context.Items.ToListAsync();
                if (seedItems.Count < 26)
                    throw new Exception($"Expected at least 26 seed items, found {seedItems.Count}");
                Console.WriteLine($"  - Verified {seedItems.Count} items in database");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Item Test Failed: {ex.Message}");
                return false;
            }
        }
    }
}

