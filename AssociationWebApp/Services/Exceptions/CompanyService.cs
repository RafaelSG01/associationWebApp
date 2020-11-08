using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssociationWebApp.Data;
using AssociationWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AssociationWebApp.Services.Exceptions
{
    public class CompanyService
    {
        private readonly AssociationDbContext _context;

        public CompanyService(AssociationDbContext context)
        {
            _context = context;
        }
        //Search method Company in db and get result
        public async Task<List<Company>> FainBayNameAsync(string name, string cnpj)
        {
            var result = from obj in _context.Company select obj;
            if (!String.IsNullOrEmpty(name))
            {
                result = result.Where(x => x.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(cnpj))
            {
                result = result.Where(x => x.Cnpj.Contains(cnpj));
            }

            return await result.ToListAsync();
        }
    }
}
