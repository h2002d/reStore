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
            ViewBag.Categories = Models.ParentCategory.GetCategories(null);

        }

        public ActionResult Index(int? card)
        {
            Dictionary<Category, List<Product>> categoryList = new Dictionary<Category, List<Models.Product>>();
            List<Models.Product> prod = Models.Product.GetSpecialProducts(null).Take(5).ToList();
            //var recommendedList = Models.Product.GetProducts(null).Take(18);
            //List<int> randomList = new List<int>();
            //Random rnd = new Random();
            //if (recommendedList.Count() > 5)
            //{
            //    while (true)
            //    {


            //        int r = rnd.Next(recommendedList.Count());
            //        if (!randomList.Contains(r))
            //            randomList.Add(r);
            //        if (randomList.Count >= 5)
            //            break;
            //    }

            //    foreach (int item in randomList)
            //    {

            //        prod.Add(recommendedList.ElementAt(item));
            //    }
            //}
            ViewBag.ShowCard = card;
            ViewBag.Recommended = prod;
            Category recom = new Models.Category();
            recom.CategoryName = "Հատուկ առաջարկներ";
            categoryList.Add(recom, prod);
            var categories = Models.Category.GetCategories(null);
            foreach (var category in categories)
            {
                var productsList = Models.Product.GetProductsByCategoryId(Convert.ToInt32(category.Id)).Take(5).ToList();
                categoryList.Add(category, productsList);
            }
            ViewBag.Categories = Models.ParentCategory.GetCategories(null);

            ViewBag.Sales = Models.Sales.GetSalesById(null);
            return View(categoryList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ViewResult NotFound()
        {
            return View();
        }
        public ActionResult SubCategory(int id)
        {
            var category = Models.ParentCategory.GetCategories(id).First();
            return View(category); 
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
            ViewBag.Measure = LaserArt.Models.Measure.GetMeasure(null);
            ViewBag.Category = LaserArt.Models.Category.GetCategories(null);
            Models.Product prod = new Models.Product();
            return View(prod);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult EditProduct(int id)
        {
            Product product = new Models.Product();
            product = Models.Product.GetProducts(id).FirstOrDefault();
            ViewBag.Measure = LaserArt.Models.Measure.GetMeasure(null);
            ViewBag.Category = LaserArt.Models.Category.GetCategories(null);
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
                newProduct.ImageSource2 = newProduct.ImageSource2 == null ? "" : newProduct.ImageSource2;
                newProduct.ImageSource3 = newProduct.ImageSource3 == null ? "" : newProduct.ImageSource3;
                newProduct.ImageSource4 = newProduct.ImageSource4 == null ? "" : newProduct.ImageSource4;
                newProduct.ImageSource5 = newProduct.ImageSource5 == null ? "" : newProduct.ImageSource5;
                newProduct.ImageSource6 = newProduct.ImageSource6 == null ? "" : newProduct.ImageSource6;
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
            ViewBag.Category = Models.ParentCategory.GetCategories(null);

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult EditCategory(int id)
        {
            Category category = Models.Category.GetCategories(id).FirstOrDefault();
            ViewBag.Category = Models.ParentCategory.GetCategories(null);
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

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult CreateParentCategory()
        {
            ViewBag.Category = Models.Category.GetCategories(null);
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult EditParentCategory(int id)
        {
            ParentCategory ParentCategory = Models.ParentCategory.GetCategories(id).FirstOrDefault();
            ViewBag.Category = Models.Category.GetCategories(null);
            return View("CreateParentCategory", ParentCategory);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ValidateInput(false)]
        public ActionResult DeleteParentCategory(int id)
        {
            Models.ParentCategory.DeleteParentCategory(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateParentCategory(ParentCategory newParentCategory)
        {
            try
            {
                newParentCategory.SaveParentCategory();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("CreateParentCategory");
            }
        }
        public ActionResult Product(int id)
        {
            var product = LaserArt.Models.Product.GetProducts(id).FirstOrDefault();
            var category = LaserArt.Models.Category.GetCategories(product.CategoryId).FirstOrDefault();
            var parent = LaserArt.Models.ParentCategory.GetCategories(category.ParentId).First();
            ViewBag.Parent = parent;
            ViewBag.CategoryName = category.CategoryName;
            return View(product);
        }

        public ActionResult Category(int id)
        {
            var category = LaserArt.Models.Category.GetCategories(id).FirstOrDefault();
            ViewBag.CategoryName = category.CategoryName;
            ViewBag.IsCategory = 1;
            var parent = Models.ParentCategory.GetCategories(category.ParentId).First();
            ViewBag.Parent = parent;
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

        public JsonResult AddToCard(int productId, decimal quantity)
        {
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];
                var value = cookieOld.Value;
                var model = JsonConvert.DeserializeObject<List<CardModel>>(value);
                int index = model.FindIndex(item => item.ProductId == productId);

                decimal quantityNew=0;
                if(index >=0)
                {
                    quantityNew=model[index].ProductQuantity;
                    model.RemoveAt(index);
                }
                CardModel newItem = new CardModel();
                newItem.ProductId = productId;
                newItem.ProductQuantity = quantity+quantityNew;
                model.Add(newItem);

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

        public ActionResult OutStock()
        {
            var products = LaserArt.Models.Product.GetOutProducts(null);
            return View(products);
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
                    item.product = LaserArt.Models.Product.GetProducts(Convert.ToInt32(item.ProductId)).FirstOrDefault();
                    productList.Add(item);
                    sum += item.ProductQuantity * item.product.Price;
                }

            }
            List<Models.Product> prod = new List<Models.Product>();
            var recommendedList = Models.Product.GetProducts(null).Take(18);
            List<int> randomList = new List<int>();
            Random rnd = new Random();
            while (true)
            {
                if (recommendedList.Count() < 3)
                    break;
                int r = rnd.Next(recommendedList.Count());
                if (!randomList.Contains(r))
                    randomList.Add(r);
                if (randomList.Count >= 3)
                    break;
            }
            foreach (int item in randomList)
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
            decimal sum = 0;
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];
                var value = cookieOld.Value;
                var model = JsonConvert.DeserializeObject<List<CardModel>>(value);
                var product = model.Where(m => m.ProductId == id).First();
                Product prod = Models.Product.GetProducts(product.ProductId).First();
                sum = prod.Price * product.ProductQuantity;

                model.Remove(model.Where(m => m.ProductId == id).First());

                cookieOld.Value = JsonConvert.SerializeObject(model);
                Response.Cookies.Add(cookieOld);
            }

            return Json(sum, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int id)
        {
            Models.Product.DeleteProduct(id);
            return Json("Телефон удален.", JsonRequestBehavior.AllowGet);
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
        public ActionResult OrderDetails(int id)
        {
            Order newOrder = new Order();
            ViewBag.Cities = DeliveryCity.GetDelivery(null);


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
            else
            {
                return RedirectToAction("Index");
            }
            newOrder.CityId = id;
            return View(newOrder);
        }

        [HttpPost]
        [ValidateInput(false)]
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
                    var product = Models.Product.GetProducts(item.ProductId).First();
                    newOrder.Amount += product.Price * item.ProductQuantity;
                }
            }
            newOrder.Amount += newOrder.city.Money;
            newOrder.Id = newOrder.saveOrder();
            SendMail(newOrder);
            if (Request.Cookies["Cart"] != null)
            {
                var c = new HttpCookie("Cart");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            if (newOrder.isCash)
            {
                return RedirectToAction("OrderAproval", new { id = newOrder.Id });
            }
            else
            {
                //onlinePayment
                return RedirectToAction("OrderAproval", new { id = newOrder.Id });
            }
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
                mail.Subject = "ՆՈՐ ՊԱՏՎԵՐ!";
                StringBuilder Body = new StringBuilder();
                Body.Append("<b>Դուք ունեք նոր պատվեր</b><br/>")
                    .Append(string.Format("Պատվերի համար:{0} <br/>", newOrder.Id))
                    .Append(string.Format("Հասցե:{0} <br/>", newOrder.Address))
                    .Append(string.Format("Հեռ.:{0} <br/>", newOrder.PhoneNumber))

                    .Append(string.Format("Պատվիրատույի անունը:{0} {1} <br/>", newOrder.Name, newOrder.SurName));
                foreach (var item in newOrder.Products)
                {
                    var product = Models.Product.GetProducts(item.ProductId).FirstOrDefault();
                    Body.Append(string.Format("Ապրանք:N{0} {1}  քան:{2}<br/>", item.ProductId, product.ProductTitle,item.ProductQuantity));
                }
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
            catch (Exception ex)
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
        [Authorize(Roles = "Administrator")]
        public ActionResult Sales()
        {
            var sales = Models.Sales.GetSalesById(null);
            return View(sales);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult SalesDetails(int? id)
        {
            var sale = Models.Sales.GetSalesById(id).FirstOrDefault();
            return View(sale);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult SalesDetails(Sales sale)
        {
            try
            {
                sale.SaveSales();
                return RedirectToAction("Sales");
            }
            catch (Exception ex)
            {
                return RedirectToAction("SalesDetails", sale.Id);

            }




        }

        public ActionResult Guarantee()
        {
            return View();
        }
        public ActionResult Delivery()
        {
            return View();
        }
        public ActionResult CardPartial(int? category)
        {
            try
            {
                var rd = ControllerContext.ParentActionViewContext.RouteData;
                var currentAction = rd.GetRequiredString("action");
                ViewBag.Route = currentAction.ToUpper();
            }

            catch 
            {
                if(category==1)
                {
                    ViewBag.Route = "CATEGORY";
                }
            }
            List<CardModel> productList = new List<CardModel>();
            ViewBag.Cities = DeliveryCity.GetDelivery(null);
            decimal sum = 0;
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
            ViewBag.Sum = sum;
            return PartialView(productList);
        }
        [HttpPost]
        public ActionResult OrderCategory(List<CardModel> card)
        {
            return View();
        }
        public string GetCount()
        {
            if (Request.Cookies["Cart"] != null)
            {
                HttpCookie cookieOld = Request.Cookies["Cart"];

                var value = cookieOld.Value;

                var idList = JsonConvert.DeserializeObject<List<CardModel>>(value);
                return idList.Count.ToString();

            }
            else
            {
                return "0";
            }
        }
        [HttpPost]
        public JsonResult SubmitReview(string email, string name, string body)
        {
            try
            {
                string EmailTo = WebConfigurationManager.AppSettings["EmailTo"];
                string EmailFrom = WebConfigurationManager.AppSettings["EmailFrom"];
                string EmailFromPassword = WebConfigurationManager.AppSettings["EmailFromPassword"];

                MailMessage mail = new MailMessage();
                mail.To.Add(EmailTo);
                mail.From = new MailAddress(EmailFrom);
                mail.Subject = string.Format("Նոր հարցում {0} էլ. փոստ {1}", name, email);

                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(EmailFrom, EmailFromPassword); // Enter seders User name and password   
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ViewBag.MessageContact = "";
                return Json("Հարցումը հաջողությամբ մշակված է", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Հարցումը մշակված չէ", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Discount()
        {
            ViewBag.IsCategory = 1;
            List<Models.Product> model = Models.Product.GetDiscountedProducts();
            return View(model);
        }
    }
}