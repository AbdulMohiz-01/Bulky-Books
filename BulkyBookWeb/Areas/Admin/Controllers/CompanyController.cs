using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Net.Security;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // IWebHostEnvironment tells about the
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        // GET method for Upsert
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                // create

                return View(company);
            }
            else
            {
                // update
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
        }

        // POST method for Upsert ( Insert and update )
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                // if the id is 0 it means the user is creating the product
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company Created Successfully!!!";
                }
                else
                {
                    TempData["success"] = "Company Updated Successfully!!!";
                    _unitOfWork.Company.Update(obj);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS

        // this region will handle all the API CALLS.
        [HttpGet]
        public IActionResult GetAll()
        {
            // retrieve all the products from database
            var companies = _unitOfWork.Company.GetAll();

            // now return all the products in JSON format, we are making anonymouse type/class object in which there will only be one property called data in which all the products will be stored.
            return Json(new { data = companies });
        }

        // API CALL for delete
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while Deleting the Product" });
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!!!" });
        }
        #endregion

    }
}

