using CultureInfoDetails1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CultureInfoDetails1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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


        public IActionResult CultureDetails()
        {
            var cultures = new List<CultureSpecification>();
            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures).ToList();
            foreach (var culture in allCultures)
            {
                var cultureSpec = new CultureSpecification { 
                    CultureCode= culture.Name,
                    Name=culture.DisplayName,
                    DecimalSeparator=culture.NumberFormat.CurrencyDecimalSeparator,
                    GroupSeparator=culture.NumberFormat.CurrencyGroupSeparator,
                    DisplayName=culture.DisplayName,
                    DecimalSeparatorUnicode= culture.NumberFormat.CurrencyDecimalSeparator.Select(t => $"\\u{Convert.ToUInt16(t):X4} ").FirstOrDefault(),
                    GroupSeparatorUnicode= culture.NumberFormat.CurrencyGroupSeparator.Select(t => $"\\u{Convert.ToUInt16(t):X4} ").FirstOrDefault()
                };
                cultures.Add(cultureSpec);
            }
            ViewBag.searchCode = "";
            return View(cultures);

        }

        [HttpPost]
        public IActionResult CultureDetails(string searchCode)
        {
            var cultures = new List<CultureSpecification>();
            IEnumerable<CultureInfo> allCultures = new List<CultureInfo>();
            if (!string.IsNullOrEmpty(searchCode))
            {
                allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures).ToList().Where(a => a.Name.ToLower().Contains(searchCode.ToLower()));
            }
            else { 
                allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures).ToList();
            }
            foreach (var culture in allCultures)
            {
                var cultureSpec = new CultureSpecification
                {
                    CultureCode = culture.Name,
                    Name = culture.DisplayName,
                    DecimalSeparator = culture.NumberFormat.CurrencyDecimalSeparator,
                    GroupSeparator = culture.NumberFormat.CurrencyGroupSeparator,
                    DisplayName = culture.DisplayName,
                    DecimalSeparatorUnicode = culture.NumberFormat.CurrencyDecimalSeparator.Select(t => $"\\u{Convert.ToUInt16(t):X4} ").FirstOrDefault(),
                    GroupSeparatorUnicode = culture.NumberFormat.CurrencyGroupSeparator.Select(t => $"\\u{Convert.ToUInt16(t):X4} ").FirstOrDefault()
                };
                cultures.Add(cultureSpec);
            }
            ViewBag.searchCode = searchCode;
            return View(cultures);

        }
    }


}
