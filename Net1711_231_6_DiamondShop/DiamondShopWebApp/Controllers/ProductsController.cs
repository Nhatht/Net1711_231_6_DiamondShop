using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiamondShopData.Models;
using DiamondShopData;
using DiamondShopData.Repository;
using DiamondShopBusiness;
using Newtonsoft.Json;
using DiamondShopData.ViewModel.ProductDTO;
using System.Text;


namespace DiamondShopWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Net17112316DiamondShopContext _context;
        //private readonly UnitOfWork _unitOfWork;
        private readonly ProductBusiness _productBusiness;
        private string apiUrl = "https://localhost:7056/api/Product/";
        public ProductsController()
        {

        }
        public IActionResult index()
        {
            return View();
        }
        [HttpGet]
        public async Task<List<Product>> GetAll()
        {
            try
            {
                var result = new List<Product>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Product>>(content);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("Add", new Product());
        }
        [HttpPost]
        public async Task<Product> Create([FromBody] Product product)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync($"{apiUrl}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var createdBooking = JsonConvert.DeserializeObject<Product>(responseContent);
                            return createdBooking;
                        }
                        else
                        {
                            throw new Exception($"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync($"{apiUrl}{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<Product> Update(int id, [FromBody] ProductUpdateDTO product)
        {

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"{apiUrl}{id}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var updatedProduct = JsonConvert.DeserializeObject<Product>(responseContent);
                            return updatedProduct;
                        }
                        else
                        {
                            throw new Exception($"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ProductUpdateDTO result = null;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiUrl}{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<ProductUpdateDTO>(content);
                        }
                    }
                }

                return PartialView("edit", result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
