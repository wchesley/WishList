using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WishList.Pages
{
    public class SavedProducts : PageModel
    {
        private readonly ProductContext _context;
        private readonly ILogger<SavedProducts> _logger;
        public Paginate<ProductMeta> SavedProductsList { get; set; }
        // Pagination vars: 
        public string NameSort { get; set; }
        public string NameIdSort { get; set; }
        public string CurrentSort { get; set; }
        public string searchFilter { get; set; }

        public SavedProducts(ILogger<SavedProducts> logger, ProductContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnGetAsync(string sortOrder, string currentFilter,
            string searchString, int? pageIndex)
        {
            //Set pagination vars: 
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "name_desc";
            NameIdSort = sortOrder == "id" ? "id_desc" : "id"; 

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            searchFilter = searchString;

            IQueryable<ProductMeta> productMetas = from p in _context.ProductMeta select p;

            if (!String.IsNullOrEmpty(searchFilter))
            {
                productMetas = productMetas.Where(p => p.NameHtmlId.Contains(searchFilter));
            }
            //Reorder list based on which button was clicked in the webpage: 
            switch (sortOrder)
            {
                case "name":
                    productMetas = productMetas.OrderBy(p => p.VanityName);
                    break;
                case "name_desc":
                    productMetas = productMetas.OrderByDescending(p => p.VanityName);
                    break;
                case "id": 
                    productMetas = productMetas.OrderBy(p => p.NameHtmlId); 
                    break; 
                case "id_desc":
                    productMetas = productMetas.OrderByDescending(p => p.NameHtmlId);
                    break;
                default:
                    productMetas = productMetas.OrderBy(p => p.Id);
                    break;
                
            }
            //max number of objects returned from database: 
            int pageSize = 10;
            SavedProductsList = await Paginate<ProductMeta>.CreateAsync(productMetas.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //Load 1:M relationship from DB: 
            ProductMeta itemToDelete = await _context.ProductMeta.Include(p => p.products)
                .AsNoTracking().FirstOrDefaultAsync(pd => pd.Id == id);
            var scrapedItems = itemToDelete.products;
            if (itemToDelete == null)
            {
                return NotFound();
            }
            try
            {
                //Delete Parent obj: 
                _context.ProductMeta.Remove(itemToDelete);
                //Delete Children too: 
                _context.RemoveRange(scrapedItems);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Obj Deleted from Database:\nID:{itemToDelete.Id}\nURL:{itemToDelete.ProductUrl}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Deleting obj");
            }
            return RedirectToPage("./SavedProducts");
        }
    }
}