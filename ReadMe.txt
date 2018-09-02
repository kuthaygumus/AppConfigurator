###################################### AppConfigurator ######################################

### Projenin Amacı
---------------------------------------------

- Herhangi ortak bir storage'da tutulan configuration, setting vb. değerlerin, dinamik olarak değiştirilmesi, değişikliklerin verilen zaman dilimlerinde aktif olmasını sağlamak.


## Projenin Çalıştırılması İçin Gereksinimler
---------------------------------------------

- dotnet core
- docker
- redis


## Proje Mimarisi
---------------------------------------------

- Lib 
	- IoC olarak, dotnet core'un kendi Service Provider'ı kullanıldı.(İhtiyaçlarımı karşılamak adına gayet yeterli olduğunu düşünüyorum Autofac veya LightInject yerine tercih ettim.)
	- Interfaces kısmında, ApplicationModel'in abstractionı yaptıldı.
	- Enums, kısmı Integer, Boolean, String value type ları tutmak için kullanıldı.
	- Application içerisinde, ApplicationModel'in yanında, Redis'den configurasyonlarını okumak amacı ile, IApplication Interface'i register ederek gereksinimleri belirlendi.
	- Utils kısmı, Redis'ten alınan ve Redis'e yazılan modelin Json convert işlemleri için kullanıldı.

- Data
	- Çok basic bir şekilde yazılan ve proje ayağa kalkar kalkmaz, Redis'e atılacak ve daha sonra muhtemelen ezilecek configurasyonlar bulunmaktadır.

- Service
	- Asenkron yapıya sahip Redis Manager'ı ve ileride bir çok Manager'ı barındıracak kütüphane.
	- Burada, Redis'in asenkron CRUD metodları bulunmaktadır.
	- Data katmanı buraya dependency olarak verilip, dataların okunup proje ayağa kalkarken Redis'e basılması sağlandı.

- UI
	- Sıfırdan bir asp.net core web projesi oluşturuldu, Controller'ı api olarak ayağa kaldırıldı.
	- Redis'e register edilmiş datalar, burada okundu.
	- Web arayüzü olarak, vue.js ile basit bir app oluşturuldu.
	- app.js üzerinden View beslenmiş oldu.

- Test
	- xUnit ile yazdığım basic birkaç unit test bulunmaktadır.
	- Fake veya Mock'lanmış data yerine, Redis'e gerçek data'yı basıp, onun üzerinden koşturmaktayım testleri.