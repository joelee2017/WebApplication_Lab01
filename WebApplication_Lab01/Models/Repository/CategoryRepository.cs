using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Lab01.Models.Interface;

namespace WebApplication_Lab01.Models.Repository
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {      
        protected NorthwindEntities db
        {
            get;
            private set;
        }

        public CategoryRepository()
        {
            this.db = new NorthwindEntities();
        }

        public IQueryable<Categories> GetAll()
        {
            return db.Categories.OrderBy(x => x.CategoryID);
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
                if(this.db !=null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}