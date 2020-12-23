using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k8.kubernetesWorld.Web
{
    public class Staff
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string _message { get; set; }

    }
}
