using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWorkDonorSite.Models;

namespace CourseWorkDonorSite.Controllers
{
    public class BloodTransfusionStationsController : Controller
    {
        private readonly DonorContext _context;

        public BloodTransfusionStationsController(DonorContext context)
        {
            _context = context;
        }

        // GET: BloodTransfusionStations
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Місця здачі крові";

            var donorContext = _context.BloodTransfusionStations.Include(b => b.City);
            return View(await donorContext.OrderBy(t => t.Name).ToListAsync());
        }

        // GET: BloodTransfusionStations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Title"] = "Докладніше про місце здачі крові";

            if (id == null)
            {
                return NotFound();
            }

            var bloodTransfusionStation = await _context.BloodTransfusionStations
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (bloodTransfusionStation == null)
            {
                return NotFound();
            }

            return View(bloodTransfusionStation);
        }

        // GET: BloodTransfusionStations/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Додати місце здачі крові";
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name");
            return View();
        }

        // POST: BloodTransfusionStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StationId,CityId,Name,Address")] BloodTransfusionStation bloodTransfusionStation)
        {

            ViewData["Title"] = "Додати місце здачі крові";
            if (ModelState.IsValid)
            {
                _context.Add(bloodTransfusionStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodTransfusionStation.CityId);
            return View(bloodTransfusionStation);
        }

        // GET: BloodTransfusionStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = "Редагувати місце здачі крові";

            if (id == null)
            {
                return NotFound();
            }

            var bloodTransfusionStation = await _context.BloodTransfusionStations.FindAsync(id);
            if (bloodTransfusionStation == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodTransfusionStation.CityId);
            return View(bloodTransfusionStation);
        }

        // POST: BloodTransfusionStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StationId,CityId,Name,Address")] BloodTransfusionStation bloodTransfusionStation)
        {
            ViewData["Title"] = "Редагувати місце здачі крові";

            if (id != bloodTransfusionStation.StationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodTransfusionStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodTransfusionStationExists(bloodTransfusionStation.StationId))
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
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t =>t.Name), "CityId", "Name", bloodTransfusionStation.CityId);
            return View(bloodTransfusionStation);
        }

        // GET: BloodTransfusionStations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = "Видалити місце здачі крові";

            if (id == null)
            {
                return NotFound();
            }

            var bloodTransfusionStation = await _context.BloodTransfusionStations
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (bloodTransfusionStation == null)
            {
                return NotFound();
            }

            return View(bloodTransfusionStation);
        }

        // POST: BloodTransfusionStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Title"] = "Видалити місце здачі крові";

            var bloodTransfusionStation = await _context.BloodTransfusionStations.FindAsync(id);
            _context.BloodTransfusionStations.Remove(bloodTransfusionStation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodTransfusionStationExists(int id)
        {
            return _context.BloodTransfusionStations.Any(e => e.StationId == id);
        }
    }
}
