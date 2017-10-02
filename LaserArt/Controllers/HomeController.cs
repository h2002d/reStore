using ImageResizer;
using LaserArt.Models;
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
using System.Web.Routing;
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
            Dictionary<string, List<Product>> categoryList = new Dictionary<string, List<Models.Product>>();
           
            var recomended = LaserArt.Models.Product.GetProducts(null).Take(3).ToList();
            categoryList.Add("Re:Store-Family рекомендует:", recomended);
            var categories = Models.Category.GetCategories(null);
            foreach(var category in categories)
            {
                var productsList = Models.Product.GetProductsByCategoryId(Convert.ToInt32(category.Id)).Take(6).ToList();
                categoryList.Add(category.CategoryName, productsList);
            }
            ViewBag.Sales = Models.Sales.GetSalesById(null);
            return View(categoryList);
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
        [ValidateInput(false)]
        public ActionResult EditProduct(int id)
        {
            Product product = Models.Product.GetProducts(id).FirstOrDefault();
            ViewBag.Categories = LaserArt.Models.Category.GetCategories(null);
            return View("CreateProduct", product);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult CreateProduct(Product newProduct)
        {
            try
            {
                newProduct.ImageSource1 = newProduct.ImageSource1 == null ? "" : newProduct.ImageSource1;
                newProduct.ImageSource2 = newProduct.ImageSource2 == null ? "" : newProduct.ImageSource1;
                newProduct.ImageSource3 = newProduct.ImageSource3 == null ? "" : newProduct.ImageSource1;

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
        [ValidateInput(false)]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult EditCategory(int id)
        {
            Category category = Models.Category.GetCategories(id).FirstOrDefault();
            return View("CreateCategory", category);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult DeleteCategory(int id)
        {
            Models.Category.DeleteCategory(id);
            return RedirectToAction("Index");
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
            ResizeSettings resizeSetting = new ResizeSettings
            {
                Width = 300,
                Height = 300,
                Format = file.FileName.Split('.')[1]
            };
            ImageBuilder.Current.Build(path, path, resizeSetting);
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

        [HttpPost]
        public JsonResult FileUploadCarousel()
        {
            HttpPostedFile file = null;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                file = System.Web.HttpContext.Current.Request.Files["HttpPostedFileBase"];
            }
            string pic = System.IO.Path.GetFileName(file.FileName);
            string path = System.IO.Path.Combine(
                                   Server.MapPath("~/images/Carousel"), pic);
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
                var model = JsonConvert.DeserializeObject<List<CardModel>> (value);
                int index = model.FindIndex(item => item.ProductId == productId);
                if (index < 0)
                {
                    CardModel newItem = new CardModel();
                    newItem.ProductId = productId;
                    newItem.ProductQuantity = quantity;
                    model.Add(newItem);
                }
                cookieOld.Value = JsonConvert.SerializeObject(model);
                Response.Cookies.Add(cookieOld);
            }
            else
            {
                List<CardModel> idList = new List<CardModel>();
                CardModel model = new CardModel();
                model.ProductId = productId;
                model.ProductQuantity = quantity;
                idList.Add(model);
                var json = JsonConvert.SerializeObject(idList);
                HttpCookie cookie = new HttpCookie("Cart");
                cookie.Value = json;
                Response.Cookies.Add(cookie);
            }

            return Json("Добавлено в карзину.", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Card()
        {
            List<CardModel> productList = new List<CardModel>();
            decimal sum = 0;
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];

                var value = cookieOld.Value;

                var idList = JsonConvert.DeserializeObject<List<CardModel>>(value);


                foreach (var item in idList)
                {
                    item.product= LaserArt.Models.Product.GetProducts(Convert.ToInt32(item.ProductId)).FirstOrDefault();
                    productList.Add(item);
                    sum += item.ProductQuantity*item.product.Price;
                }

            }
            List<Models.Product> prod = new List<Models.Product>();
            var recommendedList= Models.Product.GetProducts(null).Take(18);
            List<int> randomList = new List<int>();
            Random rnd = new Random();
            while (true)
            {
              
                int r = rnd.Next(recommendedList.Count());
                if (!randomList.Contains(r))
                    randomList.Add(r);
                if (randomList.Count >= 3)
                    break;
            }
            foreach(int item in randomList)
            {
                prod.Add(recommendedList.ElementAt(item));
            }
            ViewBag.Recommended = prod;
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
                var model = JsonConvert.DeserializeObject<List<CardModel>>(value);

                model.Remove(model.Where(m=>m.ProductId==id).First());

                cookieOld.Value = JsonConvert.SerializeObject(model);
                Response.Cookies.Add(cookieOld);
            }

            return RedirectToAction("Card");
        }

        [HttpPost]
        public ActionResult RemoveProduct(int id)
        {
            Models.Product.DeleteProduct(id);
            return Json("Телефон удален.",JsonRequestBehavior.AllowGet);
        }

        public ActionResult Order(int? id)
        {
            List<CardModel> productList = new List<CardModel>();
            decimal sum = 0;
            if (id == null)
            {

                if (Request.Cookies["Cart"] != null)
                {
                    HttpCookie cookieOld = Request.Cookies["Cart"];

                    var value = cookieOld.Value;

                    var idList = JsonConvert.DeserializeObject<List<CardModel>>(value);
                    foreach (var item in idList)
                    {

                        item.product = LaserArt.Models.Product.GetProducts(Convert.ToInt32(item.ProductId)).FirstOrDefault();
                        productList.Add(item);
                        sum += item.ProductQuantity * item.product.Price;

                    }

                }
            }
            else
            {
                ViewBag.ListInt = id;
                CardModel model = new CardModel();
                model.product = LaserArt.Models.Product.GetProducts(id).FirstOrDefault();
                model.ProductId = Convert.ToInt32(id);
                model.ProductQuantity = 1;
                productList.Add(model);
                sum += model.product.Price;
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
                    var idList = JsonConvert.DeserializeObject<List<CardModel>>(value);
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
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];

                var value = cookieOld.Value;
                var idList = JsonConvert.DeserializeObject<List<CardModel>>(value);
                foreach (var item in idList)
                {
                    newOrder.Products.Add(item);
                }
            }
            newOrder.Id = newOrder.saveOrder();
            SendMail(newOrder);
            return RedirectToAction("OrderAproval", new { id = newOrder.Id });
        }
        public ActionResult OrderAproval(int id)
        {
            ViewBag.OrderId = id;
            return View();
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
                   var product= Models.Product.GetProducts(item.ProductId).FirstOrDefault();
                    Body.Append(string.Format("Название товара:N{0} {1} <br/>", item.ProductId, product.ProductTitle));
                        }
                Body.Append(string.Format("Дата заказа: {0}", DateTime.Now));
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
        public ActionResult Search(string query)
        {
            var listProducts = Models.Product.GetProductsByQuery(query);
            ViewBag.Sales = Models.Sales.GetSalesById(null);
            ViewBag.Info = "Результаты поиска:";
            return View(listProducts);
        }
        public ActionResult Sales()
        {
            var sales = Models.Sales.GetSalesById(null);
            return View(sales);
        }

        public ActionResult SalesDetails(int? id)
        {
            var sale = Models.Sales.GetSalesById(id).FirstOrDefault();
            return View(sale);
        }
        [HttpPost]
        public ActionResult SalesDetails(Sales sale)
        {
            try
            {
                sale.SaveSales();
                return RedirectToAction("Sales");
            }
            catch(Exception ex)
            {
                return RedirectToAction("SalesDetails",sale.Id);

            }

          
           

        }
    }
}