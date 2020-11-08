using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationWebApp.Models.ViewModel
{
    public class AssociatedViewModel
    {
        public Associated Associated { get; set; }
        public Association Association { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}
