using DiamondShopBusiness;
using DiamondShopData.Models;
using DiamondShopData.ViewModel.CustomerDTO;
using DiamondShopWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DiamondShopWebApp.Controllers
{
    public class CustomersController : Controller
    {
        private string apiUrl = "https://localhost:7056/api/Customer/";

        public IActionResult index()
        {
            return View();
        }
        public CustomersController()
        {

        }
        [HttpGet]
        public async Task<List<Customer>> GetAll()
        {
            try
            {
                var result = new List<Customer>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Customer>>(content);
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
        public ActionResult Add()
        {
            return PartialView("AddCustomer", new CreateCustomerDTO());
        }

        [HttpPost]
        public async Task<IBusinessResult> Create(CreateCustomerDTO customer)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonResult = JsonConvert.SerializeObject(customer);
                    var content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "CreateCustomer", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(responseContent);
                            return result;
                        }
                        else
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            throw new Exception(errorContent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IBusinessResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync($"{apiUrl}{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            return result;
                        }
                        else
                        {
                            var error = await response.Content.ReadAsStringAsync();
                            throw new Exception(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                CustomerDTO result = null;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiUrl}{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<CustomerDTO>(content);
                        }
                    }
                }
                return PartialView("EditCustomer", result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPut]
        public async Task<Customer> Update(int id, [FromBody] CustomerDTO customerDTO)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(customerDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"{apiUrl}{id}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var updatedCustomer = JsonConvert.DeserializeObject<Customer>(responseContent);
                            return updatedCustomer;
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
    }
}
