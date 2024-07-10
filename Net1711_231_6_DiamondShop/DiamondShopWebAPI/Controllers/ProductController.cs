using Azure.Core;
using DiamondShopBusiness;
using DiamondShopData.ViewModel.ProductDTO;
using DiamondShopData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;


namespace DiamondShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductBusiness _business;
        
        public ProductController()
        {
            _business = new ProductBusiness();
        }
        //[HttpGet]
        //public async Task<IActionResult> ProductsPage(int pageNumber = 1, int pageSize = 10)
        //{
        //    var result = await _business.GetProductsPageAsync(pageNumber, pageSize);
        //    return Ok(result);
        //}
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 2, string? query = null)
        {
            var result = await _business.GetAll(pageNumber, pageSize, query);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _business.GetById(id);
            if (result != null && result.Status > 0)
            {

                return Ok(result.Data);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromForm] ProductAddDTO product)
        {
            string url = "";
            if (product.ImageUrl != null)
            {
                string fileName = product.Name + Path.GetExtension(product.ImageUrl.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    product.ImageUrl.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                url = baseUrl + "/ProductImage/" + fileName;
            }
            else
            {
                url = "https://placehold.co/600x400";
            }
            var result = await _business.Insert(product, url);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDTO product)
        {
            string url = "";
            if (product.ImageUrl != null)
            {
                string fileName = product.Name + Path.GetExtension(product.ImageUrl.FileName);
                string filePath = @"wwwroot\ProductImage\" + fileName;

                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo file = new FileInfo(directoryLocation);

                if (file.Exists)
                {
                    file.Delete();
                }

                using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
                {
                    product.ImageUrl.CopyTo(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                url = baseUrl + "/ProductImage/" + fileName;
            }
            var result = await _business.Update(id, product,url);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result);
            }
        }
        //[HttpDelete]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(int id, [FromQuery] bool? isDeleted)
        //{
        //    var result = await _business.UpdateIsDelete(id, isDeleted);
        //    if (result != null && result.Status > 0)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound(result);
        //    }
        //}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _business.Delete(id);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
