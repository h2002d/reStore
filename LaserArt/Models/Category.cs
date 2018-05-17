using LaserArt.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public string ImageSource { get; set; }
        public static List<Category> GetCategories(int? id)
        {
            return CategoryDAO.getCategories(id);
        }

        public Category SaveCategory()
        {
            return CategoryDAO.saveCategory(this);
        }

        public static void DeleteCategory(int id)
        {
            CategoryDAO.DeleteCategoryByID(id);
        }
    }
}