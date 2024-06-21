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
using System.Net.Http.Headers;


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
            return PartialView("Add", new ProductAddDTO());
        }
        //[HttpPost]
        //public async Task<Product> Create([FromForm] ProductAddDTO product)
        //{
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            var json = JsonConvert.SerializeObject(product);
        //            var content = new StringContent(json, Encoding.UTF8, "application/json");
        //            using (var response = await httpClient.PostAsync($"{apiUrl}", content))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var responseContent = await response.Content.ReadAsStringAsync();
        //                    var createdBooking = JsonConvert.DeserializeObject<Product>(responseContent);
        //                    return createdBooking;
        //                }
        //                else
        //                {
        //                    throw new Exception($"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductAddDTO product)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(product.DiamondId.ToString()), nameof(product.DiamondId));
                    content.Add(new StringContent(product.Name), nameof(product.Name));
                    content.Add(new StringContent(product.Description), nameof(product.Description));
                    content.Add(new StringContent(product.Metal), nameof(product.Metal));
                    content.Add(new StringContent(product.Price.ToString()), nameof(product.Price));
                    content.Add(new StringContent(product.Cost.ToString()), nameof(product.Cost));
                    content.Add(new StringContent(product.Stock.ToString()), nameof(product.Stock));
                    content.Add(new StringContent(product.Size.ToString()), nameof(product.Size));

                    if (product.ImageUrl != null)
                    {
                        var fileContent = new StreamContent(product.ImageUrl.OpenReadStream());
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(product.ImageUrl.ContentType);
                        content.Add(fileContent, nameof(product.ImageUrl), product.ImageUrl.FileName);
                    }

                    using (var response = await httpClient.PostAsync($"{apiUrl}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var createdProduct = JsonConvert.DeserializeObject<Product>(responseContent);
                            return Ok(createdProduct);
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
        //[HttpPut]
        //public async Task<Product> Update(int id, [FromBody] ProductUpdateDTO product)
        //{

        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            var json = JsonConvert.SerializeObject(product);
        //            var content = new StringContent(json, Encoding.UTF8, "application/json");
        //            using (var response = await httpClient.PutAsync($"{apiUrl}{id}", content))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var responseContent = await response.Content.ReadAsStringAsync();
        //                    var updatedProduct = JsonConvert.DeserializeObject<Product>(responseContent);
        //                    return updatedProduct;
        //                }
        //                else
        //                {
        //                    throw new Exception($"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Internal server error: {ex.Message}");
        //    }
        //}
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDTO product)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(product.DiamondId.ToString()), nameof(product.DiamondId));
                    content.Add(new StringContent(product.Name), nameof(product.Name));
                    content.Add(new StringContent(product.Description), nameof(product.Description));
                    content.Add(new StringContent(product.Metal), nameof(product.Metal));
                    content.Add(new StringContent(product.Price.ToString()), nameof(product.Price));
                    content.Add(new StringContent(product.Cost.ToString()), nameof(product.Cost));
                    content.Add(new StringContent(product.Stock.ToString()), nameof(product.Stock));
                    content.Add(new StringContent(product.Size.ToString()), nameof(product.Size));

                    if (product.ImageUrl != null)
                    {
                        var fileContent = new StreamContent(product.ImageUrl.OpenReadStream());
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(product.ImageUrl.ContentType);
                        content.Add(fileContent, nameof(product.ImageUrl), product.ImageUrl.FileName);
                    }

                    using (var response = await httpClient.PutAsync($"{apiUrl}{id}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var updatedProduct = JsonConvert.DeserializeObject<Product>(responseContent);
                            return Ok(updatedProduct);
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
