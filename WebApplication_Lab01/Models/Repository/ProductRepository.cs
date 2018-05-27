using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication_Lab01.Models.Interface;

namespace WebApplication_Lab01.Models.Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        protected NorthwindEntities db
        {
            get;
            private set;
        }

        public ProductRepository()
        {
            this.db = new NorthwindEntities();
        }

        public void Create(Products instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Products.Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(Products instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(Products instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public Products Get(int productID)
        {
            return db.Products.FirstOrDefault(x => x.ProductID == productID);
        }

        public IQueryable<Products> GetAll()
        {
            return db.Products.Include(x => x.Categories).OrderByDescending(x => x.ProductID);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
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

        public IQueryable<Products> Search(string productName)
        {
          return  db.Products.Where(s => s.ProductName.Contains(productName));
        }

        IQueryable<Products> SortOrder(string sortOrder)
        {
            switch (sortOrder)
            {
                case "ProductName_desc":
                    db.Products.OrderByDescending(s => s.ProductName);
                    break;
                case "UnitPrice":
                    db.Products.OrderBy(s => s.UnitPrice);
                    break;
                case "UnitPrice_desc":
                    db.Products.OrderByDescending(s => s.UnitPrice);
                    break;
                default:
                    db.Products.OrderBy(s => s.ProductName);
                    break;
            }
          
        }
    }
}