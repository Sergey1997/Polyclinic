using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models.BusinessLogic;
using Microsoft.AspNetCore.Identity;
using Company.Models;
using Microsoft.AspNetCore.Authorization;

namespace Company.Controllers
{
    public class ObjectOfExpendituresController : Controller
    {
        private readonly CompanyContext _context;

        public ObjectOfExpendituresController(CompanyContext context)
        {
            _context = context;

        }

        // GET: ObjectOfExpenditures
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.ObjectOfExpenditures.Include(o => o.Material).Include(o => o.Provider);
            return View(await companyContext.ToListAsync());
        }
        [Authorize(Roles = "Менеджер")]
        public async Task<IActionResult> CountlyMonth(string dateFrom,string dateTo,string material, string provider)
        {
            var materials = from ObjectOfExpenditure in _context.ObjectOfExpenditures select ObjectOfExpenditure;

            if (!String.IsNullOrEmpty(dateFrom) && !String.IsNullOrEmpty(dateTo))
            {

                materials = materials.Where(s => s.Date.CompareTo(dateFrom) >= 0 && s.Date.CompareTo(dateTo) <= 0);
            }
            if (!String.IsNullOrEmpty(material))
            {
                materials = materials.Where(s => s.Material.Name.ToLower().Contains(material));
            }
            if (!String.IsNullOrEmpty(provider))
            {
                materials = materials.Where(s => s.Provider.Name.ToLower().Contains(provider));
            }

            ViewBag.Materials = await _context.Materials.ToListAsync();

            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(materials);
        }
        // GET: ObjectOfExpenditures/Create

        [Authorize(Roles = "Учётчик")]
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name");
            return View();
        }

        [Authorize(Roles = "Учётчик")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaterialId,Amount,ProviderId")] ObjectOfExpenditure objectOfExpenditure)
        {
            objectOfExpenditure.Date = DateTime.Now;
            objectOfExpenditure.UserId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(objectOfExpenditure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", objectOfExpenditure.MaterialId);
            ViewData["ProviderId"] = new SelectList(_context.Providers, "Id", "Name", objectOfExpenditure.ProviderId);
            return View(objectOfExpenditure);
        }

        [Authorize(Roles = "Менеджер")]
        // GET: ObjectOfExpenditures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectOfExpenditure = await _context.ObjectOfExpenditures
                .Include(o => o.Material)
                .Include(o => o.Provider)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (objectOfExpenditure == null)
            {
                return NotFound();
            }

            return View(objectOfExpenditure);
        }

        // POST: ObjectOfExpenditures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objectOfExpenditure = await _context.ObjectOfExpenditures.SingleOrDefaultAsync(m => m.Id == id);
            _context.ObjectOfExpenditures.Remove(objectOfExpenditure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObjectOfExpenditureExists(int id)
        {
            return _context.ObjectOfExpenditures.Any(e => e.Id == id);
        }
    }
}
