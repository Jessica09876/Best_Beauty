using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Best_Beauty.Data;
using Best_Beauty.Models;


namespace Best_Beauty.Pages.Services
{
    public class CreateModel : ProductsPageModel
    {
        private readonly Best_Beauty.Data.Best_BeautyContext _context;
        public CreateModel(Best_Beauty.Data.Best_BeautyContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "NumeClient");
            var service = new Service();
            service.Products = new List<Product>();
            PopulateAssignedCategoryData(_context, service);
            return Page();
        }
        [BindProperty]
        public Service Service { get; set; }
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newService = new Service();
            if (selectedCategories != null)
            {
                newService.Products = new List<Product>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new Product
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newService.Products.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Service>(
            newService,
            "Service",
            i => i.Denumire, i => i.Durata,
            i => i.Pret, i => i.Data, i => i.ClientID))
            {
                _context.Service.Add(newService);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newService);
            return Page();
        }
    }
}