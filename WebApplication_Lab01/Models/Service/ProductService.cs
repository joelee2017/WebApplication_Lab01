using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Lab01.Models.Service
{
    public class ProductService : IProductService,IDisposable
    {
        protected NorthwindEntities db
        {
            get;
            private set;
        }

        public ProductService()
        {
            this.db = new NorthwindEntities();
        }

        public IQueryable<Products> Search(string productName)
        {
            return db.Products.Where(s => s.ProductName.Contains(productName));
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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}