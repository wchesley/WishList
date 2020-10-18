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
        [Display(Name = "Price HTML Id:")]
        [MinLength(1)]
        public string priceId { get; set; }
        [BindProperty]
        [DataType(DataType.Url)]
        [Required]
        public string url { get; set; }
        [BindProperty]
        [Display(Name = "Product Name HTML Id:")]
        [MinLength(1)]
        public string productNameId { get; set; }
        [BindProperty]
        [MinLength(1)]
        [Display(Name = "Vanity Name: ")]
        public string vanityName { get; set; }
        [BindProperty]
        public string prefilledSite { get; set; }
        public Dictionary<string, List<string>> prefillData {get; set; }
        public Create(ILogger<Create> logger, ProductContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            prefillData = new FormattedSites().formattedSitesDict;
            return Page();  
        }

        public async Task<IActionResult> OnPostAsync()
        {
           //Check to see if user selected any prefilled site info: 
            if(!String.IsNullOrEmpty(prefilledSite))
            {
                _logger.LogInformation($"Using pre-filled info:{prefilledSite}"); 
                var siteData = new FormattedSites(); 
                siteData.formattedSitesDict.TryGetValue(prefilledSite, out List<string> priceNamesList);
                Console.WriteLine($"FOUND: {priceNamesList[0]}+ {priceNamesList[1]}");
                //When using prefilled info, it's always in the same order: price, name 
                priceId = priceNamesList[0];
                productNameId = priceNamesList[1]; 
            }
            var productMeta = new ProductMeta
            {
                ProductUrl = url,
                PriceHtmlId = priceId,
                NameHtmlId = productNameId,
                VanityName = vanityName
            };
            try
            {
                //Save changes to database and scrape the newly added product. 
                _context.ProductMeta.Add(productMeta);
                await _context.SaveChangesAsync();
                scrape scraper = new scrape(_context);
                scraper.ScrapeSingle(productMeta.ProductUrl, productMeta.PriceHtmlId, productMeta.NameHtmlId, productMeta.Id, _context);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Error Saving to database:");
            }
            return RedirectToPage("SavedProducts");
        }
    }
}