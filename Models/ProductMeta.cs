using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishList
{
    public class ProductMeta
    {
        public int Id {get; set;}
        [DataType(DataType.Url)]
        [Required]
        public string ProductUrl {get; set;}
        [MinLength(1)]
        [Required]
        public string PriceHtmlId {get; set;}
        [MinLength(1)]
        [Required]
        public string NameHtmlId {get; set;}
        public List<Product> products {get; set;} = new List<Product>();
    }
}