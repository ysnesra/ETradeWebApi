﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductWithUser = "Ürünün detay bilgilerini kullanıcı ismiyle birlikte getirdi";
        public static string NoExist_ProductWithUser = "Bu kullanıcıya ait böyle bir ürün bulunmamaktadır";
        
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductDetail = "Ürünün detay bilgilerini getirdi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductUpdated = "Ürün bilgileri güncellendi";
        public static string ProductDeleted = "Ürün silindi";
        public static string ProductInvalid = "Ürün bulunamadı"; 
        public static string ProductNameFilter = "Ürünler ; ürün adına göre filtreli listelendi";
        public static string ProductNameNoFilter = "Ürünler ; ürün adına göre filtrelenemez.Kullanıcın böyle bir ürünü yok";


        public static string ProductPriceFilter = "Ürünler ; ürün fiyatına göre filtreli listelendi";
        public static string ProductPriceNoFilter = "Ürünler ; ürün fiyatına göre filtrelenemez.Kullanıcın bu fiyatta ürünü yok";

        public static string UsersListed = "Kullanıcılar listelendi";
        public static string UserDetail = "Kullanıcının detay bilgilerini getirdi";        
        public static string UserEmailInvalid = "Mail adresi yanlış ";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string UserUpdated = "Kullanıcı bilgileri güncellendi";
        public static string UserDeleted = "Kullanıcı sistemden silindi";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası"; 
        public static string LoginSuccessful = "Başarılı Giriş";
        public static string UserExists = "Kullanıcı sistemde mevcut";
        public static string CreatedToken = "Token oluşturuldu";
        public static string UserNotLogin = "Kullanıcı giriş yapmamış";
        

    }
}
