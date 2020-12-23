using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using k8.kubernetesWorld.Service.Product.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace k8.kubernetesWorld.Service.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<object> _logger;
        public IConfiguration Configuration { get; }
        public ProductController(ILogger<object> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }
        [HttpGet]
        [Route("GetMetadata")]
        public IEnumerable<string> GetMetadata()
        {
            return new string[] { "Connection", $"environment:{Environment.GetEnvironmentVariable("DefaultConnection")}" };
        }
        [HttpGet]
        public List<EFModel.Product> Get()
        {
            List<EFModel.Product> _response = new List<EFModel.Product>();
            try
            {
                DbInitializer.Initialize(Environment.GetEnvironmentVariable("DefaultConnection"));
                using (SqlConnection connection = new SqlConnection(/*Configuration.GetConnectionString("DefaultConnection")*/
                    Environment.GetEnvironmentVariable("DefaultConnection")))
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");
                    using (SqlCommand command = new SqlCommand("USE productDB; Select * from [Product];", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _response.Add(new EFModel.Product
                                {
                                    ID = Convert.ToInt32(reader.GetInt64(0)),
                                    Name = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    EnrollmentDate = reader.GetDateTime(3)
                                });
                            }
                        }
                    }
                }            
            }
            catch (Exception ex)
            {
                _response.Add(new EFModel.Product { _message = ex.Message.ToString() });

            }
            return _response;
        }
    }
}