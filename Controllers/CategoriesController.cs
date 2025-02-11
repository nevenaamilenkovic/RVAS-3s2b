using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tsql3s2b.Data;
using Tsql3s2b.Models;

namespace Tsql3s2b.Controllers
{
    public class CategoriesController : Controller
    {



        private readonly TsqlContext _context;
        public CategoriesController(TsqlContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kategorije = await _context.Categories.ToListAsync();
            return View(kategorije);
        }

        // sa /categories/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //problem sa vezbi bilo je [Bind("Categoryid","CategoryName","Description")] a treba [Bind("CategoryId","Categoryname","Description")]
        //vodite racuna o nazivima kolona, da svako slovo bude isto i iste velicine kao u modelu
        public async Task<IActionResult> Create([Bind("CategoryId","Categoryname","Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
