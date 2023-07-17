using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var obj = _db.Products.FirstOrDefault(u => u.Id == product.Id);
            if (obj != null)
            {
                obj.Title = product.Title;
                obj.Description = product.Description;
                obj.Price = product.Price;
                obj.ListPrice = product.ListPrice;
                obj.Price50 = product.Price50;
                obj.Price100 = product.Price100;
                obj.ISBN = product.ISBN;
                obj.Author = product.Author;
                obj.CategoryId = product.CategoryId;
                obj.CoverId = product.CoverId;

                if (product.ImageUrl != null)
                {
                    obj.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
