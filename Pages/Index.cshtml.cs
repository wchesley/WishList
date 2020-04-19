using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WishList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductContext _context;
        public List<Product> ProductsList {get; set;}

        public IndexModel(ILogger<IndexModel> logger, ProductContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            ProductsList = _context.Product.ToList(); 
        }
    }
}
