using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace ADOMvcCRUD.Controllers
{
    public class ProductsController : Controller
    {
        ProductDemo db = new ProductDemo();
        // GET: Products
        public ActionResult Index()
        {
            return View(db.ProductsList);
        }

        public ActionResult Details(int id)
        {
            return View(db.ProductsList.Single(x=>x.ProductId==id));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            db.InsertProduct(product);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(db.ProductsList.Single(x=>x.ProductId==id));
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            db.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}