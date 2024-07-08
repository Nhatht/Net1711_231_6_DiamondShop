using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiamondShopData.Models;
using DiamondShopData;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Azure;
using DiamondShopData.ViewModel.DiamondDTO;
using DiamondShopData.ViewModel;

namespace DiamondShopWebApp.Controllers
{
    public class DiamondsController : Controller
    {
        private string apiUrl = "https://localhost:7056/api/Diamond/";
        public DiamondsController() 
        {

        }

        //private readonly Net17112316DiamondShopContext _context;
        [HttpGet]
        public IActionResult index()
        {
            return View();
        }
        [HttpGet]
        public async Task<PageableResponseDTO<Diamond>> GetAll(int pageNumber = 1, int pageSize = 10, string? query = null)
        {
            try
            {
                var result = new PageableResponseDTO<Diamond>();
                using (var httpClient = new HttpClient())
                {
                    string apiEndpoint = apiUrl + $"GetAll?pageNumber={pageNumber}&pageSize={pageSize}";
                    if (!string.IsNullOrEmpty(query))
                    {
                        apiEndpoint += $"&query={Uri.EscapeDataString(query)}";
                    }
                    using (var response = await httpClient.GetAsync(apiEndpoint))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<PageableResponseDTO<Diamond>>(content);
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
            return PartialView("add", new DiamondDTO());
        }
        [HttpPost]
        public async Task<IActionResult> AddDiamond(DiamondDTO diamondDTO)
        {
            try
            {
                // Gửi dữ liệu DiamondDTO đến API
                var json = JsonConvert.SerializeObject(diamondDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(apiUrl + "AddDiamond", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Json(new { status = 1, message = "Save successful!", data = result });
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        return Json(new { status = 0, message = "Save failed!", error = error });
                    }
                }
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có lỗi xảy ra trong quá trình gửi dữ liệu đến API
                return Json(new { status = 0, message = "An error occurred while saving the diamond.", error = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync($"{apiUrl}Delete/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Delete successful!" });
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        return Json(new { status = 0, message = "Delete failed!", error = error });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = "An error occurred while deleting the diamond.", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + id);
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var diamond = JsonConvert.DeserializeObject<DiamondDTO>(responseString);

                return PartialView("edit", diamond);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] DiamondDTO diamondDto)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(diamondDto);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Sử dụng apiUrl để tạo URL của API
                    var response = await httpClient.PutAsync($"{apiUrl}Update/{id}", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Update successful!" });
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        return Json(new { status = 0, message = "Update failed!", error = error });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = "An error occurred while updating the diamond.", error = ex.Message });
            }
        }



    }
}
