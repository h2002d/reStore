using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class CardModel
    {
        public int ProductId { get; set; }
        public decimal ProductQuantity { get; set; }
        public Product product { get; set; }
        public CardModel()
        {
            product = new Product();
        }
        public DeliveryCity city { get; set; }
    }

    public class DeliveryCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }

        public static List<DeliveryCity> GetDelivery(int? id)
        {
            return DAO.OrderDAO.getDeliveryCity(id);
        }
    }
}