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
using AssociationWebApp.Services.Exceptions;
using AssociationWebApp.Models.ViewModel;
using System.Diagnostics;

namespace AssociationWebApp.Controllers
{
    public class AssociatedsController : Controller
    {
        private readonly AssociationDbContext _context;
        private readonly AssociatedService _associatedService;

        public AssociatedsController(AssociationDbContext context, AssociatedService associatedService)
        {
            _context = context;
            _associatedService = associatedService;
        }

        // GET: Associateds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Associated.ToListAsync());
        }

        // GET: Associateds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            var associated = await _context.Associated.FirstOrDefaultAsync(m => m.Id == id);
            if (associated == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não encontrado!" });
            }

            return View(associated);
        }

        // GET: Associateds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Associateds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cpf,BirthDate")] Associated associated)
        {
            if (ModelState.IsValid)
            {
                //Return erro repeated CPF
                bool hasCpf = await _context.Associated.AnyAsync(x => x.Cpf == associated.Cpf);
                if (hasCpf)
                {
                    return RedirectToAction(nameof(Error), new { message = "CPF informado já foi cadastrado!" });
                }
                //end return


                _context.Add(associated);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(associated);
        }

        // GET: Associateds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            var associated = await _context.Associated.FindAsync(id);
            if (associated == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não encontrado!" });
            }
            return View(associated);
        }

        // POST: Associateds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cpf,BirthDate")] Associated associated)
        {
            if (id != associated.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificadores diferentes!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(associated);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociatedExists(associated.Id))
                    {
                        return RedirectToAction(nameof(Error), new { message = "O identificador não existe!" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(associated);
        }

        // GET: Associateds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            var associated = await _context.Associated
                .FirstOrDefaultAsync(m => m.Id == id);
            if (associated == null)
            {
                return RedirectToAction(nameof(Error), new { message = "O identificador não foi fornecido!" });
            }

            return View(associated);
        }

        // POST: Associateds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var associated = await _context.Associated.FindAsync(id);
            _context.Associated.Remove(associated);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssociatedExists(int id)
        {
            return _context.Associated.Any(e => e.Id == id);
        }
        //Search Associated
        public async Task<IActionResult> SearchAsync(string name, string cpf)
        {
            var result = await _associatedService.FainBayNameAsync(name, cpf);
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
