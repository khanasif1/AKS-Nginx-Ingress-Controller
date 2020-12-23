using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k8.kubernetesWorld.Web.Models
{
    public class Sales
    {
        public int sellerid { get; set; }
        public string sellername { get; set; }
        public int saleitem { get; set; }
        public string saleitemname { get; set; }
    }
}
