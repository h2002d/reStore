using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class CardModel
    {
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public ProductSpecification Specification { get; set; }
        public string Color { get; set; }
        public Product product { get; set; }
        public CardModel()
        {
            Specification = new ProductSpecification();
            product = new Product();
        }
    }
}