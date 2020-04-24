using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace WishList
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductContext(serviceProvider.GetRequiredService<DbContextOptions<ProductContext>>()))
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                if (context.ProductMeta.Any())
                {
                    logger.LogInformation("Database  is already seeded...");
                    return;
                }
                context.ProductMeta.Add(
                    new ProductMeta
                    {
                        Id = 1,
                        ProductUrl = "https://www.amazon.com/FIFINE-Microphone-Adjustable-Instruments-Streaming-T669/dp/B07Y1C6GDS/ref=sr_1_2?dchild=1&keywords=fifine&qid=1587310409&sr=8-2",
                        PriceHtmlId = "priceblock_ourprice",
                        NameHtmlId = "productTitle",
                        VanityName = "Seed Data",
                    }
                );
                context.SaveChanges();
                context.Product.AddRange(
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/05/2020 12:14:40 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/06/2020 01:05:04 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/06/2020 08:33:33 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/06/2020 09:13:12 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/07/2020 08:22:16 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/09/2020 08:27:03 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/14/2020 12:49:04 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/17/2020 07:14:28 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/19/2020 03:51:46 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/20/2020 03:27:18 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/20/2020 08:24:12 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/29/2020 03:55:53 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("01/30/2020 03:22:24 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("02/16/2020 06:49:38 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("02/20/2020 12:53:25 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("03/03/2020 08:27:28 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("03/16/2020 07:43:39 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("03/30/2020 01:02:59 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("04/01/2020 10:16:20 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("04/06/2020 02:13:28 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("04/11/2020 11:26:19 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("04/18/2020 02:03:15 PM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("04/22/2020 10:50:45 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    },
                    new Product
                    {
                        timeRetreived = DateTime.Parse("04/23/2020 12:21:18 AM"),
                        price="66.99",
                        name="FIFINE Studio Condenser USB Microphone Computer PC Microphone Kit with Adjustable Scissor Arm Stand Shock Mount for Instruments Voice Overs Recording Podcasting YouTube Karaoke Gaming Streaming-T669 ",
                        productMeta = context.ProductMeta.Where(p => 1 == p.Id).First(), 
                    }

                );
                context.SaveChanges(); 
            }
        }
    }
}