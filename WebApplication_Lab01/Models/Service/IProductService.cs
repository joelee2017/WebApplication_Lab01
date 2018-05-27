using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Lab01.Models.Service
{
    public interface IProductService
    {
        IQueryable<Products> Search(string productName);

        IQueryable<Products> SortOrder(string sortOrder);
    }
}