using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using dotNet_projektni.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNet_projektni.Controllers
{
	public class ItemsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ItemsController> _logger;

		public ItemsController(ApplicationDbContext context, ILogger<ItemsController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			var items = await _context.Items.ToListAsync();
			return View(items);
		}

		[Route("Items/Details/{id}")]
		public async Task<IActionResult> Details(int id)
		{
			var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id);

			if (item == null)
				return NotFound();

			return View(item);
		}

		public async Task<IActionResult> Create(int? shoppingListId)
		{
			ViewBag.ShoppingListId = shoppingListId ?? 0;
			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name");
			ViewBag.ShoppingLists = new SelectList(await _context.ShoppingLists.ToListAsync(), "ShoppingListId", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Quantity,Unit,IsPurchased,ShoppingListId,CategoryId")] Item item)
		{
			if (ModelState.IsValid)
			{
				_context.Add(item);
				await _context.SaveChangesAsync();

				if (item.ShoppingListId > 0)
					return RedirectToAction("DetailedList", "List", new { id = item.ShoppingListId });
				
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name", item.CategoryId);
			ViewBag.ShoppingLists = new SelectList(await _context.ShoppingLists.ToListAsync(), "ShoppingListId", "Name", item.ShoppingListId);
			return View(item);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var item = await _context.Items.FindAsync(id);
			if (item == null)
				return NotFound();

			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name", item.CategoryId);
			ViewBag.ShoppingLists = new SelectList(await _context.ShoppingLists.ToListAsync(), "ShoppingListId", "Name", item.ShoppingListId);
			return View(item);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ItemId,Name,Quantity,Unit,IsPurchased,ShoppingListId,CategoryId")] Item item)
		{
			if (id != item.ItemId)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(item);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ItemExists(item.ItemId))
						return NotFound();
					throw;
				}

				if (item.ShoppingListId > 0)
					return RedirectToAction("DetailedList", "List", new { id = item.ShoppingListId });
				
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "Name", item.CategoryId);
			ViewBag.ShoppingLists = new SelectList(await _context.ShoppingLists.ToListAsync(), "ShoppingListId", "Name", item.ShoppingListId);
			return View(item);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var item = await _context.Items.FindAsync(id);
			if (item != null)
			{
				var shoppingListId = item.ShoppingListId;
				_context.Items.Remove(item);
				await _context.SaveChangesAsync();
				
				if (shoppingListId > 0)
					return RedirectToAction("DetailedList", "List", new { id = shoppingListId });
			}
			return RedirectToAction(nameof(Index));
		}

		private bool ItemExists(int id)
		{
			return _context.Items.Any(e => e.ItemId == id);
		}
	}
}

