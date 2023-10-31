using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FullCRUDZoo.Data;
using Full_CRUD_Zoo;

namespace FullCRUDZoo.Pages.Animals
{
    public class CreateModel : PageModel
    {
        private readonly FullCRUDZoo.Data.ZooContext _context;

        public CreateModel(FullCRUDZoo.Data.ZooContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyAnimal = new Animal();

            if (await TryUpdateModelAsync(
                emptyAnimal,
                "animal",
                a => a.Name, a => a.Species, a => a.Diet, async => async.Photo, a => a.DateOfBirth, a => a.DateAquired, a => a.LastFed
            ))
            {
                _context.Animals.Add(emptyAnimal);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            return Page();
        }
    }
}
