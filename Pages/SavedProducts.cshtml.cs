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
        public string NameSort { get; set; }
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
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "name_desc";

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

            switch (sortOrder)
            {
                case "name_desc":
                    productMetas = productMetas.OrderByDescending(p => p.NameHtmlId);
                    break;
                case "name":
                    productMetas = productMetas.OrderBy(p => p.NameHtmlId);
                    break;
            }
            int pageSize = 10;
            SavedProductsList = await Paginate<ProductMeta>.CreateAsync(productMetas.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public void OnPost()
        {
            SavedProductsList = _context.ProductMeta.ToList();
            scrape scraper = new scrape();
            scraper.Scrape();
            RedirectToPage("./Index"); 
        }
    }
}