using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaserArt.DAO;

namespace LaserArt.Models
{
    public class ParentCategory
    {
        public int? Id { get; set; }
        public string ParentCategoryName { get; set; }
        public string ImageSource { get; set; }
        public List<Category> Childs { get {
                return ParentCategoryDAO.getChildCategories(this.Id);

            }
            
        }
        public static List<ParentCategory> GetCategories(int? id)
        {
            return ParentCategoryDAO.getCategories(id);
        }
        //public List<Category> Childs()
        //{
        //    return ParentCategoryDAO.getChildCategories(this.Id);
        //}
        public ParentCategory SaveParentCategory()
        {
            return ParentCategoryDAO.saveParentCategory(this);
        }

        public static void DeleteParentCategory(int id)
        {
            ParentCategoryDAO.DeleteParentCategoryByID(id);
        }
    }
}
