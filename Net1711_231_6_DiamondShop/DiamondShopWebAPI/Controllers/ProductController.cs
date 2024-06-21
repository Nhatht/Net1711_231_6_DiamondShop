using Azure.Core;
using DiamondShopBusiness;
using DiamondShopData.ViewModel.ProductDTO;
using DiamondShopData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _business.GetAll();
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
        public async Task<IActionResult> Create([FromBody] ProductAddDTO product)
        {
            var result = await _business.Insert(product);
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
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDTO product)
        {
            var result = await _business.Update(id, product);
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
