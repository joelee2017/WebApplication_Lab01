using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Lab01.Models;
using PagedList;
using WebApplication_Lab01.Models.Interface;
using WebApplication_Lab01.Models.Repository;

namespace WebApplication_Lab01.Controllers
{
    public class ProductsController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();

        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        private ISupplierRepository supplierRepository;

        public IEnumerable<Categories> Categories
        {
            get
            {
                return categoryRepository.GetAll();
            }
        }
        public IEnumerable<Suppliers> Suppliers
        {
            get
            {
                return supplierRepository.GetAll();
            }
        }

        public ProductsController()
        {
            this.productRepository = new ProductRepository();
            this.categoryRepository = new CategoryRepository();
            this.supplierRepository = new SupplierRepository();
        }

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
            }
            ViewBag.CurrentFilter = searchString;

            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ?  "ProductName_desc" : "";
            ViewBag.UnitPriceSortParm = sortOrder == "UnitPrice" ? "UnitPrice_desc" : "UnitPrice";

            IQueryable<Products> result = db.Products;


            if (!String.IsNullOrEmpty(searchString))
            {
                //抽換  db.Products.Where(s => s.ProductName.Contains(searchString));
                result = this.productRepository.Search(searchString);
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
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id =0)
        {
            //抽換 db.Products.Find(id);
            Products products = this.productRepository.Get(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        //=============================================================================

        public ActionResult Create()
        {
            //抽換 db.Suppliers db.Categories
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName");
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName");           
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products products)
        {

            if (ModelState.IsValid)
            {
                //抽換
                //db.Products.Add(products);
                //db.SaveChanges();

                this.productRepository.Create(products);
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //==============================================================================

        public ActionResult Edit(int id = 0)
        {
            //抽換 db.Products.Find(id);
            Products products = this.productRepository.Get(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }

        [HttpPost]
        public ActionResult Edit(Products products)
        {
            if (ModelState.IsValid)
            {
                //抽換
                //db.Entry(products).State = EntityState.Modified;
                //db.SaveChanges();

                this.productRepository.Update(products);
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //==============================================================================
        
        public ActionResult Delete(int id =0)
        {
            //抽換 db.Products.Find(id);
            Products products = this.productRepository.Get(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //抽換 db.Products.Find(id);
            Products products = this.productRepository.Get(id);
            //db.Products.Remove(products);
            //db.SaveChanges();
            this.productRepository.Delete(products);
            return RedirectToAction("Index");
        }
        //==============================================================================

    }
}