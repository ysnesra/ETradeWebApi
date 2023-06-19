# ETradeWebApi
ETradeWepApi projesi .NET Core 6'da; n-Tier Architecture yapısı ile ORM Toollarından EntitiyFramework ve CodeFirst Tekniğini kullanarak oluşturduğum E-ticaret Api projesidir.

Bu uygulama; , kullanıcıların ürünlerle ilgili bilgileri alabildiği, yeni ürün ekleyebildiği ve varolan ürünleri güncelleyebildiği bir e-ticaret platformudur.

1. CodeFirst ORM tekniğiyle MySQL'de database oluşturuldu -> etradedb isminde 
  //User(N)- Product(1) Relationship kuruldu

2. EntityFramework kütüphaneleri ve MySql.Data kütüphanesi DataAccess ve Core katmanlarına yüklendi.

3. Core Katmanına --> IEntity,IDto,
	                 IEntityRepository (CRUD işlemelerinin generic olarak oluşturuldu)
					 EfEntityRepositoryBase ((TEntity,TContext) hangi tablo, hangi veritabanı verillirse ona göre CRUD işlemlerini yapacak base sınıfı oluşturuldu.
					 Utilities klasörü altına Results yapılandırması oluşturuldu.
					 Business katmanına, Constants içine Messages.cs classı oluşturularak mesaj textleri yazıldı.
					 Paging klasöründe Sayfalama yapısının generic kısmı oluşturuldu.

4. FluentValidation desteği eklendi.

5. Katmanlar oluşturuldu.
  Business katmanına Service ve Managerlar oluşturuldu. DataAccess katmanına Dal ve EfDal oluşturuldu.

6. Product ve User CRUD işlemleri yapıldı.

7. Sayfalama yapısı eklendi.
   Core katmanında-> Paging klasöründe sayfalama kısmı generic olarak oluşturuldu.

8. BaseEntity oluşturuldu.Id alanı bütün Entitlerde ortak olduğundan generic yapıya taşındı.

9. Jwt Authentication yapısı oluşturuldu.
  
10. Login olan kişi database de varsa CreteToken metotuyla Jwt Token üretir.
	 Autofac yapısı oluşturuldu.Ama kullanmadım.Role vermediğim için [Authorize] attribute ı yeterli oldu.

11. Product kısmında;
	   Kullanıcı login olduktan sonra işlem yapabiliyor.
	   Bunun için; ProductController a [Authorize] attributeu koyuldu.
	   Swagger da; login olunca oluşan token -> Authorize kısmına yapıştırılıp, login olup, işlemler öyle yapıldı.
	   Ürün Listeleme,Ekleme,Güncelleme ve Silme işlemlerinde tokendaki userId kullanılanarak kontrol yapıldı. 
   
    User kısmında;
	   Kullanıcı; listeleme,ekleme,güncelleme,silme kısmında Authorize olma şartı yok.

12. Ürün listesinde ürün adına göre filtreleme yapıldı.
    Core katmanına GetByFilter isminde metot oluşturuldu.(Sayfalama da dahil edilerek)
   
Kullanıcı üye olunca token üretip bu token ile Authorize oluyoruz:

![1Register_istek](https://github.com/ysnesra/ETradeWebApi/assets/104023688/64e86619-1d1d-4c4b-abfb-10ca1c0f01ec)
![1Register_sonuc](https://github.com/ysnesra/ETradeWebApi/assets/104023688/e7fd7c0b-85b0-4d0e-8288-1a600fab882d)
![2Authorizeolma](https://github.com/ysnesra/ETradeWebApi/assets/104023688/30f54d05-2455-4439-b301-40751dd18095)
![3Login_istek](https://github.com/ysnesra/ETradeWebApi/assets/104023688/96ba1256-e7ac-486f-93c0-2d6a4ae418f8)
![3Login_sonuc](https://github.com/ysnesra/ETradeWebApi/assets/104023688/bf38aa2a-ce36-40fb-a741-440972dedab6)

Product işlemleri Authorize olan kullanıcı için yapılır.

GetProductsDetails metotuyla -> Ürün detayını kullanıcı ismi ile birlikte getirir: 
![getproductDetails](https://github.com/ysnesra/ETradeWebApi/assets/104023688/6940de73-6eb5-458b-96f7-d74f581c9ede)
![getproductDetails_sonuc](https://github.com/ysnesra/ETradeWebApi/assets/104023688/3b34cdb4-77db-469b-9e99-9c95f2b82c6f)

GetProductByUserId metotuyla -> Login olan Kullanıcıya ait tüm ürünleri listeler:

![getbyuserID](https://github.com/ysnesra/ETradeWebApi/assets/104023688/860fcaf8-f290-481c-8bd4-f3c00e504655)
![getbyuserID_sonuc](https://github.com/ysnesra/ETradeWebApi/assets/104023688/02a9e035-3047-46e7-aeaf-d5783fdeb6c3)

Ürünler; ürün adına göre filtrelenerek listelenir:
![filter](https://github.com/ysnesra/ETradeWebApi/assets/104023688/e42630ef-ab35-466d-8eb5-a713e6166fe0)
![filter_sonuc](https://github.com/ysnesra/ETradeWebApi/assets/104023688/d1d858e5-dbf3-4149-ade7-04c856a6cd4e)

