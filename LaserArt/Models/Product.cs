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
        public int MeasureId { get; set; }

        public bool IsSpecial { get; set; }
        public bool IsOut { get; set; }
        public Measure Measure { get { try { return Measure.GetMeasure(MeasureId).First(); } catch { return null; } } }
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
        public int CategoryId { get; set; }

        public Dictionary<string,string> Colors { get; set; }
#endregion

        public Product():base()
        {

            this.ImageSource1 = "";
            this.ImageSource2 = "";
            this.ImageSource3 = "";
            this.ImageSource4 = "";
            this.ImageSource5 = "";
            this.ImageSource6 = "";
         
        }

        public Product SaveProduct()
        {
            return ProductDAO.saveProduct(this);
        }

        public static List<Product> GetProducts(int? id)
        {
            return ProductDAO.getProducts(id);
        }

        public static List<Product> GetOutProducts(int? id)
        {
            return ProductDAO.getOutProducts(id);
        }

        public static List<Product> GetSpecialProducts(int? id)
        {
            return ProductDAO.getSpecialProducts(id);
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

        internal static List<Product> GetDiscountedProducts()
        {
            return ProductDAO.getDiscountedProducts();
        }
    }
}