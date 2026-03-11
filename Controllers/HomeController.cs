using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var monthlyExpenses = _context.Expenses
                .GroupBy(e => e.Date.Month)
                .Select(g => new
                {
                    MonthNumber = g.Key,
                    Total = g.Sum(e => (double)e.Amount)
                })
                .OrderBy(x => x.MonthNumber)
                .ToList()
                .Select(x => new
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat
                            .GetAbbreviatedMonthName(x.MonthNumber),
                    x.Total
                }).ToList();

            var months = monthlyExpenses.Select(x => x.Month).ToList();
            var totals = monthlyExpenses.Select(x => x.Total).ToList();

            var average = totals.Average();

            ViewBag.Months = months;
            ViewBag.Totals = totals;
            ViewBag.Average = average;

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
    }
}
