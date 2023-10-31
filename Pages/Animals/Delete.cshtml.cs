using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FullCRUDZoo.Data;
using Full_CRUD_Zoo;
using System.Runtime;

namespace FullCRUDZoo.Pages.Animals
{
    public class DeleteModel : PageModel
    {
        private readonly FullCRUDZoo.Data.ZooContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(FullCRUDZoo.Data.ZooContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Animal Animal { get; set; } = default!;
        public string Age { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            Animal = await _context.Animals
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AnimalID == id);
            
            if (Animal == null)
            {
                return NotFound();
            }

            DateTime Today = DateTime.Now;

            if (Today.Year - Animal.DateOfBirth.Year == 0)
            {
                Age = "< 1 years old";
            }
            else
            {
                Age = (Today.Year - Animal.DateOfBirth.Year).ToString() + " years old";
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = $"Delete {Animal.Name} failed. Try again.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            try 
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting animal");
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}
