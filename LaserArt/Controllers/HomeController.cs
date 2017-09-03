﻿using LaserArt.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LaserArt.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
           
            ViewBag.Categories = LaserArt.Models.Category.GetCategories(null);
        }
        public ActionResult Index()
        {
            var products = LaserArt.Models.Product.GetProductsByCategoryId(1);
            ViewBag.Sales = Sales.GetSalesById(null);
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateProduct()
        {
            ViewBag.Categories = LaserArt.Models.Category.GetCategories(null);
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditProduct(int id)
        {
            Product product = Models.Product.GetProducts(id).FirstOrDefault();
            ViewBag.Categories = LaserArt.Models.Category.GetCategories(null);
            return View("CreateProduct", product);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateProduct(Product newProduct)
        {
            try
            {
                newProduct.SaveProduct();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateProduct");
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditCategory(int id)
        {
            Category category = Models.Category.GetCategories(id).FirstOrDefault();
            return View("CreateCategory", category);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateCategory(Category newCategory)
        {
            try
            {
                newCategory.SaveCategory();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateCategory");
            }
        }
        public ActionResult Product(int id)
        {
            var product = LaserArt.Models.Product.GetProducts(id).FirstOrDefault();
            return View(product);
        }

        public ActionResult Category(int id)
        {
            var category = LaserArt.Models.Category.GetCategories(id).FirstOrDefault();
            ViewBag.CategoryName = category.CategoryName;
            ViewBag.CategoryId = id;
            var products = LaserArt.Models.Product.GetProductsByCategoryId(id);
            return View(products);
        }
        [HttpPost]
        public JsonResult FileUpload()
        {
            HttpPostedFile file = null;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                file = System.Web.HttpContext.Current.Request.Files["HttpPostedFileBase"];
            }
            string pic = System.IO.Path.GetFileName(file.FileName);
            string path = System.IO.Path.Combine(
                                   Server.MapPath("~/images"), pic);
            // file is uploaded
            file.SaveAs(path);

            // save the image path path to the database or you can send image 
            // directly to database
            // in-case if you want to store byte[] ie. for DB
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
            }


            // after successfully uploading redirect the user
            return Json("Картинка загружена", JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult AddToCard(int productId,int quantity)
        {
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];
                var value = cookieOld.Value;
                var model = JsonConvert.DeserializeObject<List<int>>(value);
                if (!model.Contains(productId))
                    model.Add(productId);

                cookieOld.Value = JsonConvert.SerializeObject(model);
                Response.Cookies.Add(cookieOld);
            }
            else
            {
                List<int> idList = new List<int>();
                idList.Add(productId);
                var json = JsonConvert.SerializeObject(idList);
                HttpCookie cookie = new HttpCookie("Cart");
                cookie.Value = json;
                Response.Cookies.Add(cookie);
            }

            return Json("Добавлено в карзину.", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Card()
        {
            List<Product> productList = new List<Models.Product>();
            decimal sum = 0;
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];

                var value = cookieOld.Value;

                var idList = JsonConvert.DeserializeObject<List<int>>(value);


                foreach (var item in idList)
                {
                    var product = LaserArt.Models.Product.GetProducts(Convert.ToInt32(item)).FirstOrDefault();
                    productList.Add(product);
                    sum += product.Price;
                }

            }
            ViewBag.Sum = sum;
            return View(productList);
        }
        [HttpPost]
        public ActionResult RemoveCard(int id)
        {
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];
                var value = cookieOld.Value;
                var model = JsonConvert.DeserializeObject<List<int>>(value);

                model.Remove(id);

                cookieOld.Value = JsonConvert.SerializeObject(model);
                Response.Cookies.Add(cookieOld);
            }

            return RedirectToAction("Card");
        }

        public ActionResult Order(int? id)
        {
            List<Product> productList = new List<Models.Product>();
            decimal sum = 0;
            if (id == null)
            {

                if (Request.Cookies["Cart"] != null)
                {
                    HttpCookie cookieOld = Request.Cookies["Cart"];

                    var value = cookieOld.Value;

                    var idList = JsonConvert.DeserializeObject<List<int>>(value);
                    foreach (var item in idList)
                    {
                        var product = LaserArt.Models.Product.GetProducts(Convert.ToInt32(item)).FirstOrDefault();
                        productList.Add(product);
                        sum += product.Price;
                    }

                }
            }
            else
            {
                ViewBag.ListInt = id;

                var product = LaserArt.Models.Product.GetProducts(id).FirstOrDefault();
                productList.Add(product);
                sum += product.Price;
            }
            ViewBag.Sum = sum;
            return View(productList);
        }

        [HttpPost]
        public JsonResult AcceptOrder(Order newOrder)
        {
            newOrder.Latitide = "0";
            newOrder.Longitude = "0";
            newOrder.saveOrder();
            return Json("Заказ оформлен, наши сотрудники свяжутся с вами в течении часа.", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult OrderDetails(int? id)
        {
            Order newOrder = new Order();
            if (id == null)
            {

                if (Request.Cookies["Cart"] != null)
                {
                    HttpCookie cookieOld = Request.Cookies["Cart"];

                    var value = cookieOld.Value;
                    var idList = JsonConvert.DeserializeObject<List<int>>(value);
                    foreach (var item in idList)
                    {
                        newOrder.Products.Add(item);
                    }
                }
            }
            else
            {

            }
            return View(newOrder);
        }

        [HttpPost]
        public ActionResult OrderDetails(Order newOrder)
        {
            newOrder.Latitide = "0";
            newOrder.Longitude = "0";
           newOrder.Id= newOrder.saveOrder();
            SendMail(newOrder);
            return RedirectToAction("Index");
        }

        public void SendMail(Order newOrder)
        {
            try
            {
                string EmailTo = WebConfigurationManager.AppSettings["EmailTo"];
                string EmailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string EmailFromPassword = WebConfigurationManager.AppSettings["EmailFromPassword"];

                MailMessage mail = new MailMessage();
                mail.To.Add(EmailTo);
                mail.From = new MailAddress(EmailFrom);
                mail.Subject = "НОВЫЙ ЗАКАЗ!";
                StringBuilder Body = new StringBuilder();
                Body.Append("<b>У вас есть новый заказ</b><br/>")
                    .Append(string.Format("Номер заказа:{0} <br/>", newOrder.Id))
                    .Append(string.Format("Адрес:{0} <br/>", newOrder.Address))

                    .Append(string.Format("Имя зказчика:{0} {1} <br/>", newOrder.Name, newOrder.SurName));
                    foreach (var item in newOrder.Products) {
                   var product= Models.Product.GetProducts(item).FirstOrDefault();
                    Body.Append(string.Format("Название товара:N{0} {1} <br/>", item, product.ProductTitle));
                        }
                Body.Append(string.Format("Вреия заказа: {0}", DateTime.Now));
                mail.Body = Body.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(EmailFrom, EmailFromPassword); // Enter seders User name and password   
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}