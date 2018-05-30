using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Lab01.Models.Interface;
using WebApplication_Lab01.Models.Repository;

namespace WebApplication_Lab01.Models.Service
{
    public class ProductService : IProductService
    {
        private  IProductRepository d_productRepository ;

        public ProductService(IProductRepository productRepository)
        {
            d_productRepository = productRepository;
        }

        public  IResult Create(Products instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.d_productRepository.Create(instance);
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Update(Products instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException();
            }

            IResult result = new Result(false);
            try
            {
                this.d_productRepository.Update(instance);
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int productID)
        {
            IResult result = new Result(false);

            if(!this.IsExists(productID))
            {
                result.Message = "找不到資料";
            }

            try
            {
                var instance = this.GetByID(productID);
                this.d_productRepository.Delete(instance);
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public bool IsExists(int productID)
        {
            return this.d_productRepository.GetAll().Any(x => x.ProductID == productID);
        }

        Products GetByID(int productID)
        {
            return this.d_productRepository.Get(x => x.ProductID == productID);
        }

        public IQueryable<Products> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Products> GetDefault()
        {
            throw new NotImplementedException();
        }

        public IResult SaveChanges()
        {
            throw new NotImplementedException();
        }

      

        public IQueryable<Products> Search(string productName)
        {
            return d_productRepository.GetDefault().Where(s => s.ProductName.Contains(productName)).OrderBy(s => s.ProductName);
        }

        public IQueryable<Products> SortOrder(string sortOrder)
        {
            switch (sortOrder)
            {
                case "ProductName_desc":
                    return d_productRepository.GetDefault().OrderByDescending(s => s.ProductName);
                case "UnitPrice":
                    return d_productRepository.GetDefault().OrderBy(s => s.UnitPrice);
                case "UnitPrice_desc":
                    return d_productRepository.GetDefault().OrderByDescending(s => s.UnitPrice);
                default:
                    return d_productRepository.GetDefault().OrderBy(s => s.ProductName);
            }
        }

      
    }
}