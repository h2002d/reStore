using LaserArt.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<int> ProductId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Latitide { get; set; }
        public string Longitude { get; set; }
        public bool isCash { get; set; }
        public int Status { get; set; } //0 open,1 shipped,2closed
        public List<CardModel> Products{ get; set; }
        public int CityId { get; set; }
        public DeliveryCity city { get { return DeliveryCity.GetDelivery(this.CityId).First(); } }
public decimal Amount { get; set; }
        public Order()
        {
            Products = new List<CardModel>();
        }

        public int saveOrder()
        {
          return  OrderDAO.saveOrder(this);
        }

        public static List<Order> GetOrderById(int? id)
        {
            return OrderDAO.getOrdersById(id);
        }
        public static void ChangeOrderStatus(int orderId,int status)
        {
            OrderDAO.changeOrderStatus(orderId, status); 
        }
    }
}