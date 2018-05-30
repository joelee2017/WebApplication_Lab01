using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Lab01.Models.Interface
{
    public interface IProductRepository
    {
        void Create(Products instance);

        void Update(Products instance);

        void Delete(Products instance);        

        Products Get(int productID);

        IQueryable<Products> GetAll();

        IQueryable<Products> GetDefault();

        void SaveChanges();
    }
}