using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WishList
{
    public class ProductPage : PageModel
    {
        private readonly ILogger<ProductPage> _logger;
        private readonly ProductContext _context; 
        public ProductMeta ProductDetails {get; set;} 
        public ProductPage(ILogger<ProductPage> logger, ProductContext context)
        {
            _context = context; 
            _logger = logger; 
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound(); 
            }

            ProductDetails = await _context.ProductMeta.Include( p => p.products)
                .AsNoTracking().FirstOrDefaultAsync(pd => pd.Id == id);
            if(ProductDetails == null)
            {
                return NotFound(); 
            }
            return Page(); 
        }
    }
}