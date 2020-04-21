using System;
using System.Linq;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace WishList
{
    public class scrape
    {
        private readonly ProductContext _context;
        private readonly ILogger<scrape> _logger;
        public scrape(ProductContext context, ILogger<scrape> logger)
        {
            _context = context;
            _logger = logger;
        }
        public scrape()
        {
        }
        public ChromeDriver createBrowser()
        {
            var options = new ChromeOptions();
            options.AddArguments("whitelisted-ips=''", "headless");
            ChromeDriver browser = new ChromeDriver("./", options);
            //give pages 30 seconds to respond before timeing out: 
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return browser;
        }
        public void getPrice(string url, string titleId, string priceId, ChromeDriver browser, int productId)
        {
            //the web can be a tricky, unreliable place, best to wrap this in a try/catch block: 
            try
            {
                browser.Navigate().GoToUrl(url);

                var timeStamp = DateTime.Now;
                var Price = browser.FindElementById(priceId).Text;
                var title = browser.FindElementById(titleId).Text;
                Console.WriteLine($"Object Found:\nName:{title}\nPrice:{Price}\nURL:{url}\nTime:{timeStamp.ToString()}");
                var thing = Program.globalContext.ProductMeta.Find(productId);
                var foundProduct = new Product
                {
                    timeRetreived = timeStamp,
                    price = Price,
                    name = title,
                };
                thing.products.Add(foundProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();
        }

        public IActionResult Scrape()
        {
            var services = Program.globalContext; 
            var browser = createBrowser();
            Console.WriteLine("Init Browser..."); 
            try
            {
                var products = services.ProductMeta.ToList();
                foreach (var item in products)
                {
                    Console.WriteLine($"Object Found:\nName:{item.NameHtmlId}\nPrice:{item.PriceHtmlId}\nURL:{item.ProductUrl}");
                    getPrice(item.ProductUrl, item.NameHtmlId, item.PriceHtmlId, browser, item.Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                browser.Close();
            }
            services.SaveChanges();
            browser.Close();
            return null; 
        }
    }
}
/*
url: https://www.amazon.com/FIFINE-Microphone-Adjustable-Instruments-Streaming-T669/dp/B07Y1C6GDS/ref=sr_1_2?dchild=1&keywords=fifine&qid=1587310409&sr=8-2
priceId: priceblock_ourprice
titleId: productTitle
*/
