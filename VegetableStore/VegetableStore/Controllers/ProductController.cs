using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VegetableStore.Interfaces;
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
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }
    }
}