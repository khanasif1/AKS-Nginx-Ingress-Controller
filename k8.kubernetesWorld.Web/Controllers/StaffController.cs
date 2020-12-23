using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace k8.kubernetesWorld.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        public IConfiguration Configuration { get; }
        public StaffController(ILogger<StaffController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;

        }
        public async Task<IActionResult> Index()
        {
            string apiBase = Environment.GetEnvironmentVariable("staff");
                //Configuration.GetSection("AppSettings").GetSection("staff").Value;
            List<Staff> productList = new List<Staff>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(
                    $"{apiBase}" +                    
                    "/api/Staff")
                    )
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    productList = JsonConvert.DeserializeObject<List<Staff>>(apiResponse);
                }
            }
            return View(productList);
        }
    }
}