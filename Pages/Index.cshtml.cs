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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductContext _context;
        public IList<Product> ProductsList {get; set;}
        public string NameSort {get; set;}
        public string DateSort {get; set;}
        public string PriceOrder {get; set;}
        public string CurrentSort {get; set;}
        public string searchFilter {get; set;}

        public IndexModel(ILogger<IndexModel> logger, ProductContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name" : "name_desc";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            PriceOrder = sortOrder == "price" ? "price_desc" : "price";

            searchFilter = searchString; 

            IQueryable<Product> products = from p in _context.Product select p; 

            if(!String.IsNullOrEmpty(searchFilter))
            {
                products = products.Where(p => p.name.Contains(searchFilter)); 
            }

            switch(sortOrder)
            {
                case "name":
                products = products.OrderByDescending(p => p.name);
                break;
                case "Date":
                products = products.OrderBy(p => p.timeRetreived);
                break;
                case "date_desc":
                products = products.OrderByDescending(p => p.timeRetreived);
                break;
                case "name_desc":
                products = products.OrderBy(p => p.name);
                break;
                case "price":
                products = products.OrderBy(p => p.price);
                break;
                case "price_desc":
                products = products.OrderByDescending(p => p.price);
                break;
                default:
                products = products.OrderBy(p => p.timeRetreived);
                break;
            }
            ProductsList = await products.AsNoTracking().ToListAsync();   
        }
    }
}
