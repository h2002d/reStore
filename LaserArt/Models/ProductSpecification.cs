using LaserArt.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class ProductSpecification
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        public string SpecificationName { get; set; }
        public decimal Price { get; set; }
        static ProductSpecificationDAO DAO = new ProductSpecificationDAO();

        public static List<ProductSpecification> GetProductSpecification(int id)
        {
            return DAO.getProductSpecification(id);
        }

        public static List<ProductSpecification> GetProductSpecificationById(int id)
        {
            return DAO.getProductSpecificationById(id);
        }

        public void Save()
        {
            DAO.saveProduct(this);
        }
        public static void Delete(int id)
        {
            DAO.DeleteSpecificationByID(id);
        }
    }
}