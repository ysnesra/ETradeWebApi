using Core.DataAccess;
using Core.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService 
    {
        IResult Add(UserRegisterDto user);
        IResult Update(UpdatedUserDto user);
        IResult Delete(DeletedUserDto user);
        IDataResult<IPaginate<User>> GetAll(PageRequest pageRequest);
        IDataResult<UserDetailGetByIdDto> GetById(int userId);        
        IDataResult<User> GetByEmail(string email);
        string CurrentUser(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor);
    }
}
