using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_SampleApp.Models;
using Microsoft.EntityFrameworkCore;
using MVC_SampleApp.Data;
using System.Data.Common;
using MVC_SampleApp.Models;

namespace MVC_SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductContext _context;

        public HomeController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Stats()
        {

            List<CustomerReviewGroup> groups = await _context.Customers
                .GroupBy(s => s.EnrollmentDate)
                .Select(grp => new CustomerReviewGroup()
                {
                    EnrollmentDate = grp.Key,
                    CustomerCount = grp.Count()
                })
                .OrderByDescending( grp => grp.EnrollmentDate)
                .ToListAsync();

            return View(groups);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
