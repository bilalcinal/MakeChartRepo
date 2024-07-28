# Dynamic Chart Application

Bu proje, kullanıcıların dinamik grafikler oluşturabileceği, kayıtlı veritabanlarından veri çekip görselleştirebileceği bir uygulamadır. Kullanıcılar, veritabanı bağlantı bilgilerini kayıt edebilir, giriş yapabilir ve çeşitli grafik türlerinde verileri görselleştirebilirler.

## Özellikler

- Kullanıcı kayıt ve giriş sistemi (JWT ile)
- Kullanıcıların veritabanı bağlantı bilgilerini kaydetme
- Kayıtlı veritabanlarından verileri çekip grafik oluşturma
- Farklı grafik türleri (Çizgi, Bar, Radar, Pasta)

## Kullanım

1. Uygulama başlatıldıktan sonra tarayıcıda `index.html` dosyasını açın.
2. Kullanıcı kaydı yapın ve ardından giriş yapın.
3. Dashboard sayfasında yeni bir veritabanı bağlantısı kaydedin veya mevcut kaydedilmiş veritabanlarını yönetin.
4. Chart oluşturma sayfasına giderek kayıtlı veritabanlarından veri çekin ve grafik oluşturun.

## Proje Yapısı

- **ChartRepoBackend**: .NET Core ile yazılmış sunucu tarafı kodlarını içerir.
  - **Controllers**: API kontrolleri.
  - **Data**: Veritabanı modelleri ve DbContext.
  - **Services**: İş mantığı ve veritabanı etkileşimleri.
  - **Helpers**: Yardımcı sınıflar (JWT oluşturma gibi).

- **ChartRepoFrontend**: İstemci tarafı HTML, CSS ve JavaScript ve Jquery dosyalarını içerir.
  - **index.html**: Kullanıcı kayıt ve giriş sayfası.
  - **dashboard.html**: Kullanıcıların veritabanı bağlantılarını yönetebileceği sayfa.
  - **chart.html**: Grafik oluşturma sayfası.
  
