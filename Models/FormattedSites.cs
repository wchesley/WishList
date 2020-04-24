using System;
using System.Collections.Generic;

namespace WishList
{
    public class FormattedSites
    {
        //Dictonary for sites that have similar structure for all product pages, should be most everyone: 
        //formattedSitesDict will have a list of keys which are named after the website; ie. www.amazon.com is just amazon in the dictionary
        //The list of values will always carry the same order; Price first, then Title. it will be passed to the scraper in that order!
        //This implementation should allow users to only specify the URL for certain sites; making adding products easier. 
        //The Key will be what is displayed on the Create form
        public Dictionary<string, List<string>> formattedSitesDict = new Dictionary<string, List<string>>();

        public FormattedSites()
        {
            List<string> AmazonPageObjs = new List<string>();
            AmazonPageObjs.Add("priceblock_ourprice");
            AmazonPageObjs.Add("productTitle");
            List<string> NeweggPageObjs = new List<string>();
            NeweggPageObjs.Add("grpDescrip_h");
            NeweggPageObjs.Add("price-current");
            formattedSitesDict.Add("Amazon", AmazonPageObjs);
            formattedSitesDict.Add("Newegg", NeweggPageObjs); 
        }
    }
}