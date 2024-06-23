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
            string strData = await response.Content.ReadAsStringAsync();

            JArray jsonArray = JArray.Parse(strData);
            List<Order> items = jsonArray.Select(x => new Order
            {
                Id = (int)x["id"],
                Status = (string)x["status"],
                CreatedDate = DateOnly.FromDateTime((DateTime)x["createdDate"]),
                TotalPrice = (long) x["totalPrice"]
            }).ToList();
            return View(items);
        }
        public async Task<IActionResult> Details(int id)
        {
            string detailsApiUrl = ApiUrl + id;
            HttpResponseMessage response = await client.GetAsync(detailsApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                Order order = JsonConvert.DeserializeObject<Order>(strData);
                return View(order);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
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
