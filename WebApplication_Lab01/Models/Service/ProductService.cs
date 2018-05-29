using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Lab01.Models.Interface;
using WebApplication_Lab01.Models.Repository;

namespace WebApplication_Lab01.Models.Service
{
    public class ProductService : ProductRepository,IProductService
    {

        public IQueryable<Products> Search(string productName)
        {
            return db.Products.Where(s => s.ProductName.Contains(productName)).OrderBy(s => s.ProductName);
        }

        public IQueryable<Products> SortOrder(string sortOrder)
        {
            switch (sortOrder)
            {
                case "ProductName_desc":
                    return db.Products.OrderByDescending(s => s.ProductName);
                case "UnitPrice":
                    return db.Products.OrderBy(s => s.UnitPrice);
                case "UnitPrice_desc":
                    return db.Products.OrderByDescending(s => s.UnitPrice);
                default:
                    return db.Products.OrderBy(s => s.ProductName);
            }
        }

    }
}