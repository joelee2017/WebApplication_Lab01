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
using WebApplication_Lab01.Models.Service;

namespace WebApplication_Lab01.Controllers
{
    public class ProductsController : Controller
    {
        private ICategoryService categoryService;
        private ISupplierService supplierService;
        private IProductService productService;

        public IEnumerable<Categories> Categories
        {
            get
            {
                return categoryService.GetAll();
            }
        }
        public IEnumerable<Suppliers> Suppliers
        {
            get
            {
                return supplierService.GetAll();
            }
        }

        public ProductsController()
        {
            this.categoryService = new CategoryService();
            this.supplierService = new SupplierService();
            this.productService = new ProductService();
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

            IQueryable<Products> result = this.productService.GetAll();

            
            if (!String.IsNullOrEmpty(searchString))
            {
                 result = this.productService.Search(searchString);
            }
            else
            {
                result = this.productService.SortOrder(sortOrder);
            }
            
            
            

            int pageSize = 5;
            int pageNumber = (page ?? 1);            
            return View(result.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id =0)
        {
            Products products = this.productService.Get(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        //=============================================================================

        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName");
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName");           
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                this.productService.Create(products);
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //==============================================================================

        public ActionResult Edit(int id = 0)
        {
            Products products = this.productService.Get(id);
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
                this.productService.Update(products);
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(this.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);
            return View(products);
        }
        //==============================================================================
        
        public ActionResult Delete(int id =0)
        {
            Products products = this.productService.Get(id);
            if(products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = this.productService.Get(id);
            this.productService.Delete(products);
            return RedirectToAction("Index");
        }
        //==============================================================================

    }
}