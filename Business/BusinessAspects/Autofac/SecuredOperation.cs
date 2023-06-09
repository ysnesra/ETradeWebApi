using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    //JWT İÇİN
    public class SecuredOperation : MethodInterception
    {
        protected override void OnBefore(IInvocation invocation)
        {
            var httpContext = invocation.InvocationTarget.GetType()
            .GetProperty("HttpContext")?.GetValue(invocation.InvocationTarget) as HttpContext;
            // Kullanıcının login olup olmadığını kontrol eder
            if (httpContext?.User.Identity.IsAuthenticated ?? false)
            {
                return; // Login olmuş, metodu çalıştırmaya devam et
            }
            throw new Exception(Messages.AuthorizationDenied);
          
        }

        //private IHttpContextAccessor _httpContextAccessor;// Jwt a gönderecek istek yapınca Her istek için bir threat(HttpContext) oluşur       

        //public SecuredOperation()
        //{
        //    //ServiceTool-> Projemizde aspectlerde İnjection yapmamızı sağlar.Injection alt yapımızı okuyabilmemizi sağlayan araçtır
        //    //ServiceTool ile Autofac de oluşturduğumuz servis mimarimize ulaşır

        //    _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        //}

        //protected override void OnBefore(IInvocation invocation)
        //{
        //    // Kullanıcının login olup olmadığını kontrol eder
        //    if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        return; // Login olmuş, metodu çalıştırmaya devam et
        //    }
        //    throw new Exception(Messages.AuthorizationDenied);
        //}
    }
}
