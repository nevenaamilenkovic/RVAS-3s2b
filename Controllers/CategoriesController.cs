using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tsql3s2b.Data;
using Tsql3s2b.Models;
using Tsql3s2b.Models.ViewModels;

namespace Tsql3s2b.Controllers
{
    public class CategoriesController : Controller
    {



        private readonly TsqlContext _context;
        public CategoriesController(TsqlContext context) {
            _context = context;
        }

        //READ
        public async Task<IActionResult> Index()
        {
            var kategorije = await _context.Categories.ToListAsync();
            return View(kategorije);
        }

        // sa /categories/create
        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //problem sa vezbi: bilo je [Bind("Categoryid","CategoryName","Description")] a treba [Bind("CategoryId","Categoryname","Description")]
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

        //UPDATE
        public async Task<IActionResult>Edit(int id)
        {
            var c = await _context.Categories.FirstOrDefaultAsync(c=>c.Categoryid == id);
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, [Bind("CategoryId", "Categoryname", "Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);

        }

        //DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Categories
                .Include(c=>c.Products)
                .FirstOrDefaultAsync(c=>c.Categoryid == id);
            if(c == null)
            {
                return NotFound();
            }
            var model = new CategoryViewModel
            {
                Category = c,
                Products = c.Products.ToList()
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Categoryid == id);

            if (c != null)
            {
                _context.Categories.Remove(c);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
