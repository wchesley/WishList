using System; 

namespace WishList
{
    public class Product
    {
        public int Id {get; set;}
        public DateTime timeRetreived {get; set;}
        public string price {get; set;}
        public string name {get; set;}
        public ProductMeta productMeta {get; set;}
    }
}