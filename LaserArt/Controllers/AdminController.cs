using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaserArt.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {
            ViewBag.Categories = LaserArt.Models.Category.GetCategories(null);
        }
        // GET: Admin
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Order()
        {
            var model=Models.Order.GetOrderById(null);
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult OrderDetails(int OrderId)
        {
            
            var product = Product.GetProductsByOrderId(OrderId);
            return View(product);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult ChangeOrderStatus(int orderId,int status)
        {
            Models.Order.ChangeOrderStatus(orderId, status);
           return RedirectToAction("Order");
        }
    }
}