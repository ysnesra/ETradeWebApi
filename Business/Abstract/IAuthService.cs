using Business.Utilities.Security.JWT;
using Core.DataAccess;
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
    public interface IAuthService 
    {
        //Kayıt(Üye olma) olurken Dto model ve password istiyoruz
        IDataResult<User> Register(UserRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserLoginDto userForLoginDto);

        //Kullanıcı var mı
        IResult UserExists(string email);

        //AccessToken üreten metot
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
