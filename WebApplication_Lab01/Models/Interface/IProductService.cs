using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Lab01.Models.Interface;

namespace WebApplication_Lab01.Models.Service
{
    public interface IProductService
    {
        IResult Create(Products instance);

        IResult Update(Products instance);

        IResult Delete(int productID);

        Products GetByID(int productID);

        IQueryable<Products> GetAll();

        IQueryable<Products> GetDefault();

        IQueryable<Products> Search(string productName);

        IQueryable<Products> SortOrder(string sortOrder);
    }
}