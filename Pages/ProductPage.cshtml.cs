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
        public string vanityName { get; set; }

        public ProductPage(ILogger<ProductPage> logger, ProductContext context)
        {
            _context = context;
            _logger = logger;
        }

        /*
        HTTP GET:
        */
        ///<summary>
        ///OnGet for ProductMeta found by ID. deletedid is only used when 
        ///OnPostDelete() is called. It allows that method to redirect back to 
        ///this page. 
        ///</summary>
        public async Task<IActionResult> OnGetAsync(int? id, int? deletedid)
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
            PoputlateGraph(); 
            return Page();
        }

        /*
        HTTP POST:
        */

        ///<summary>
        ///OnPost method for deleting one specific scraped 
        ///item within a product detail page. Redirects back to Product Detail page.
        ///</summary>
        public async Task<IActionResult> OnPostDelete(int? parentId, int? id)
        {
            _logger.LogInformation($"OnPostDelete Called...\n");
            if (id == null)
            {
                _logger.LogError("ID is null");
                return NotFound();
            }
            

            ProductDetails = await _context.ProductMeta.Include(p => p.products)
                .AsNoTracking().FirstOrDefaultAsync(pd => pd.Id == parentId);
 
            Product itemToDelete = ProductDetails.products.Where(p => p.Id == id).First();
            //ModelState is invalid when deleting individual products: 
            // if (!ModelState.IsValid)
            // {
            //     _logger.LogError("Model State invalid!");
            //     return Page();
            // }
            ViewData["deletedProductName"] = itemToDelete.name;
            ViewData["deletedProduct"] = itemToDelete.Id; 

            try
            {
                _context.Product.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                ProductsList = ProductDetails.products.ToList();
                PoputlateGraph();
                _logger.LogInformation($"removed: {itemToDelete.Id} : {itemToDelete.name}"); 
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting obj");
                return Page();
            }
        }

        ///<summary>
        ///OnPost for updating ProductMeta item 
        ///</summary>
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

        ///<summary>
        ///Method used to add data for graphing within product detail page.
        ///if nothing is within the ProductList property, this method passes up 
        ///default values so the page won't error out.  
        ///</summary>
        public void PoputlateGraph()
        {
            if (ProductsList == null)
            {
                chartXAxis = JsonConvert.SerializeObject("No information");
                chartYAxis = JsonConvert.SerializeObject("0");
                return;
            }
            //convert data to JSON for Chart: 
            //prep two lists for x and y data: 
            List<Array> initX = new List<Array>();
            List<double> innitY = new List<double>();
            foreach (var product in ProductsList)
            {
                double tempY = 0.00;
                Array tempX = new object[] { product.timeRetreived.ToString() };
                if (!product.price.Contains("Error"))
                {
                    //Y value needs to be a number: 
                    //rather than have .NET remove the '$', I've opted to do this my self:
                    double.TryParse(product.price.Substring(1), out tempY); 
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
        }
    }
}