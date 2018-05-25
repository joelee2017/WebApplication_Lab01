using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Lab01.Models;
using PagedList;

namespace WebApplication_Lab01.Controllers
{
    public class ProductsController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                ViewBag.CurrentFilter = searchString;
            }

            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ?  "ProductName_desc" : "";
            ViewBag.UnitPriceSortParm = sortOrder == "UnitPrice" ? "UnitPrice_desc" : "UnitPrice";

            IQueryable<Products> result = db.Products;


            if (!String.IsNullOrEmpty(searchString))
            {
                result = db.Products.Where(s => s.ProductName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ProductName_desc":
                    result = result.OrderByDescending(s => s.ProductName);
                    break;
                case "UnitPrice":
                    result = result.OrderBy(s => s.UnitPrice);
                    break;
                case "UnitPrice_desc":
                    result = result.OrderByDescending(s => s.UnitPrice);
                    break;
                default:
                    result = result.OrderBy(s => s.ProductName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var products = db.Products.Include(x => x.Categories).OrderByDescending(x => x.ProductID);
            return View(result.ToPagedList(pageNumber, pageSize));
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
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");           
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products products)
        {

            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
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
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
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
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
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