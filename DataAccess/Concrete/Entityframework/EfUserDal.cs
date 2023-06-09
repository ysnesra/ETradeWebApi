using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entityframework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ETradeAppDbContext>, IUserDal
    {
        private readonly ETradeAppDbContext _context;

        public EfUserDal(ETradeAppDbContext context) : base(context)
        {
            _context = context;
        }

        public UserDetailDto GetUserByEmail(string email)
        {            
                var result = _context.Users.Include(x => x.Products)
                                           .Where(x=>x.Email == email)
                                           .Select(x => new UserDetailDto
                                           {
                                                UserId = x.Id,
                                                FirstName = x.FirstName,
                                                LastName = x.LastName,
                                                Email = x.Email,
                                                Password = x.Password,
                                           }).FirstOrDefault();

                if (result != null)
                {
                    return result;
                }
                return null;
            
        }
    }
}

