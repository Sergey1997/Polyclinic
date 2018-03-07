using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models.BusinessLogic;
using Microsoft.AspNetCore.Authorization;

namespace Company.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class ProvidersController : Controller
    {
        private readonly CompanyContext _context;

        public ProvidersController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Providers.Include(p => p.Material);
            return View(await companyContext.ToListAsync());
        }

        // GET: Providers/Details/5
        // GET: Providers/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MaterialId")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", provider.MaterialId);
            return View(provider);
        }
        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .Include(p => p.Material)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Providers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Providers.Any(e => e.Id == id);
        }
    }
}
