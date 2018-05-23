using System;
using System.Collections.Generic;
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
            var query = db.Products.OrderBy(x => x.ProductID).ToList();
            return View(query);
        }


    }
}