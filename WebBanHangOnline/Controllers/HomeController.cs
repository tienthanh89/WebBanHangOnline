using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.DbContext;
using WebBanHangOnline.Models.Entity;
using WebBanHangOnline.Models.ViewModels.DemoApi;


namespace WebBanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly WebBanHangOnlineContext _db;

        public HomeController(ILogger<HomeController> logger, WebBanHangOnlineContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> DemoApi()
        {
            // Xử lý gọi api với url là: http://localhost:5241/api/products
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5241/api/product");
            // GET dữ liệu
            var response = await client.GetAsync(client.BaseAddress);
            // Lấy response body của response
            var content = await response.Content.ReadAsStringAsync();
            // Convert dữ kiệu từ string về object
            //var products = JsonConvert.DeserializeObject<List<ProductDemoApi>>(content);
            var products = JsonConvert.DeserializeObject<List<OuputProduct>>(content);
            //var json= JsonConvert.SerializeObject(content);
            //return StatusCode(StatusCodes.Status200OK, products);
            return StatusCode(StatusCodes.Status200OK, products);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(InputProduct input)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:5241/");

            var json = JsonConvert.SerializeObject(input);

            var raw = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/product", raw);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct(Guid Id, InputProduct input)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:5241/");

            var json = JsonConvert.SerializeObject(input);

            var raw = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/product", raw);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
