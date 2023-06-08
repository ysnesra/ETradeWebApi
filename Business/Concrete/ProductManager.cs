using Business.Abstract;
using Business.Constants;
using Core.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(CreatedProductDto model)
        {
            Product newProduct = new Product()
                { 
                   ProductName=model.ProductName,
                   Description=model.Description,
                   Price=model.Price,
                   UserId=model.UserId
                };
            _productDal.Add(newProduct);   
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(DeletedProductDto model)
        {
            Product deleteProduct = new Product()
            {
                ProductId=model.ProductId,                         
            };           
            _productDal.Delete(deleteProduct);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<IPaginate<Product>> GetAll(PageRequest pageRequest)
        {
           return new SuccessDataResult<IPaginate<Product>>(_productDal.GetList(index:pageRequest.Page,size:pageRequest.PageSize),Messages.ProductsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(c => c.ProductId == productId), Messages.ProductDetail);
        }

        public IDataResult<IPaginate<Product>> GetByUserId(int userId, PageRequest pageRequest)
        {
            return new SuccessDataResult<IPaginate<Product>>(_productDal.GetList
                (x => x.UserId == userId, index: pageRequest.Page, size: pageRequest.PageSize));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails(int userId)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetListProductWithUser(userId),Messages.ProductListWithUser);
        }

        public IResult Update(UpdatedProductDto model)
        {
            Product updateProduct = new Product()
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                UserId = model.UserId
            };
            _productDal.Update(updateProduct);
            return new SuccessResult(Messages.ProductUpdated);
        }

    }
}
