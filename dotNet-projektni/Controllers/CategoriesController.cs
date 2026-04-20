using Microsoft.AspNetCore.Mvc;
using Model;
using dotNet_projektni.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNet_projektni.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<CategoriesController> _logger;

		public CategoriesController(ApplicationDbContext context, ILogger<CategoriesController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			var categories = await _context.Categories.ToListAsync();
			return View(categories);
		}

		[Route("Categories/Details/{id}")]
		public async Task<IActionResult> Details(int id)
		{
			var category = await _context.Categories
				.Include(c => c.ShoppingListItems)
				.FirstOrDefaultAsync(c => c.CategoryId == id);

			if (category == null)
				return NotFound();

			return View(category);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name")] Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Add(category);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
				return NotFound();

			var category = await _context.Categories.FindAsync(id);
			if (category == null)
				return NotFound();

			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] Category category)
		{
			if (id != category.CategoryId)
				return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(category);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CategoryExists(category.CategoryId))
						return NotFound();
					throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		private bool CategoryExists(int id)
		{
			return _context.Categories.Any(e => e.CategoryId == id);
		}
	}
}

