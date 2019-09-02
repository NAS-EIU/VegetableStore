using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VegetableStore.Interfaces;
using VegetableStore.Models.ViewModels;

namespace VegetableStore.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {

        private IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
     
        
        public IActionResult GetAll()
        {
            var model = _blogRepository.GetAll();
            return new OkObjectResult(model);
        }
        [Route("/admin/blog/index")]
        [HttpGet]
        public IActionResult GetAllPaging( int page, int pageSize)
        {
            var model = _blogRepository.GetAllPaging( page, pageSize);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(BlogViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (productVm.Id == 0)
                {
                    _blogRepository.Add(productVm);
                }
                else
                {
                    _blogRepository.Update(productVm);
                }
                _blogRepository.Save();
                return new OkObjectResult(productVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _blogRepository.Delete(id);
                _blogRepository.Save();

                return new OkObjectResult(id);
            }
        }

        [HttpPost]
        public IActionResult SaveImages(int blogId, string[] images)
        {
            _blogRepository.AddImages(blogId, images);
            _blogRepository.Save();
            return new OkObjectResult(images);
        }

        [HttpGet]
        public IActionResult GetImages(int blogId)
        {
            var images = _blogRepository.GetImages(blogId);
            return new OkObjectResult(images);
        }
    }
}