using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models.BusinessLogic
{
    public class Material
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Provider> Providers { get; set; }
        //Added test commit.
    }
}
