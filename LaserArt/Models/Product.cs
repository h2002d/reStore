using LaserArt.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class Product
    {
        public int? Id { get; set; }
        [Required]
        public string ProductTitle { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal PriceDiscounted { get; set; }
        [Required]
        public string ImageSource { get; set; }
        [DefaultValue("")]
        public string ImageSource1 { get; set; } = "";
        [DefaultValue("")]

        public string ImageSource2 { get; set; } = "";
        [DefaultValue("")]

        public string ImageSource3 { get; set; } = "";
        [Required]
        public int CategoryId { get; set; }

        public Product():base()
        {
            this.ImageSource1 = "";
            this.ImageSource2 = "";
            this.ImageSource3 = "";
        }
        public Product SaveProduct()
        {
            return ProductDAO.saveProduct(this);
        }

        public static List<Product> GetProducts(int? id)
        {
            return ProductDAO.getProducts(id);
        }
        public static void DeleteProduct(int id)
        {
             ProductDAO.deleteProduct(id);
        }
        public static List<Product> GetProductsByCategoryId(int categoryId)
        {
            return ProductDAO.getProductsByCategoryId(categoryId);
        }
        public static List<Product> GetProductsByQuery(string query)
        {
            return ProductDAO.getProductsByQuery(query);
        }
        public static List<CardModel> GetProductsByOrderId(int orderId)
        {
            return ProductDAO.getProductsByOrderId(orderId);
        }
    }
}