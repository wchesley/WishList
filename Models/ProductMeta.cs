using System; 
using System.Collections.Generic;

namespace WishList
{
    public class ProductMeta
    {
        public int Id {get; set;}
        public string ProductUrl {get; set;}
        public string PriceHtmlId {get; set;}
        public string NameHtmlId {get; set;}
        public ICollection<Product> product {get; set;}
    }
}