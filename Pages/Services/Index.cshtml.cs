using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Best_Beauty.Data;
using Best_Beauty.Models;

namespace Best_Beauty.Pages.Services
{
    public class IndexModel : PageModel
    {
        private readonly Best_Beauty.Data.Best_BeautyContext _context;

        public IndexModel(Best_Beauty.Data.Best_BeautyContext context)
        {
            _context = context;
        }

        public IList<Service> Service { get;set; }
        public ServiceData ServiceD { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            ServiceD = new ServiceData();
            ServiceD.Services = await _context.Service
                .Include(b => b.Client)
                .Include(b => b.Products)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Denumire)
                .ToListAsync();

            if (id != null)
            {
                ProductID = id.Value;
                Service service = ServiceD.Services
                .Where(i => i.ID == id.Value).Single();
                ServiceD.Categories = service.Products.Select(s => s.Category);
            }
        }
    }
}
