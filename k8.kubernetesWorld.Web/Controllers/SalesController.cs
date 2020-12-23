using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using k8.kubernetesWorld.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace k8.kubernetesWorld.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        public IConfiguration Configuration { get; }
        public SalesController(ILogger<StaffController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;

        }
        public async Task<IActionResult> Index()
        {
            string apiBase = Environment.GetEnvironmentVariable("sales");
            
            List<Sales> productList = new List<Sales>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    $"{apiBase}" +                    
                    "/api/sales")
                    )
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productList = JsonConvert.DeserializeObject<List<Sales>>(apiResponse);
                }
            }
            return View(productList);
        }
    }
}