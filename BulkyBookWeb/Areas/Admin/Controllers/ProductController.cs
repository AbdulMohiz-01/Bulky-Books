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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // IWebHostEnvironment tells about the
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        // GET method for Upsert
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                // create

                return View(productVM);
            }
            else
            {
                // update
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
        }

        // POST method for Upsert ( Insert and update )
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                // it will get the excat path of the wwwroot folder
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    // it will generate the unique name of the file like asj131-312jkasdha-1239asjdk-123jnksd
                    string fileName = Guid.NewGuid().ToString();

                    // once we get the file name then we'll get the exact path where the file should be uploaded like here it should be uploaded in wwwroot\images\products
                    var uploads = Path.Combine(wwwRootPath, @"images\products");

                    // now get the extension of the uploaded file
                    var extension = Path.GetExtension(file.FileName);

                    //now before we copy the image let's first check if there's already an image in database, if it exists then it means that the user is updating the image. So, to update image we should delete the older image
                    if (obj.Product.ImageUrl != null)
                    {
                        //Notice that Combine method do not take \images\product, it take images\product so we have to trim this first
                        var olderImage = Path.Combine(wwwRootPath, obj.Product.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(olderImage))
                        {
                            System.IO.File.Delete(olderImage);
                        }
                    }

                    // now copy that file to the desired location by using the file Stream object
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    // also update the ImageUrl in the obj
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                // if the id is 0 it means the user is creating the product
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully!!!";

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
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category,Cover");

            // now return all the products in JSON format, we are making anonymouse type/class object in which there will only be one property called data in which all the products will be stored.
            return Json(new { data = products });
        }

        // API CALL for delete
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while Deleting the Product" });
            }

            var olderImage = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(olderImage))
            {
                System.IO.File.Delete(olderImage);
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!!!" });
        }
        #endregion

    }
}

