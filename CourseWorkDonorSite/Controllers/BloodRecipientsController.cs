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
    public class BloodRecipientsController : Controller
    {
        private readonly DonorContext _context;
        private IBloodRepository BloodRepositories { get; }
        public BloodRecipientsController (DonorContext context, IBloodRepository repositories)
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
        // GET: BloodRecipients
        public async Task<IActionResult> Index()
        {
            _listCitiesOfDonation = BloodRepositories.GetCitiesOfDonation();

            listItemsCitiesDonation = new SelectList(_listCitiesOfDonation, _listCitiesOfDonation[0]);

            ViewData["Title"] = "Реципієнти";

            ViewBag.SelectItem1 = listItemsBloodTypes;
            ViewBag.SelectItem2 = listItemsBloodRhesus;
            ViewBag.SelectItem3 = listItemsCitiesDonation;

            var donorContext = _context.BloodRecipients.Include(b => b.City);
            return View(await donorContext.OrderBy(t => t.RecipientName).ToListAsync());
        }

        [HttpPost]
        public IActionResult Index(string selectedItemBloodType, string selectedItemBloodRhesus, string selectedItemCityDonation)
        {
            ViewData["Title"] = "Знайдені реципієнти";

            _listCitiesOfDonation = BloodRepositories.GetCitiesOfDonation();

            listItemsCitiesDonation = new SelectList(_listCitiesOfDonation, selectedItemCityDonation);

            listItemsBloodTypes = new SelectList(_listOfBloodTypes, selectedItemBloodType);

            listItemsBloodRhesus = new SelectList(_listOfBloodRhesus, selectedItemBloodRhesus);

            ViewBag.SelectItem1 = listItemsBloodTypes;
            ViewBag.SelectItem2 = listItemsBloodRhesus;
            ViewBag.SelectItem3 = listItemsCitiesDonation;


            var bloodRecipients = _context.BloodRecipients
               .Include(b => b.City)
               .Where(m => m.BloodType == selectedItemBloodType && m.RhesusBlood == selectedItemBloodRhesus && m.City.Name == selectedItemCityDonation);

            return View("Index", bloodRecipients.OrderBy(t => t.RecipientName).ToList());

        }

        // GET: BloodRecipients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Title"] = "Інформація про реципієнта";

            if (id == null)
            {
                return NotFound();
            }

            var bloodRecipient = await _context.BloodRecipients
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.BloodRecipientId == id);
            if (bloodRecipient == null)
            {
                return NotFound();
            }

            return View(bloodRecipient);
        }

        // GET: BloodRecipients/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Додати реципієнта";

            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name");
            return View();
        }

        // POST: BloodRecipients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BloodRecipientId,CityId,RecipientName,Phone,BloodType,RhesusBlood")] BloodRecipient bloodRecipient)
        {
            ViewData["Title"] = "Додати реципієнта";

            if (ModelState.IsValid)
            {
                _context.Add(bloodRecipient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodRecipient.CityId);
            return View(bloodRecipient);
        }

        // GET: BloodRecipients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = "Редагувати реципієнта";

            if (id == null)
            {
                return NotFound();
            }

            var bloodRecipient = await _context.BloodRecipients.FindAsync(id);
            if (bloodRecipient == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodRecipient.CityId);
            return View(bloodRecipient);
        }

        // POST: BloodRecipients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BloodRecipientId,CityId,RecipientName,Phone,BloodType,RhesusBlood")] BloodRecipient bloodRecipient)
        {
            ViewData["Title"] = "Редагувати реципієнта";

            if (id != bloodRecipient.BloodRecipientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodRecipient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodRecipientExists(bloodRecipient.BloodRecipientId))
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
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(t => t.Name), "CityId", "Name", bloodRecipient.CityId);
            return View(bloodRecipient);
        }

        // GET: BloodRecipients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = "Видалити реципієнта";

            if (id == null)
            {
                return NotFound();
            }

            var bloodRecipient = await _context.BloodRecipients
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.BloodRecipientId == id);
            if (bloodRecipient == null)
            {
                return NotFound();
            }

            return View(bloodRecipient);
        }

        // POST: BloodRecipients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Title"] = "Видалити реципієнта";

            var bloodRecipient = await _context.BloodRecipients.FindAsync(id);
            _context.BloodRecipients.Remove(bloodRecipient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodRecipientExists(int id)
        {
            return _context.BloodRecipients.Any(e => e.BloodRecipientId == id);
        }
    }
}
