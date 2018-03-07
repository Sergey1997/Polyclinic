using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Models;
using Company.Data;

namespace Company.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(CompanyContext db)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
