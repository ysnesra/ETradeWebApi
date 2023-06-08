using Core.DataAccess.Entityframework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entityframework
{
    public class EfProductDal : EfEntityRepositoryBase<Product,ETradeAppDbContext >, IProductDal
    {
        private readonly ETradeAppDbContext _context;

        public EfProductDal(ETradeAppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductDetailDto> GetListProductWithUser(int userId)
        {
            var result = _context.Products.Include(x => x.User)
                                          .Where(x=>x.UserId==userId)
                                          .Select(x=>new ProductDetailDto
                                          {
                                              ProductId = x.ProductId,
                                              ProductName = x.ProductName,
                                              Price = x.Price,
                                              Description = x.Description,
                                              UserName = $"{x.User.FirstName}{x.User.LastName}",
                                          }).ToList();
            return result;

        }
    }
}
