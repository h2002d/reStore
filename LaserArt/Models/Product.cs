using LaserArt.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public decimal PriceDiscounted { get; set; }
        public string ImageSource { get; set; }
        public int CategoryId { get; set; }

        public Product SaveProduct()
        {
            return ProductDAO.saveProduct(this);
        }

        public static List<Product> GetProducts(int? id)
        {
            return ProductDAO.getProducts(id);
        }

        public static List<Product> GetProductsByCategoryId(int categoryId)
        {
            return ProductDAO.getProductsByCategoryId(categoryId);
        }
        public static List<Product> GetProductsByOrderId(int orderId)
        {
            return ProductDAO.getProductsByOrderId(orderId);
        }
    }
}