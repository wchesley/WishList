using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;

namespace WishList
{
    public class ProductPage : PageModel
    {
        private readonly ILogger<ProductPage> _logger;
        private readonly ProductContext _context;
        [BindProperty]
        public ProductMeta ProductDetails { get; set; }
        public List<Product> ProductsList { get; set; }
        public string chartXAxis { get; set; }
        public string chartYAxis { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Url)]
        public string productUrl { get; set; }
        [BindProperty]
        [Required]
        public string nameHtmlId { get; set; }
        [BindProperty]
        [Required]
        public string priceHtmlId { get; set; }
        [BindProperty]
        public string vanityName {get; set;}

        public ProductPage(ILogger<ProductPage> logger, ProductContext context)
        {
            _context = context;
            _logger = logger;
        }

        /*
        HTTP GET:
        */

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"OnGET Fired:\n\tRequested id?:{id.ToString()}\n\tTime:{DateTime.Now.ToString()}");
            
            //Load 1:M relationship from DB: 
            ProductDetails = await _context.ProductMeta.Include(p => p.products)
                .AsNoTracking().FirstOrDefaultAsync(pd => pd.Id == id);
                
            if (ProductDetails == null)
            {
                return NotFound();
            }

            ProductsList = ProductDetails.products.ToList();

            //convert data to JSON for Chart: 
            //prep two lists for x and y data: 
            List<Array> initX = new List<Array>();
            List<double> innitY = new List<double>();
            foreach (var product in ProductsList)
            {
                double tempY = 0.00;
                Array tempX = new object[] {product.timeRetreived.ToString()};
                if(!product.price.Contains("Error"))
                {
                    //Y value needs to be a number: 
                    //can ignore the dollar sign: https://docs.microsoft.com/en-us/dotnet/api/system.double.parse?redirectedfrom=MSDN&view=netframework-4.8#System_Double_Parse_System_String_System_Globalization_NumberStyles_ 
                    tempY =  double.Parse(product.price, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    initX.Add(tempX);
                    innitY.Add(tempY); 
                }                
            }
            //new list for storage: 
            var Xaxis = initX.ToList();
            var Yaxis = innitY.ToList();
            //serialize into JSON and store in a string: 
            chartXAxis = JsonConvert.SerializeObject(Xaxis);
            chartYAxis = JsonConvert.SerializeObject(Yaxis);
            return Page();
        }

        /*
        HTTP POST:
        */

        public async Task<IActionResult> OnPostDelete(int? id, int? parentId)
        {
            _logger.LogInformation("OnPostDelete Called...");
            if (id == null || parentId == null)
            {
                return NotFound();
            }
            ProductDetails = await _context.ProductMeta.Include(p => p.products)
                .AsNoTracking().FirstOrDefaultAsync(pd => pd.Id == parentId);
            Product itemToDelete = await _context.Product.FindAsync(id);
            try
            {
                _context.Product.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("./ProductPage?id=" + ProductDetails.Id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting obj");
                return RedirectToPage("./ProductPage?id=" + ProductDetails.Id);
            }
        }
        public async Task<IActionResult> OnPostUpdate(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductDetails = await _context.ProductMeta.FirstOrDefaultAsync(pd => pd.Id == id);
            ProductDetails.NameHtmlId = nameHtmlId;
            ProductDetails.PriceHtmlId = priceHtmlId;
            ProductDetails.ProductUrl = productUrl;
            ProductDetails.VanityName = vanityName; 
            await _context.SaveChangesAsync();
            return RedirectToPage("./SavedProducts");
        }
    }
}