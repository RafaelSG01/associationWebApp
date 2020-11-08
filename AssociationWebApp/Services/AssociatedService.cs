using AssociationWebApp.Data;
using AssociationWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AssociationWebApp.Services
{
    public class AssociatedService
    {
        private readonly AssociationDbContext _context;

        public AssociatedService(AssociationDbContext context)
        {
            _context = context;
        }
        //Search Associeated in db and get result
        public async Task<List<Associated>> FainBayNameAsync(string name, string cpf)
        {
            var result = from obj in _context.Associated select obj;
            if (!String.IsNullOrEmpty(name))
            {
                result = result.Where(x => x.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(cpf))
            {
                result = result.Where(x => x.Cpf.Contains(cpf));
            }

            return await result.ToListAsync();
        }
    }
}
