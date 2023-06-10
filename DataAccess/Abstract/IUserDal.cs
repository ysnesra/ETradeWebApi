using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        UserDetailDto GetUserByEmail(string email);

        string CurrentUser(IHttpContextAccessor httpContextAccessor);
    }
}
