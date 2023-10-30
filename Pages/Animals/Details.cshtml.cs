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
    public class DetailsModel : PageModel
    {
        private readonly FullCRUDZoo.Data.ZooContext _context;

        public DetailsModel(FullCRUDZoo.Data.ZooContext context)
        {
            _context = context;
        }

      public Animal Animal { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FirstOrDefaultAsync(m => m.AnimalID == id);
            if (animal == null)
            {
                return NotFound();
            }
            else 
            {
                Animal = animal;
            }
            return Page();
        }
    }
}
