using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

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
                    MonthNumber = x.MonthNumber,
                    Month = CultureInfo.CurrentCulture.DateTimeFormat
                            .GetAbbreviatedMonthName(x.MonthNumber),
                    x.Total
                }).ToList();

            var months = monthlyExpenses.Select(x => x.Month).ToList();
            var totals = monthlyExpenses.Select(x => x.Total).ToList();
            var monthNumbers = monthlyExpenses.Select(x => x.MonthNumber).ToList();

            var average = totals.Any() ? totals.Average() : 0;

            ViewBag.Months = months;
            ViewBag.Totals = totals;
            ViewBag.MonthNumbers = monthNumbers;
            ViewBag.Average = average;

            return View();
        }

        [HttpGet]
        public IActionResult MonthBreakdown(int month)
        {
            // Load expenses for the requested month, include Category, group by category name
            var breakdown = _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.Date.Month == month)
                .AsEnumerable()
                .GroupBy(e => e.Category?.Name ?? "Uncategorized")
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(e => (double)e.Amount)
                })
                .ToList();

            return Json(breakdown);
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
