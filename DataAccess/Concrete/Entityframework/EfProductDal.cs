using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
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
    }
}
