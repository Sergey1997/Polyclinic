using Company.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models
{
    public class CountMonthForm
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public int MaterialId {get;set;}
        public Material Material { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

    }
}
