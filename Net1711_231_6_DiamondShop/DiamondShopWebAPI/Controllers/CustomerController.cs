using DiamondShopBusiness;
using DiamondShopData.Models;
using DiamondShopData.ViewModel.CustomerDTO;
using Microsoft.AspNetCore.Mvc;

namespace DiamondShopWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBusiness _business;

        public CustomerController()
        {
            _business = new CustomerBusiness();
        }
        [HttpGet]
        [Route("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var result = await _business.GetAllCustomer();
            if (result != null && result.Status > 0)
            {
                var customer = result.Data as List<Customer>;
                return Ok(customer);
            }
            else
            {
                return NotFound(result?.Message);
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
        [Route("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _business.GetById(id);
            if (result != null && result.Status > 0)
            {

                return Ok(result.Data);
            }
            else
            {
                return NotFound(result?.Message);
            }
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO createCustomerDTO)
        {
            var customer = new Customer()
            {
                Username = createCustomerDTO.Username,
                Name = createCustomerDTO.Name,
                Password = createCustomerDTO.Password,
                Gender = createCustomerDTO.Gender,
                Email = createCustomerDTO.Email,
                PhoneNumber = createCustomerDTO.PhoneNumber,
                Address = createCustomerDTO.Address,
                Role = "Customer",
                IsDeleted = false
            };
            var result = await _business.CreateCustomer(customer);

            if (result != null && result.Status > 0)
            {
                //var cus = result.Data as Customer;
                return Ok(result);
            }
            else
            {
                return NotFound(result?.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _business.DeleteCustomer(id);
            if (deletedUser == null)
            {
                return NotFound(deletedUser);
            }

            return Ok(deletedUser);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCustomerDTO com)
        {
            var objectToUpdate = (Customer)_business.GetById(id).Result.Data;

            objectToUpdate.Username = com.Username;
            objectToUpdate.Name = com.Name;
            objectToUpdate.Password = com.Password;
            objectToUpdate.Gender = com.Gender;
            objectToUpdate.Email = com.Email;
            objectToUpdate.PhoneNumber = com.PhoneNumber;
            objectToUpdate.Address = com.Address;
            objectToUpdate.Role = com.Role;

            var result = await _business.Update(objectToUpdate);
            if (result != null && result.Status > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result?.Message);
            }
        }
    }
}
