using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {


        private readonly ICoverTypeRepository _coverTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CoverTypeController(IUnitOfWork context)
        {
            _unitOfWork = context;
            _coverTypeRepository = _unitOfWork.CoverType;
        }

        /// <summary>
        /// pass Categories to index view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_coverTypeRepository.GetAll());
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
        public IActionResult Create(CoverType coverType)
        {

            if (_coverTypeRepository.GetFirstOrDefault(c => c.Name == coverType.Name) != null)
            {
                ModelState.AddModelError("uniquename", "Deze kaft soort bestaat al");
            }

            if (ModelState.IsValid)
            {
                _coverTypeRepository.Add(coverType);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = "kaft succesvol aangemaakt";
                }
                catch (Exception ex)
                {
                    ViewBag.error = "Er is een probleem met de database!";
                    return View(coverType);
                }
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CoverType coverType = _coverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        /// <summary>
        /// Validates token to protect agianst XSS
        /// then checks if model is valid if so add it to the databse else return create view
        /// after creation redirect to index
        /// modelstate.isvalid checks the model rules
        /// </summary>
        /// <param name="coverType"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {

            if (ModelState.IsValid)
            {
                _coverTypeRepository.Update(coverType);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = "Categorie succesvol Gewijzigd";
                }
                catch (Exception ex)
                {
                    ViewBag.error = "Er is een probleem met de database!";
                    return View(coverType);
                }
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CoverType coverType = _coverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(CoverType coverType)
        {

            _coverTypeRepository.Remove(coverType);
            try
            {
                _unitOfWork.Save();
                TempData["result"] = "Categorie succesvol verwijderd";
            }
            catch (Exception ex)
            {
                ViewBag.error = "Er is een probleem met de database!";
                return View(coverType);
            }
            return RedirectToAction("Index");

        }
    }
}
