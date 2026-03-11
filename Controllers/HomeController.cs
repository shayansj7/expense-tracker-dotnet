using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                    Month = g.Key,
                    Total = g.Sum(e => (double)e.Amount)
                })
                .OrderBy(x => x.Month)
                .ToList();

            ViewBag.Months = monthlyExpenses.Select(x => x.Month).ToList();
            ViewBag.Totals = monthlyExpenses.Select(x => x.Total).ToList();

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
