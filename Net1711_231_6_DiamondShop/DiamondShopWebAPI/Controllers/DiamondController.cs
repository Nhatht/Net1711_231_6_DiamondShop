using DiamondShopBusiness;
using DiamondShopData.Models;
using DiamondShopData.ViewModel.DiamondDTO;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopWebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DiamondController : ControllerBase
    {

        private readonly DiamondBusiness _business;
        public DiamondController()
        {
            _business = new DiamondBusiness();
        }
        [HttpGet]
        [Route("GetAllDiamonds")]
        public async Task<IActionResult> GetAllDiamonds()
        {
            var result = await _business.GetAllDiamonds();
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

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
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _business.GetById(id);
            if (result.Status > 0)
            {
                var diamond = result.Data as Diamond;
                return Ok(diamond);
            }
            else
            {
                return NotFound(result?.Message);
            }
        }
        [HttpPost]
        [Route("AddDiamond")]
        public async Task<IActionResult> AddDiamond(DiamondDTO diamond)
        {
            var result = await _business.Insert(diamond);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, DiamondDTO diamond)
        {
            var result = await _business.Update(id, diamond);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _business.Delete(id);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}
        
