using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FullCRUDZoo.Data;
using Full_CRUD_Zoo;

namespace FullCRUDZoo.Pages.Animals
{
    public class EditModel : PageModel
    {
        private readonly FullCRUDZoo.Data.ZooContext _context;

        public EditModel(FullCRUDZoo.Data.ZooContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal = await _context.Animals.FindAsync(id);

            if (Animal == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var animalToUpdate = await _context.Animals.FindAsync(id);

            if (animalToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Animal>(
                animalToUpdate,
                "animal",
                a => a.Name, a => a.Species, a => a.Diet, a => a.PhotoURL, a => a.DateOfBirth, a => a.DateAquired, a => a.LastFed
            ))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Details", new { id = animalToUpdate.AnimalID });
            }
            return Page();
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animals?.Any(e => e.AnimalID == id)).GetValueOrDefault();
        }
    }
}
