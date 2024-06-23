using DiamondShopBusiness;
using DiamondShopData.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DiamondShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderBusiness _business;
        private readonly OrderProductBusiness business;
        public OrderController()
        {
            _business = new OrderBusiness();
            business = new OrderProductBusiness();
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
        [Route("GetOrderProduct{id}")]
        public async Task<IActionResult> GetOrderProduct(int id)
        {
            var result = await business.GetByOrderId(id);
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
        public async Task<IActionResult> Create([FromBody] List<ProductsDTO> products)
        {
            var result = await _business.Insert(products);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDTO dto)
        {
            var result = await _business.Update(id,dto);
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
