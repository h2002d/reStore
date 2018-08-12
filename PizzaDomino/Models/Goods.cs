using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaDomino.Models
{
    public class Goods
    {
        #region Properties
        public int? Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageSource { get; set; }


        static DAO.GoodsDAO DAO = new PizzaDomino.DAO.GoodsDAO();
        #endregion
        public void Save()
        {
            DAO.saveGood(this);
        }

        public List<Ingredients> Ingredients
        {
            get { return Models.Ingredients.GetIngredientsByGoodId(Convert.ToInt32(Id)); }
        }

        public List<Size> Sizes
        {
            get { return Models.Size.GetSizeByGoodId(Convert.ToInt32(Id)); }
        }

        public static List<Goods> GetGoodById(int id)
        {
            return DAO.getGoodById(id);
        }

        public static List<Goods> GetGoodsByCategoryId(int id)
        {
            return DAO.getGoodByCategoryId(id);
        }
    }

    public class Ingredients
    {
        public int? IngredientId { get; set; }
        public int GoodId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        static DAO.IngredientDAO DAO = new PizzaDomino.DAO.IngredientDAO();

        public void Save()
        {
            DAO.saveIngredient(this);
        }

        public static List<Ingredients> GetIngredientsById(int id)
        {
            return null;
        }

        public static List<Ingredients> GetIngredientsByGoodId(int GoodId)
        {
            return null;
        }
    }

    public class Size
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public static List<Size> GetSizeById(int id)
        {
            return null;
        }

        public static List<Size> GetSizeByGoodId(int GoodId)
        {
            return null;
        }
    }
}