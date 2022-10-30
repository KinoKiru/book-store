using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {

        private Data.ApplicationDbContext _context { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CategoryController(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// pass Categories to index view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_context.Categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Validates token to protect agianst XSS
        /// then checks if model is valid if so add it to the databse else return create view
        /// after creation redirect to index
        /// modelstate.isvalid checks the model rules
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Naam en Volgnummer mogen niet hetzelfde zijn");
            }

            if (_context.Categories.FirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("uniquename", "Deze categorienaam bestaat al");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.error = "Er is een probleem met de database!";
                    return View(category);
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
