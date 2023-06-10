using Business.Abstract;
using Business.Constants;
using Business.Utilities.Security.JWT;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using Entities.Concrete;
using Entities.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        //Kayıt(Üye olma) metotu Dto model ve password istiyoruz
        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {          
            _userService.Add(userRegisterDto);
            var user = new User
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Password = userRegisterDto.Password
            };
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            var userdb = new UserLoginDto
            {
                Email = userToCheck.Email,              
                Password = userToCheck.Password
            };
            if (userdb.Password != userLoginDto.Password)
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }     
            
            return new SuccessDataResult<User>(userToCheck, Messages.LoginSuccessful);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByEmail(email).Data != null)
            {
                return new ErrorResult(Messages.UserExists);
            }
            return new SuccessResult();
        }

        //AccessToken üreten metot
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            // Kullanıcının login olup olmadığını kontrol etmek için
            if (user != null)
            {
                var accessToken = _tokenHelper.CreateToken(user);
                return new SuccessDataResult<AccessToken>(accessToken, Messages.CreatedToken);
            }
            else
            {
                // Kullanıcı login olmamış, hata döndür
                return new ErrorDataResult<AccessToken>(Messages.UserNotLogin);
            }
        }
    }
}
