using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WishList.Pages
{
    public class SavedProducts : PageModel
    {
        private readonly ProductContext _context;
        private readonly ILogger<SavedProducts> _logger;
        public List<ProductMeta> SavedProductsList {get; set;}

        public SavedProducts(ILogger<SavedProducts> logger, ProductContext context)
        {
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {
            SavedProductsList = _context.ProductMeta.ToList();    
        }
        public void OnPost()
        {
            scrape scraper = new scrape(_context);
            scraper.TestScrape();
            RedirectToPage("Index"); 
        }
    }
}