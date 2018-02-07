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
        #region Properties
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
        [DefaultValue("")]
        public string ImageSource4 { get; set; } = "";
        [DefaultValue("")]
        public string ImageSource5 { get; set; } = "";
        [DefaultValue("")]
        public string ImageSource6 { get; set; } = "";
        [Required]
        public string Color { get; set; }
        [DefaultValue("")]
        public string Color1 { get; set; } = "";
        [DefaultValue("")]
        public string Color2 { get; set; } = "";
        [DefaultValue("")]
        public string Color3 { get; set; } = "";
        [DefaultValue("")]
        public string Color4 { get; set; } = "";
        [DefaultValue("")]
        public string Color5 { get; set; } = "";
        [DefaultValue("")]
        public string Color6 { get; set; } = "";
        [Required]
        public int CategoryId { get; set; }

        public ProductSpecification Specification { get; set; }

        public Dictionary<string,string> Colors { get; set; }
#endregion

        public Product():base()
        {
            Colors = new Dictionary<string, string>();
            Colors.Add("c-black-onyx-full", "Чёрный оникс");
            Colors.Add("c-grey-full", "Тёмно серый");
            Colors.Add("c-black-full", "Чёрный");
            Colors.Add("c-gold-full", "Золотой");
            Colors.Add("c-rose-full", "Розовое Золото");
            Colors.Add("c-white-full", "Белый");
            Colors.Add("c-red-full", "Красный");

            Specification = new ProductSpecification();
            this.ImageSource1 = "";
            this.ImageSource2 = "";
            this.ImageSource3 = "";
            this.ImageSource4 = "";
            this.ImageSource5 = "";
            this.ImageSource6 = "";
            this.Color1 = "";
            this.Color2 = "";
            this.Color3 = "";
            this.Color4 = "";
            this.Color5 = "";
            this.Color6 = "";
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