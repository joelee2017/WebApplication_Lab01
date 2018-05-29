using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Lab01.Models.Interface;

namespace WebApplication_Lab01.Models.Service
{
    public interface IProductService : IProductRepository
    {
        IQueryable<Products> Search(string productName);

        IQueryable<Products> SortOrder(string sortOrder);
    }
}