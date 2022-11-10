using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {


        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CategoryController(IUnitOfWork context)
        {
            _unitOfWork = context;
            _categoryRepository = _unitOfWork.Category;
        }

        /// <summary>
        /// pass Categories to index view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_categoryRepository.GetAll());
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

            if (_categoryRepository.GetFirstOrDefault(c => c.Name == category.Name) != null)
            {
                ModelState.AddModelError("uniquename", "Deze categorienaam bestaat al");
            }

            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = "Categorie succesvol aangemaakt";
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

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
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
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Naam en Volgnummer mogen niet hetzelfde zijn");
            }

            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = "Categorie succesvol Gewijzigd";
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

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Category category)
        {

            _categoryRepository.Remove(category);
            try
            {
                _unitOfWork.Save();
                TempData["result"] = "Categorie succesvol verwijderd";
            }
            catch (Exception ex)
            {
                ViewBag.error = "Er is een probleem met de database!";
                return View(category);
            }
            return RedirectToAction("Index");

        }
    }
}
