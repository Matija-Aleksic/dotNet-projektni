using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotNet_projektni.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "UserId", "CreatedAt", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "john@example.com", null, "john_doe" },
                    { 2, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "jane@example.com", null, "jane_smith" },
                    { 3, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "mike@example.com", null, "mike_wilson" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Groceries" },
                    { 2, "Fruits & Vegetables" },
                    { 3, "Dairy & Eggs" },
                    { 4, "Meat & Seafood" },
                    { 5, "Bakery" },
                    { 6, "Beverages" },
                    { 7, "Pantry Staples" },
                    { 8, "Frozen Foods" },
                    { 9, "Snacks" },
                    { 10, "Health & Beauty" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingLists",
                columns: new[] { "ShoppingListId", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "Essential items for the week", "Weekly Groceries", new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), 1 },
                    { 2, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "Items needed for Saturday dinner", "Dinner Party Supplies", new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), 1 },
                    { 3, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "Snacks and drinks for office", "Office Supplies", new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), 2 },
                    { 4, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "Morning essentials", "Breakfast Items", new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), 2 },
                    { 5, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "Non-perishable foods for camping", "Camping Trip", new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), 3 },
                    { 6, new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), "Organic and healthy foods", "Healthy Eating", new DateTime(2026, 4, 20, 15, 47, 43, 537, DateTimeKind.Utc).AddTicks(9242), 3 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "CategoryId", "IsPurchased", "Name", "Quantity", "ShoppingListId", "Unit" },
                values: new object[,]
                {
                    { 1, 3, false, "Milk", 2, 1, "liters" },
                    { 2, 5, false, "Bread", 1, 1, "loaf" },
                    { 3, 2, false, "Apples", 6, 1, "pieces" },
                    { 4, 4, false, "Chicken Breast", 1, 1, "kg" },
                    { 5, 2, false, "Tomatoes", 3, 1, "pieces" },
                    { 6, 6, false, "Red Wine", 2, 2, "bottles" },
                    { 7, 4, false, "Salmon Fillets", 4, 2, "pieces" },
                    { 8, 7, false, "Pasta", 2, 2, "boxes" },
                    { 9, 2, false, "Fresh Basil", 1, 2, "bunch" },
                    { 10, 3, false, "Cheese", 500, 2, "grams" },
                    { 11, 6, false, "Coffee Beans", 1, 3, "kg" },
                    { 12, 9, false, "Crackers", 3, 3, "boxes" },
                    { 13, 9, false, "Cookies", 2, 3, "boxes" },
                    { 14, 9, false, "Nuts Mix", 500, 3, "grams" },
                    { 15, 3, false, "Eggs", 12, 4, "pieces" },
                    { 16, 3, false, "Yogurt", 4, 4, "cups" },
                    { 17, 9, false, "Granola", 1, 4, "box" },
                    { 18, 6, false, "Orange Juice", 1, 4, "liter" },
                    { 19, 7, false, "Canned Beans", 6, 5, "cans" },
                    { 20, 9, false, "Trail Mix", 3, 5, "bags" },
                    { 21, 7, false, "Peanut Butter", 2, 5, "jars" },
                    { 22, 9, false, "Granola Bars", 10, 5, "pieces" },
                    { 23, 7, false, "Quinoa", 1, 6, "kg" },
                    { 24, 3, false, "Almond Milk", 2, 6, "liters" },
                    { 25, 2, false, "Spinach", 200, 6, "grams" },
                    { 26, 2, false, "Blueberries", 500, 6, "grams" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ShoppingLists",
                keyColumn: "ShoppingListId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingLists",
                keyColumn: "ShoppingListId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShoppingLists",
                keyColumn: "ShoppingListId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShoppingLists",
                keyColumn: "ShoppingListId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ShoppingLists",
                keyColumn: "ShoppingListId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ShoppingLists",
                keyColumn: "ShoppingListId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
