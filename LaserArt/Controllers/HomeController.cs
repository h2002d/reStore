using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            HttpPostedFile file=null;
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
            return Json("Картика загружена", JsonRequestBehavior.AllowGet);
        }
    }
}