using Microsoft.AspNetCore.Mvc;
using Model;
using dotNet_projektni.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNet_projektni.Controllers
{
	public class ListController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<ListController> _logger;

		public ListController(ApplicationDbContext context, ILogger<ListController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			var shoppingLists = await _context.ShoppingLists
				.Include(s => s.ShoppingListItems)
				.ToListAsync();
			return View(shoppingLists);
		}

		[Route("List/Details/{id}")]
		public async Task<IActionResult> DetailedList(int id)
		{
			var shoppingList = await _context.ShoppingLists
				.Include(s => s.ShoppingListItems)
				.FirstOrDefaultAsync(s => s.ShoppingListId == id);

			if (shoppingList == null)
				return NotFound();

			return View(shoppingList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Description")] ShoppingList shoppingList)
		{
			if (ModelState.IsValid)
			{
				shoppingList.CreatedAt = DateTime.Now;
				shoppingList.UpdatedAt = DateTime.Now;
				shoppingList.UserId = 1;
				shoppingList.ShoppingListItems ??= new List<Item>();

				_context.Add(shoppingList);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(shoppingList);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var shoppingList = await _context.ShoppingLists.FindAsync(id);
			if (shoppingList == null)
				return NotFound();

			return View(shoppingList);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ShoppingListId,Name,Description")] ShoppingList shoppingList)
		{
			if (id != shoppingList.ShoppingListId)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					shoppingList.UpdatedAt = DateTime.Now;
					_context.Update(shoppingList);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ShoppingListExists(shoppingList.ShoppingListId))
						return NotFound();
					throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(shoppingList);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var shoppingList = await _context.ShoppingLists.FindAsync(id);
			if (shoppingList != null)
			{
				_context.ShoppingLists.Remove(shoppingList);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		private bool ShoppingListExists(int id)
		{
			return _context.ShoppingLists.Any(e => e.ShoppingListId == id);
		}
	}
}
