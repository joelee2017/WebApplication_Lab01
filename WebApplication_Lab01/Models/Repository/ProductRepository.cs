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
        protected NorthwindEntities db  {  get; private set; }

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

        public IQueryable<Products> GetDefault()
        {
            return db.Products;
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
      
    }
}