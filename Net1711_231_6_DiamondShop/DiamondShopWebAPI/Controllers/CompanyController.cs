using DiamondShopBusiness;
using DiamondShopData.Models;
using DiamondShopData.ViewModel.CompanyDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace DiamondShopWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyBusiness _business;
        public CompanyController()
        {
            _business = new CompanyBusiness();
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result?.Message);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _business.GetById(id);
            if (result != null && result.Status > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result?.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCompanyDTO com)
        {
            var company = new Company
            {
                Name = com.Name,
                Email = com.Email,
                Description = com.Description,
                Phone = com.Phone,
                Address = com.Address,
                Password = com.Password,
                Role = com.Role,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };
            var result = await _business.Create(company);
            if (result != null && result.Status > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result?.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCompanyDTO com)
        {
            var objectToUpdate = (Company)_business.GetById(id).Result.Data;
            objectToUpdate.Name = com.Name;
            objectToUpdate.Description = com.Description;
            objectToUpdate.Phone = com.Phone;
            objectToUpdate.Email = com.Email;
            objectToUpdate.Address = com.Address;
            objectToUpdate.Password = com.Password;
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

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var objectToDelete = _business.GetById(id).Result.Data;
            var result = await _business.Remove((Company)objectToDelete);
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