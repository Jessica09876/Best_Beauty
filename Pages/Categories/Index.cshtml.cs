using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Best_Beauty.Data;
using Best_Beauty.Models;

namespace Best_Beauty.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Best_Beauty.Data.Best_BeautyContext _context;

        public IndexModel(Best_Beauty.Data.Best_BeautyContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
