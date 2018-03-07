using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models.BusinessLogic
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
