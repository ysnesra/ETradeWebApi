# ETradeWebApi
.NET Mvc Core 6'da, ORM Toollarından EntityFramework ile Codefirst tekniği kullanılarak geliştirdiğm CallCenter Projesidir


**CALL CENTER UYGULAMASI**

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
	 Autofac yapısı oluşturuldu.

Kullanıcı üye olunca token üretip bu token ile Authorize oluyoruz:

![1Register_istek](https://github.com/ysnesra/ETradeWebApi/assets/104023688/64e86619-1d1d-4c4b-abfb-10ca1c0f01ec)
![1Register_sonuc](https://github.com/ysnesra/ETradeWebApi/assets/104023688/e7fd7c0b-85b0-4d0e-8288-1a600fab882d)
![2Authorizeolma](https://github.com/ysnesra/ETradeWebApi/assets/104023688/30f54d05-2455-4439-b301-40751dd18095)
![3Login_istek](https://github.com/ysnesra/ETradeWebApi/assets/104023688/96ba1256-e7ac-486f-93c0-2d6a4ae418f8)
![3Login_sonuc](https://github.com/ysnesra/ETradeWebApi/assets/104023688/bf38aa2a-ce36-40fb-a741-440972dedab6)

Product ve User işlemleri Authorize olan kullanıcı için yapılır: 
![4Product](https://github.com/ysnesra/ETradeWebApi/assets/104023688/fe5aff9b-f718-4100-9730-733324562aa0)
![4user](https://github.com/ysnesra/ETradeWebApi/assets/104023688/151a72c6-f3a9-443b-a904-82208f24abc2)
