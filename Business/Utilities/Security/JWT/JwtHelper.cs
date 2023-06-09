using Business.Utilities.Security.Encryption;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Core.Extensions;

namespace Business.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        //Configuration'ı dependency Enjection yaparak zayıf bağımlılık olarak verdik. appsetting dosyamızı okumamızı sağlar
        //appsettings.json deki değerleri Configuration ile okuyacağız.Orda okuduğumuz değerleri "_tokenOptions" nesnesinde tutacağız
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;   //appsetting de verdiğimiz bilgileri taşır
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//appsetting deki "TokenOptions" Section nını TokenOptions classındaki değerlerle mapple

        }
        public AccessToken CreateToken(User user)
        {
            //accessToken'nın bitş süresini belirliyor.//_tokenOptions. daki süreyi alıyor onuda Configurationdan alıyor
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); 
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();//Elimizdeki token bilgisini yazdırmak.
            var token = jwtSecurityTokenHandler.WriteToken(jwt);//Elimizdeki tokeni stringe çevirdik.
          

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        //JWT oluşturan metot
        //Hangi token bilgileri, hangi kullanıcı , hangi imza ile, hangi rolleri içeriyorsa bunları parametre olarak alıp Jwt oluşturan metot o
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,   //şimdiki zamandan önceki bir değer verilemez
                claims: SetClaims(user),   //kullanıcının claimlerini oluşturmak için SetClaims metotu oluşturuldu
                signingCredentials: signingCredentials
            );
            
            return jwt;
        }

        //Kullanıcı ve rolleri(operationClaimleri) parametrelerine göre claimleri oluşturan metot 
        private IEnumerable<Claim> SetClaims(User user)
        {
            //Claims listesi oluşturup Id ve mail claim de tutulur
            //Core da oluşturduğum ClaimExtensions.cs extension metotunda .Net in claims nesnesine oluşturdğum metotlar kullanıldı.
            List<Claim> claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");  

            return claims;
        }
    }
}

