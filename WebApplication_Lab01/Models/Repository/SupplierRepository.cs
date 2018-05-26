using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Lab01.Models.Interface;

namespace WebApplication_Lab01.Models.Repository
{
    public class SupplierRepository : ISupplierRepository, IDisposable
    {
        protected NorthwindEntities db
        {
            get;
            private set;
        }

        public SupplierRepository()
        {
            this.db = new NorthwindEntities();
        }

        public IQueryable<Suppliers> GetAll()
        {
            return db.Suppliers.OrderBy(x => x.SupplierID);
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