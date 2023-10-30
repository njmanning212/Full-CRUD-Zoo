using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FullCRUDZoo.Data;
using Full_CRUD_Zoo;

namespace FullCRUDZoo.Pages.Animals
{
    public class IndexModel : PageModel
    {
        private readonly FullCRUDZoo.Data.ZooContext _context;

        public IndexModel(FullCRUDZoo.Data.ZooContext context)
        {
            _context = context;
        }

        public IList<Animal> Animal { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Animals != null)
            {
                Animal = await _context.Animals.ToListAsync();
            }
        }
    }
}