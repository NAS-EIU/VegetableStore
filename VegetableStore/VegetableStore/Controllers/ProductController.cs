using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VegetableStore.Interfaces;
using VegetableStore.Models.ViewModels;
using VegetableStore.Repositories;

namespace VegetableStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _productRepository;
        IBillRepository _billRepository;
        IConfiguration _configuration;

        public ProductController(IProductRepository productRepository, IBillRepository billRepository, IConfiguration configuration)
        {
            _productRepository = productRepository;
            _billRepository = billRepository;
            _configuration = configuration;
        }
        [Route("products.html")]
        public IActionResult Index(int? pageSize, string sortBy, int page = 1)
        {
            var products = new ListProductViewModel();
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");
            products.PageSize = pageSize;
            products.SortType = sortBy;
            products.Data = _productRepository.GetAllPaging("", page, pageSize.Value);
            return View(products);
        }
        [Route("search.html")]
        public IActionResult Search(string keyword, int? pageSize, string sortBy, int page = 1)
        {           
            if (keyword != "")
            {
                var result = new SearchResultViewModel();
                if (pageSize == null)
                    pageSize = _configuration.GetValue<int>("PageSize");

                result.PageSize = pageSize;
                result.SortType = sortBy;
                result.Data = _productRepository.GetAllPaging(keyword, page, pageSize.Value);
                result.Keyword = keyword;

                return View(result);
            }
            return View();
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _productRepository.GetById(id);

            return new OkObjectResult(model);
        }
    }
}