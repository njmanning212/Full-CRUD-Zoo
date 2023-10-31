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
        public string Age { get; set; }

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

            DateTime Today = DateTime.Now;

            if (Today.Year - Animal.DateOfBirth.Year == 0)
            {
                Age = "< 1 year old";
            }
            else
            {
                Age = (Today.Year - Animal.DateOfBirth.Year).ToString() + " years old";
            }

            return Page();
        }

    }
}
