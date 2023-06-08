using Business.Abstract;
using Business.Constants;
using Core.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using Entities.Concrete;
using Entities.DTOs.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IDataResult<IPaginate<User>> GetAll(PageRequest pageRequest)
        {
            return new SuccessDataResult<IPaginate<User>>(_userDal
                .GetList(index: pageRequest.Page, size: pageRequest.PageSize), Messages.UsersListed);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.UserId == userId), Messages.UserDetail);
        }    
        public IResult Add(UserRegisterDto model)
        {
            User newUser = new User()
            {                               
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
                Password=model.Password,
            };
            _userDal.Add(newUser);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(DeletedUserDto model)
        {
            User deleteUser = new User()
            {
                UserId = model.UserId,
            };
            _userDal.Delete(deleteUser);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult Update(UpdatedUserDto model)
        {
            User updateUser = new User()
            {               
                UserId = model.UserId,
                FirstName=model.FirstName,
                LastName= model.LastName,
                Email= model.Email,
                Password= model.Password
            };
            _userDal.Update(updateUser);
            return new SuccessResult(Messages.UserUpdated);
        }
    
        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

    }
}

