using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WishList.Pages
{
    public class Create : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly ProductContext _context;
        [BindProperty]
        [Required]
        [Display(Name = "Price HTML Id:")]
        [MinLength(1)]
        public string priceId { get; set; }
        [BindProperty]
        [DataType(DataType.Url)]
        [Required]
        public string url { get; set; }
        [BindProperty]
        [Required]
        [Display(Name = "Product Name HTML Id:")]
        [MinLength(1)]
        public string productNameId { get; set; }
        [BindProperty]
        [MinLength(1)]
        [Display(Name = "Vanity Name: ")]
        public string vanityName { get; set; }
        public Create(ILogger<Create> logger, ProductContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var productMeta = new ProductMeta
            {
                ProductUrl = url,
                PriceHtmlId = priceId,
                NameHtmlId = productNameId,
                VanityName = vanityName
            };
            try
            {
                _context.ProductMeta.Add(productMeta);
                await _context.SaveChangesAsync();
                scrape scraper = new scrape();
                scraper.ScrapeSingle(productMeta.ProductUrl, productMeta.PriceHtmlId, productMeta.NameHtmlId, productMeta.Id);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Error Saving to database:");
            }
            return RedirectToPage("SavedProducts");
        }
    }
}