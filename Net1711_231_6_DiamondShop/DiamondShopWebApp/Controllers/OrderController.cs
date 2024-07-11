using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using Newtonsoft.Json.Linq;
using DiamondShopWebApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using DiamondShopData.Models;
using System.Text;
using DiamondShopData.ViewModel;
using DiamondShopData.ViewModel.OrderDTO;

namespace DiamondShopWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient client = null;
        private string ApiUrl = "";

        public OrderController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7056/api/Order/";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl + "GetAll");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                List<OrderDTO> orders = JsonConvert.DeserializeObject<List<OrderDTO>>(strData);
                return View(orders);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            string detailsApiUrl = ApiUrl + $"GetOrderProduct{id}";
            HttpResponseMessage response = await client.GetAsync(detailsApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                List<OrderProductDTO> orderProducts = JsonConvert.DeserializeObject<List<OrderProductDTO>>(strData);
                return View(orderProducts);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl + id);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                OrderDTO order = JsonConvert.DeserializeObject<OrderDTO>(strData);
                return View(order);
            }
            return NotFound();
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Status")] OrderDTO order)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { status = order.Status }), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{ApiUrl}{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An error occurred while updating the order status.");
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            /*var jsonContent = new StringContent(JsonConvert.SerializeObject(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ApiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }*/
            return View();
        }
    }
}
