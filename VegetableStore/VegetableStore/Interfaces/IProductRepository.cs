using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegetableStore.Models;
using VegetableStore.Models.ViewModels;

namespace VegetableStore.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductById(int ProductId);
        void AddProduct(ProductViewModel product);
        void EditProduct(int id, ProductViewModel product);
        void RemoveProduct(int id);
        IEnumerable<Product> GetProducts();
    }
}
