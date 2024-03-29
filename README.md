# IFS Web Projesi

All the requirements below are satisfied in the project:

Kullanıcılarla ilgili istenen özellikler aşağıda listelenmiştir.

1. Kullanıcı bilgilerini saklamak için USER_TAB isimli bir tablo oluşturulacak
a. Username, Password, Name, UserType, DefaultCityName, Status alanları ve
isteğe bağlı diğer alanlar yer alacak.

2. Kullanıcı girişlerine ait kayıtları saklamak için USER_LOG_TAB isimli bir tablo
oluşturulacak
a. Username, LogId, LogTime, IPAddress, Log isimli alanlar yer alacak.
3. İki tip kullanıcı olarak; ‘Yönetici’ ve ‘Son Kullanıcı’
a. Yeni kullanıcılar sadece ‘Son Kullanıcı’ tipinde oluşturulacak
b. ‘Yönetici’ tipinde bir kullanıcı, elle veri tabanına eklenecek.
4. Kullanıcıların bir profil görüntüleme sayfası olacak
a. Name, Password ve DefaultCityName bilgileri bu sayfada değiştirilebilecek
5. Yönetici olan bir kullanıcı siteye giriş yaptığında ‘Yönetim Paneli’ menüsü gözükecek, son
kullanıcılar bu sayfalara erişemeyecek.
6. (Bonus) Kullanıcılar üst üste 3 kez yanlış şifre girmişse, 1 dakika boyunca giriş
yapılamayacak.
7. (Bonus) Kullanıcıların şifresi &quot;Salt Encryption&quot; yöntemi kullanılarak saklanacak.
a. Kaynak: https://auth0.com/blog/adding-salt-to-hashing-a-better-way-to-store-
passwords/

Günlük hava tahmini bilgilerinin nasıl alınıp kaydedilmesiyle ilgili istenenler aşağıda
listelenmiştir.

1. Günlük hava durumu bilgilerini saklamak için Weather_Info_Tab isimli bir tablo
oluşturulacak.
a. WeatherDate, CityName, Temperature, MainStatus alanları alacak.
b. “Temperature” alanında, hava sıcaklığı santigrat (Celsius) biriminde saklanacak
c. “MainStatus” alanında, hava durumu bilgisi (Güneşli, Bulutlu, Yağmurlu, Sisli,
vs..) gibi metin çeklinde tutulacak.

2. Bu sayfada, daha önce tabloya eklenmiş olan bilgiler, gün ve ile göre sıralı listelenecek.
3. Listelerde hava durumuna göre değişen resimler veya ikonlar yer alacak
4. Listelerde kayıt silme özelliği olacak.
5. Sayfada, il ve gün kriterlerine göre arama yapılabilecek.
6. Sayfada kayıt ekleme butonu olacak
a. Butona basıldığında, kayıt ekleme formunu, diyalog şeklinde ekrana getirecek.
7. Kayıt formunda, gün, şehir, sıcaklık ve hava durumu bilgisi elle girilecektir.
a. (Bonus) Sadece gün ve şehir bilgisi girilip, sıcaklık ve hava durumu bilgisi, internet
üzerinden hava durumu paylaşımı yapan bir web servis üzerinden otomatik
çekilecek, Yazılımcı burada istediği web servisi tercih edebilir.

Ana sayfada, günlük ve haftalık hava tahmini bilgileri, grafikle beraber yer alacaktır. İstenen
bilgiler şu şekildedir;

1. Bulunduğumuz haftanın hava sıcaklığını gösteren bir çizgisel grafik yer alacaktır.
2. Bulunduğumuz günün hava sıcaklığı ve durumunu, yanında resimle veya simgeyle
birlikte gösterilecek.
a. (Bonus) Gösterilen hava durumu resmi animasyon şeklinde gösterilebilir.
3. Bu bilgiler, kullanıcının profilinde kayıtlı olan “DefaultCityName” bilgisine göre
getirilecek.
4. Kullanıcılar ön değer şehir bilgisini ana sayfa üzerinden değiştirebilecek.
