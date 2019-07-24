using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegetableStore.Models.ViewModels;

namespace VegetableStore.Interfaces
{
    public interface IProductRepository: IDisposable
    {
        List<ProductViewModel> GetAll();
        List<ProductQuantityViewModel> GetQuantities(int productId);
        ProductViewModel Add(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        ProductViewModel GetById(int id);

        void ImportExcel(string filePath, int categoryId);


        void Save();

        void AddImages(int productId, string[] images);

        List<ProductImageViewModel> GetImages(int productId);
        List<ProductViewModel> GetLastest(int top);

        List<ProductViewModel> GetHotProduct(int top);
        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);
    }
}
