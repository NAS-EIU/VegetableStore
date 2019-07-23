using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegetableStore.Data;
using VegetableStore.Interfaces;
using VegetableStore.Models;
using VegetableStore.Models.ViewModels;

namespace VegetableStore.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _applicationDb;
        public ProductRepository(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }
        public Product GetProductById(int ProductId)
        {
            return _applicationDb.Products.SingleOrDefault(p => p.ProductId == ProductId);
        }
        public void AddProduct(ProductViewModel product)
        {
            var p = _applicationDb.Products.SingleOrDefault(x => x.ProductId == product.ProductId);
            if (p == null)
            {
                _applicationDb.Products.Add(new Product()
                {
                    ProductId=product.ProductId,
                    Name=product.Name,
                    Description=product.Description,
                    ImageUrl=product.ImageUrl,
                    Price=product.Price
                });
                _applicationDb.SaveChanges();
            }
        }
        public void EditProduct(int id,ProductViewModel product)
        {
            var p = _applicationDb.Products.SingleOrDefault(x => x.ProductId == id);
            if (p != null)
            {
                p.Name = product.Name;
                p.Description = product.Description;
                p.Price = product.Price;
                p.ImageUrl = product.ImageUrl;
                _applicationDb.Products.Update(p);
                _applicationDb.SaveChanges();
            }
        }
        public void RemoveProduct(int id)
        {
            var p = _applicationDb.Products.SingleOrDefault(x => x.ProductId == id);
            if (p != null)
            {
                _applicationDb.Products.Remove(p);
                _applicationDb.SaveChanges();
            }
        }
        public IEnumerable<Product> GetProducts()
        {
            return _applicationDb.Products.ToList();
        }
    }
}
