using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Best_Beauty.Data;
using Best_Beauty.Models;

namespace Best_Beauty.Pages.Services
{
    public class EditModel : ProductsPageModel
    {
        private readonly Best_Beauty.Data.Best_BeautyContext _context;

        public EditModel(Best_Beauty.Data.Best_BeautyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Service Service { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service = await _context.Service
                .Include(b => b.Client)
                .Include(b => b.Products).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Service == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Service);
            ViewData["ClientID"] = new SelectList(_context.Set<Client>(), "ID", "NumeClient");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceToUpdate = await _context.Service
            .Include(i => i.Client)
            .Include(i => i.Products)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (serviceToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Service>(
            serviceToUpdate,
            "Service",
            i => i.Denumire, i => i.Durata,
            i => i.Pret, i => i.Data, i => i.Client))
            {
                UpdateProducts(_context, selectedCategories, serviceToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateProducts(_context, selectedCategories, serviceToUpdate);
            PopulateAssignedCategoryData(_context, serviceToUpdate);
            return Page();
        }
    }
}
