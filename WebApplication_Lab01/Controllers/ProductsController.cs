using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Lab01.Models;

namespace WebApplication_Lab01.Controllers
{
    public class ProductsController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(x => x.Categories).OrderByDescending(x => x.ProductID);
            return View(products.ToList());
        }

        public ActionResult Details(int id =0)
        {
            Products products = db.Products.Find(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        //=============================================================================

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products products)
        {
            if(ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //==============================================================================

        public ActionResult Edit(int id = 0)
        {
            Products products = db.Products.Find(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        [HttpPost]
        public ActionResult Edit(Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }           
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //==============================================================================
        
        public ActionResult Delete(int id =0)
        {
            Products products = db.Products.Find(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //==============================================================================

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}