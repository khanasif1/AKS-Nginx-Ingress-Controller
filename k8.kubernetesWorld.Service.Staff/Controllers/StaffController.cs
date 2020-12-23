using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using k8.kubernetesWorld.Service.Staff;
using k8.kubernetesWorld.Service.Employee.Data;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;

namespace k8.kubernetesWorld.Service.Employee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ILogger<object> _logger;
        public IConfiguration Configuration { get; }

        public StaffController(ILogger<object> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }
        [HttpGet]
        [Route("GetMetadata")]
        public IEnumerable<string> GetMetadata()
        {
            return new string[] { "Connection",$"environment:{Environment.GetEnvironmentVariable("DefaultConnection")}" };
        }

        [HttpGet]
        public List<Staff.EFModel.Staff> Get()
        {
            List<Staff.EFModel.Staff> _response = new List<Staff.EFModel.Staff>();
            try
            {
                DbInitializer.Initialize(Environment.GetEnvironmentVariable("DefaultConnection"));
                using (SqlConnection connection = new SqlConnection(/*Configuration.GetConnectionString("DefaultConnection")*/
                    Environment.GetEnvironmentVariable("DefaultConnection")))
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");
                    using (SqlCommand command = new SqlCommand("USE staffDB; Select * from [Staff];", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _response.Add(new Staff.EFModel.Staff
                                {
                                    ID = Convert.ToInt32(reader.GetInt64(0)),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    EnrollmentDate = reader.GetDateTime(3)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _response.Add(new Staff.EFModel.Staff { _message = ex.Message.ToString() });

            }
            return _response;
        }
    }
}