using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssociationWebApp.Data;
using AssociationWebApp.Models;
using AssociationWebApp.Services.Exceptions;
using AssociationWebApp.Models.ViewModel;
using System.Diagnostics;

namespace AssociationWebApp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly AssociationDbContext _context;
        private readonly CompanyService _companyService;

        public CompaniesController(AssociationDbContext context, CompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Company.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cnpj")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não encontrado!" });
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cnpj")] Company company)
        {
            if (id != company.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificadores diferentes!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return RedirectToAction(nameof(Error), new { message = "O identificador não Existe!" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            var company = await _context.Company
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não encontrado!" });
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Company.FindAsync(id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.Id == id);
        }
        //Search Company
        public async Task<IActionResult> SearchAsync(string name, string cnpj)
        {
            var result = await _companyService.FainBayNameAsync(name, cnpj);
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
