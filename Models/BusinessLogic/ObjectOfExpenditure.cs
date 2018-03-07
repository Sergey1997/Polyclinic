using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models.BusinessLogic
{
    public class ObjectOfExpenditure
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int Amount { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public string UserId { get; set; }
       
    }
}
