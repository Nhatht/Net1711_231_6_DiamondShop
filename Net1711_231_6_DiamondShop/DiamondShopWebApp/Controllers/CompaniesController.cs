using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiamondShopData.Models;
using DiamondShopBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DiamondShopWebAPI.Controllers;
using System.Net.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text;

namespace DiamondShopWebApp.Controllers
{
    public class CompaniesController : Controller
    {
        private string URL = "https://localhost:7056/api/Company/";

        public CompaniesController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("add", new Company());
        }

        [HttpGet]
        public async Task<List<Company>> GetAll()
        {
            try
            {
                var result = new List<Company>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(URL + "GetAll"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var jsonResult = JObject.Parse(content);
                            var companiesArray = jsonResult["data"].ToString();
                            result = JsonConvert.DeserializeObject<List<Company>>(companiesArray);
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
        public async Task<IActionResult> Edit(int id)
        {
            using (var httpClient = new HttpClient())
            {
                //using (var response = await httpClient.GetAsync($"{URL}{id}"))
                using (var response = await httpClient.GetAsync(URL + "GetById?id=" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        var company = JsonConvert.DeserializeObject<Company>(result.Data.ToString());
                        return PartialView("add", company);
                    }
                }
            }
            return PartialView("add", new Company());
        }

        [HttpDelete]
        public async Task<IBusinessResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync($"{URL}Delete?id={id}"))
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

		[HttpPost]
		public async Task<IBusinessResult> Create(Company com)
		{
			try
			{
				using (var httpClient = new HttpClient())
				
				{
					var jsonResult = JsonConvert.SerializeObject(com);
					var content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
					if (com.Id != 0)
						using (var response = await httpClient.PutAsync(URL + "Update?id=" + com.Id, content))
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
					else
						using (var response = await httpClient.PostAsync(URL + "Create", content))
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

		//[HttpPost]
		//public async Task<IDiamondShopResult> Create(Company com)
		//{
		//	try
		//	{
		//		using (var httpClient = new HttpClient())
		//		{
		//			HttpResponseMessage response;
		//			if (com.Id != 0)
		//			{
		//				response = await httpClient.PutAsJsonAsync(URL + "Update", com);
		//			}	
		//			var 
		//			var jsonResult = JsonConvert.SerializeObject(com);
		//			var content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
		//			if (com.Name != null)
		//				using (var response = await httpClient.PutAsync(URL + "Update", content))
		//				{
		//					if (response.IsSuccessStatusCode)
		//					{
		//						var responseContent = await response.Content.ReadAsStringAsync();
		//						var result = JsonConvert.DeserializeObject<BusinessResult>(responseContent);
		//						return result;
		//					}
		//					else
		//					{
		//						var errorContent = await response.Content.ReadAsStringAsync();
		//						throw new Exception(errorContent);
		//					}
		//				}
		//			else
		//				using (var response = await httpClient.PostAsync(URL + "Create", content))
		//				{
		//					if (response.IsSuccessStatusCode)
		//					{
		//						var responseContent = await response.Content.ReadAsStringAsync();
		//						var result = JsonConvert.DeserializeObject<BusinessResult>(responseContent);
		//						return result;
		//					}
		//					else
		//					{
		//						var errorContent = await response.Content.ReadAsStringAsync();
		//						throw new Exception(errorContent);
		//					}
		//				}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		return new BusinessResult(-4, ex.Message);
		//	}
		//}
	}
}