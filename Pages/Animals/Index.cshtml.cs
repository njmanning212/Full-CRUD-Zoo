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

        public string NameSort { get; set; }
        public string SpeciesSort { get; set; }
        public string LastFedSort { get; set; }

        public IList<Animal> Animals { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            SpeciesSort = sortOrder == "Species" ? "species_desc" : "Species";
            LastFedSort = sortOrder == "LastFed" ? "lastfed_desc" : "LastFed";

            IQueryable<Animal> animalsIQ = from a in _context.Animals
                                           select a;
            
            switch (sortOrder)
            {
                case "name_desc":
                    animalsIQ = animalsIQ.OrderByDescending(a => a.Name);
                    break;
                case "Species":
                    animalsIQ = animalsIQ.OrderBy(a => a.Species);
                    break;
                case "species_desc":
                    animalsIQ = animalsIQ.OrderByDescending(a => a.Species);
                    break;
                case "LastFed":
                    animalsIQ = animalsIQ.OrderBy(a => a.LastFed);
                    break;
                case "lastfed_desc":
                    animalsIQ = animalsIQ.OrderByDescending(a => a.LastFed);
                    break;
                default:
                    animalsIQ = animalsIQ.OrderBy(a => a.Name);
                    break;

            }

            Animals = await animalsIQ.AsNoTracking().ToListAsync();
        }
    }
}
