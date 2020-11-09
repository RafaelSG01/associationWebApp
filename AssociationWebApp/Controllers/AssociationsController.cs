using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssociationWebApp.Data;
using AssociationWebApp.Models;
using AssociationWebApp.Services;
using AssociationWebApp.Models.ViewModel;
using System.Diagnostics;
using AssociationWebApp.Services.Exceptions;

namespace AssociationWebApp.Controllers
{
    public class AssociationsController : Controller
    {
        private readonly AssociationDbContext _context;
        private readonly AssociationService _associationService;

        public AssociationsController(AssociationDbContext context, AssociationService associationService)
        {
            _context = context;
            _associationService = associationService;
        }

        // GET: Associations
        public async Task<IActionResult> Index()
        {
            var associationDbContext = _context.Association.Include(a => a.Associated).Include(a => a.Company);
            return View(await associationDbContext.ToListAsync());
        }        

        // GET: Associations/Create
        public IActionResult Create()
        {
            ViewData["AssociatedId"] = new SelectList(_context.Associated, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            return View();
        }

        // POST: Associations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssociatedId,CompanyId")] Association association)
        {
            if (ModelState.IsValid)
            {
                _context.Add(association);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssociatedId"] = new SelectList(_context.Associated, "Id", "Name", association.AssociatedId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", association.CompanyId);
            return View(association);
        }

        
        // GET: Associations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            var association = await _context.Association
                .Include(a => a.Associated)
                .Include(a => a.Company)
                .FirstOrDefaultAsync(m => m.AssociatedId == id);
            if (association == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            return View(association);
        }

        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var association = await _context.Association.FindAsync(id);
                _context.Association.Remove(association);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException)
            {
                return RedirectToAction(nameof(Error), new { message = "Identificador não encontrado!" });
            }
        }

        private bool AssociationExists(int id)
        {
            return _context.Association.Any(e => e.AssociatedId == id);
        }

        public async Task<IActionResult> SearchAsync(string namea, string namec)
        {
            var result = await _associationService.FainBayNameAsync(namea, namec);
            return View(result);
        }
        //Method error custom
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
