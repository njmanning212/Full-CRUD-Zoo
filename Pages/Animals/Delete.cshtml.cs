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
    public class DeleteModel : PageModel
    {
        private readonly FullCRUDZoo.Data.ZooContext _context;

        public DeleteModel(FullCRUDZoo.Data.ZooContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;
        public string? Age { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            Animal = await _context.Animals.FindAsync(id);

            DateTime Today = DateTime.Now;

            if (Today.Year - Animal.DateOfBirth.Year == 0)
            {
                Age = "< 1 years old";
            }
            else
            {
                Age = (Today.Year - Animal.DateOfBirth.Year).ToString() + " years old";
            }

            if (Animal == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }
            var animal = await _context.Animals.FindAsync(id);

            if (animal != null)
            {
                Animal = animal;
                _context.Animals.Remove(Animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
