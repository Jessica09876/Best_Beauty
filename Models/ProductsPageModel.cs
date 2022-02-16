using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Best_Beauty.Data;

namespace Best_Beauty.Models
{
    public class ProductsPageModel:PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(Best_BeautyContext context,
        Service service)
        {
            var allCategories = context.Category;
            var serviceCategories = new HashSet<int>(
            service.Products.Select(c => c.CategoryID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                   CategoryID = cat.ID,
                    Name = cat.NumeCategorie,
                    Assigned = serviceCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateProducts(Best_BeautyContext context,
        string[] selectedCategories, Service serviceToUpdate)
        {
            if (selectedCategories == null)
            {
                serviceToUpdate.Products = new List<Product>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var serviceCategories = new HashSet<int>
            (serviceToUpdate.Products.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!serviceCategories.Contains(cat.ID))
                    {
                        serviceToUpdate.Products.Add(
                        new Product
                        {
                            ProductID = serviceToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (serviceCategories.Contains(cat.ID))
                    {
                        Product courseToRemove
                        = serviceToUpdate
                        .Products
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
