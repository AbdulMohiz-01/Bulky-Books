using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Cover> list = _unitOfWork.CoverType.GetAll();
            return View(list);
        }

        // GET method of Add new Cover Type.

        public IActionResult Create()
        {
            return View();
        }

        // POST method of Add new Cover Type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cover cover)
        {
            /*if (ModelState.IsValid)
            {*/
            _unitOfWork.CoverType.Add(cover);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Added Successfully!!!";
            return RedirectToAction("Index");
            /* }
             else
             {
                 return View();
             }*/
        }

        // GET method for Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cover cover = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (cover == null)
            {
                return NotFound();
            }
            return View(cover);
        }

        // POST method for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditPost(Cover cover)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(cover);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Updated Successfully!!!";

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET method for Delete

        public IActionResult Delete(int id)
        {
            Cover cover = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            _unitOfWork.CoverType.Remove(cover);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Deleted Successfully!!!";

            return RedirectToAction("Index");
        }

    }
}
