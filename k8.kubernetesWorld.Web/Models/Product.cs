using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k8.kubernetesWorld.Web.Product
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string _message { get; set; }

    }
}
