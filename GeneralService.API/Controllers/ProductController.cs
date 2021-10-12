using GeneralService.API.DTOs.Products.Requests;
using GeneralService.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeneralService.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _productService.GetAll();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _productService.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreateProductRequest request)
        {
            var res = await _productService.Insert(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest request)
        {
            var res = await _productService.Update(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _productService.Delete(id);
            return Ok();
        }
    }
}
