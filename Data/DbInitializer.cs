using Company.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Data
{
    public class DbInitializer
    {
        public static void Initialize(CompanyContext context)
        {
            if (context.Materials.Any())
            {
                return;
            }
            if (context.Providers.Any())
            {
                return;
            }

            context.Materials.Add(new Material { Name = "Ручки" });

            context.Providers.Add(new Provider { Name = "ОАО Вишенка "});

            context.SaveChanges();

        }
    }
}
