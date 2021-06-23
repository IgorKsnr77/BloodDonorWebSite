using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWorkDonorSite.Models;
using CourseWorkDonorSite.Settings;
using Microsoft.AspNetCore.Authorization;
using CourseWorkDonorSite.Models.Repositories.Abstract;


namespace CourseWorkDonorSite.Controllers
{
    [Authorize]
    public class BloodDonorsController : Controller
    {
        private readonly DonorContext _context;
        private IBloodRepository BloodRepositories { get; }
        public BloodDonorsController(DonorContext context, IBloodRepository repositories)
        {
            _context = context;
            BloodRepositories = repositories;
            Config.SidebarVisible = false;
            Config.UseBootstrap = false;
                        
        }

        private static string[] _listOfBloodTypes = BloodDonor.GetTypesOfBlood();

        private SelectList listItemsBloodTypes = new SelectList(_listOfBloodTypes, _listOfBloodTypes[0]);

        private static string[] _listOfBloodRhesus = BloodDonor.GetRhesusOfBlood();

        private SelectList listItemsBloodRhesus = new SelectList(_listOfBloodRhesus, _listOfBloodRhesus[0]);
               
        private string[] _listCitiesOfDonation;

        private SelectList listItemsCitiesDonation;

        // GET: BloodDonors
        public async Task<IActionResult> Index()
        {
            _listCitiesOfDonation = BloodRepositories.GetCitiesOfDonation();

            listItemsCitiesDonation = new SelectList(_listCitiesOfDonation, _listCitiesOfDonation[0]);

            ViewData["Title"] = "Донори";

            ViewBag.SelectItem1 = listItemsBloodTypes;
            ViewBag.SelectItem2 = listItemsBloodRhesus;
            ViewBag.SelectItem3 = listItemsCitiesDonation;
                        

            var donorContext = _context.BloodDonors.Include(b => b.City);
            return View(await donorContext.OrderBy(t => t.DonorName).ToListAsync());
        }

        [HttpPost]
        public IActionResult Index(string selectedItemBloodType, string selectedItemBloodRhesus, string selectedItemCityDonation)
        {
            ViewData["Title"] = "Знайдені донори";
            
            _listCitiesOfDonation = BloodRepositories.GetCitiesOfDonation();

            listItemsCitiesDonation = new SelectList(_listCitiesOfDonation, selectedItemCityDonation);

            listItemsBloodTypes = new SelectList(_listOfBloodTypes, selectedItemBloodType);

            listItemsBloodRhesus = new SelectList(_listOfBloodRhesus, selectedItemBloodRhesus);
                        
            ViewBag.SelectItem1 = listItemsBloodTypes;
            ViewBag.SelectItem2 = listItemsBloodRhesus;
            ViewBag.SelectItem3 = listItemsCitiesDonation;


            var bloodDonors = _context.BloodDonors
               .Include(b => b.City)
               .Where(m => m.BloodType == selectedItemBloodType && m.RhesusBlood == selectedItemBloodRhesus && m.City.Name == selectedItemCityDonation );

            return View("Index", bloodDonors.OrderBy(t => t.DonorName).ToList());
            
        }

        // GET: BloodDonors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Title"] = "Інформація про донора";
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonor = await _context.BloodDonors
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.BloodDonorId == id);
            if (bloodDonor == null)
            {
                return NotFound();
            }

            return View(bloodDonor);
        }

        // GET: BloodDonors/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Додати донора";

            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name");
            return View();
        }

        // POST: BloodDonors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BloodDonorId,CityId,DonorName,Phone,BloodType,RhesusBlood")] BloodDonor bloodDonor)
        {
            ViewData["Title"] = "Додати донора";

            if (ModelState.IsValid)
            {
                _context.Add(bloodDonor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodDonor.CityId);
            return View(bloodDonor);
        }

        // GET: BloodDonors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = "Редагувати донора";

            if (id == null)
            {
                return NotFound();
            }

            var bloodDonor = await _context.BloodDonors.FindAsync(id);
            if (bloodDonor == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodDonor.CityId);
            return View(bloodDonor);
        }

        // POST: BloodDonors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BloodDonorId,CityId,DonorName,Phone,BloodType,RhesusBlood")] BloodDonor bloodDonor)
        {
            ViewData["Title"] = "Редагувати донора";

            if (id != bloodDonor.BloodDonorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodDonor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodDonorExists(bloodDonor.BloodDonorId))
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
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodDonor.CityId);
            return View(bloodDonor);
        }

        // GET: BloodDonors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = "Видалити донора";
            
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonor = await _context.BloodDonors
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.BloodDonorId == id);
            if (bloodDonor == null)
            {
                return NotFound();
            }

            return View(bloodDonor);
        }

        // POST: BloodDonors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Title"] = "Видалити донора";

            var bloodDonor = await _context.BloodDonors.FindAsync(id);
            _context.BloodDonors.Remove(bloodDonor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodDonorExists(int id)
        {
            return _context.BloodDonors.Any(e => e.BloodDonorId == id);
        }
    }
}
