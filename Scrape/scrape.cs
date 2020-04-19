using System;
using System.Linq;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace WishList
{
    class scrape : IJob
    {
        private readonly ProductContext _context;
        private readonly ILogger<scrape> _logger;
        public scrape(ProductContext context, ILogger<scrape> logger)
        {
            _context = context;
            _logger = logger;
        }
        public ChromeDriver createBrowser()
        {
            var options = new ChromeOptions();
            options.AddArguments("w0hitelisted-ips=''", "headless");
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
                int.TryParse(Price, out int newPrice); 
                _logger.LogInformation($"Object Found:\nName:{title}\nPrice:{Price}\nURL:{url}\nTime:{timeStamp.ToString()}");
                var foundProduct = new Product {
                    timeRetreived = timeStamp,
                    price = newPrice, 
                    name = title
                };
                var thing = _context.ProductMeta.Find(productId);
                thing.product.Add(foundProduct); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            
            _logger.LogInformation($"Begin Scheduled Task: {DateTime.Now.ToString()}");
            //expensive operation, we only want to do this once:
            var browser = createBrowser(); 
            
            var products = _context.ProductMeta.ToList();
            foreach(var product in products)
            {
                getPrice(product.ProductUrl, product.NameHtmlId, product.PriceHtmlId, browser, product.Id); 
            }
            //save changes in one go? 
            _context.SaveChanges(); 
            //dispose of browser to conserve resources: 
            browser.Close();
            //have to return something? 

        }
    }
}
/*
url: https://www.amazon.com/FIFINE-Microphone-Adjustable-Instruments-Streaming-T669/dp/B07Y1C6GDS/ref=sr_1_2?dchild=1&keywords=fifine&qid=1587310409&sr=8-2
priceId: priceblock_ourprice
titleId: productTitle
*/
