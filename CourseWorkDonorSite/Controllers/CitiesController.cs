using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseWorkDonorSite.Models;
using CourseWorkDonorSite.Settings;
using Microsoft.AspNetCore.Authorization;

namespace CourseWorkDonorSite.Controllers
{
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly DonorContext _context;

        public CitiesController(DonorContext context)
        {
            _context = context;
            Config.SidebarVisible = false;
            Config.UseBootstrap = false;
        }
               
        // GET: Cities
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Міста";

            return View(await _context.Cities.OrderBy(t => t.Name).ToListAsync());
        }
                

        // GET: Cities/Details/5
        public IActionResult Details(int? id)
        {
            ViewData["Title"] = "Місця здачі крові";

            if (id == null)
            {
                return NotFound();
            }
            
            List <BloodTransfusionStation> stations = _context.BloodTransfusionStations.Where(m => m.CityId == id).OrderBy(t => t.Name).ToList();

            if (stations == null)
            {
                return NotFound();
            }

            return View(stations);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Додання нового міста";
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,Name")] City city)
        {
            ViewData["Title"] = "Додання нового міста";

            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = "Редагування міста";

            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,Name")] City city)
        {
            ViewData["Title"] = "Редагування міста";

            if (id != city.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.CityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = "Видалення міста";

            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.CityId == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Title"] = "Видалення міста";

            var city = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.CityId == id);
        }
    }
}
