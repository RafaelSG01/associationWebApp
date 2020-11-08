using AssociationWebApp.Data;
using AssociationWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationWebApp.Services
{
    public class AssociationService
    {
        private readonly AssociationDbContext _context;

        public AssociationService(AssociationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Association>> FainBayNameAsync(string name, string cpf)
        {
            var result = from obj in _context.Association  select obj;
            if (!String.IsNullOrEmpty(name))
            {
                result = result.Where(x => x.Associated.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(cpf))
            {
                result = result.Where(x => x.Associated.Cpf.Contains(cpf));
            }


            return await result.Include(x => x.Associated).Include(x => x.Company).ToListAsync();
        }
    }
}
