Stok Takip Sistemi

Devarc mimarisine göre geliştirilen stok takip sisteminin yapabildiği işlemler;
-	Malzeme kartı oluşturabilmek
-	Depo tanımlamaları
-	Depolar arası transfer
-	Malzeme giriş ve çıkışları
-	Genel inceleme ve analiz için hareket raporu.

Devarc mimarisi tam anlamıyla CQRS, Aspect ve SOLID prensiplerine uymaktadır. 
Bu sebeple oluşturduğumuz Entitylerin database işlemleri için Command ve Qurylerini yazmak, hangi contexti kullanacağı belirtmek gibi
işlemleri uygulamak gerekiyor. Bu sayede işlemlerin yoğunluğuna göre kaynak verebiliriz ve yönetimini ayrı şekillerde ele alabiliriz.

Bu süreç için;

1-) Kullanıcıdan alınan bilgiler ile malzeme kartı ve depolar oluşturuldu.

2-) Depolara ürün eklendiğinde, silindiğinde ve transfer edildiğinde StockOrderes alanına bir kayıt atıldı bu şekilde giriş ve çıkışlar kontrol edildi.

3-) Rapor için tabloların alanları bir dto’da liste olarak ekranda gösterildi.

Kullanıcı token için admin

Email: admin@adminmail.com

Şifre: Q1w212*_*

Configurasyonları belirlenmiş olan databaseyi docker üzerinden ayağa kaldırmak için yml dosyasının bulunduğu dizinde,

 Docker-compose up -d 
 
 uygulamada migration almak için 
 
add-migration init -context ProjectDbContext

migrationu veritabanına uygulamak için 

update-database -context ProjectDbContext


