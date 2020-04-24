using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishList
{
    public class Product
    {
        public int Id {get; set;}
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime timeRetreived {get; set;}
        [Required]
        [MinLength(1)]
        public string price {get; set;}
        [Required]
        [MinLength(1)]
        public string name {get; set;}
        public ProductMeta productMeta {get; set;}
    }
}