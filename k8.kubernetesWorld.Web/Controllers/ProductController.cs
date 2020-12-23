using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using k8.kubernetesWorld.Web.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace k8.kubernetesWorld.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public IConfiguration Configuration { get; }
        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;

        }
        public async Task<IActionResult> Index()
        {
            string apiBase = Environment.GetEnvironmentVariable("product"); ;
            //Configuration.GetSection("AppSettings").GetSection("product").Value;
            List<Product.Product> productList = new List<Product.Product>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    $"{apiBase}" +                   
                    "/api/Product")
                    )
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productList = JsonConvert.DeserializeObject<List<Product.Product>>(apiResponse);
                }
            }
            return View(productList);
        }
    }
}