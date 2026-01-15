# 🚀 WarSpace: 2D Space Shooter (C# OOP)

WarSpace, C# ve Windows Forms (WinForms) kullanılarak geliştirilmiş, Nesne Yönelimli Programlama (OOP) prensiplerini derinlemesine uygulayan bir arcade shooter oyunudur. Proje, dinamik nesne yönetimi, kalıtım hiyerarşisi ve gerçek zamanlı oyun döngüsü mekaniklerini içermektedir.

## 🛠️ Öne Çıkan Teknik Özellikler

* **Gelişmiş OOP Mimarisi:** * `Enemy` soyut sınıfı (abstract class) üzerinden türetilen farklı düşman tipleri.
    * `virtual` ve `override` metodları ile özelleştirilmiş düşman davranışları.
* **Zengin Düşman Hiyerarşisi:** * **BasicEnemy:** Standart tek vuruşluk birimler.
    * **FastEnemy:** Hızlı hareket eden ve gemiye çarpmaya odaklanan birimler.
    * **StrongEnemy:** Yüksek zırh/sağlık puanına sahip dayanıklı gemiler.
    * **BossEnemy:** Seviye ilerledikçe ortaya çıkan, dikey hareket kapasitesine sahip ana rakipler.
* **Performans Odaklı Grafik Yönetimi:** `DoubleBuffered` ve `OptimizedDoubleBuffer` teknikleri ile WinForms ortamında titremesiz (flicker-free) 60 FPS deneyimi.
* **Dinamik Zorluk Sistemi:** Skor arttıkça seviye atlama, düşmanların ateş sıklığının (`shootInterval`) artması ve meteor yağmuru yoğunluğunun yükselmesi.



## 🎮 Oyun Mekanikleri

1.  **Gemi Gelişimi (Power-Ups):** Düşmanlardan düşen paketlerle geminizin canını, hızını ve mermi hasarını artırabilirsiniz.
2.  **Çarpışma Denetimi:** `CollisionDetector` statik sınıfı ile tüm oyun nesneleri arasında hassas `Rectangle.IntersectsWith` kontrolleri.
3.  **Hız ve Refleks:** Rastgele spawn olan meteorlar ve düşman mermileri arasında manevra kabiliyeti.

## 📋 Sınıf Yapısı (Class Structure)

* **`Game.cs`**: Oyunun ana motoru. Listeleri (Bullets, Enemies, Meteors) ve oyun döngüsünü yönetir.
* **`Enemy.cs` (Abstract)**: Tüm düşmanların temel özelliklerini (Ateş etme, hareket, çizim) barındıran temel sınıf.
* **`Spaceship.cs`**: Oyuncu karakteri, sağlık yönetimi ve hareket sınırlandırmaları.
* **`CollisionDetector.cs` (Static)**: Merkezi çarpışma algılama modülü.



## ⌨️ Kontroller

| Tuş | İşlem |
| :--- | :--- |
| **Yön Tuşları** | Uzay Gemisi Hareketi |
| **Space (Boşluk)** | Ateş Etme |
| **Herhangi Bir Tuş** | Oyunu Başlatır |

## ⚙️ Kurulum ve Çalıştırma

1.  Repoyu klonlayın: `git clone https://github.com/kullanici-adin/WarSpace.git`
2.  Visual Studio ile `.sln` dosyasını açın.
3.  `Assets` klasöründeki resimlerin (`spaceship.png`, `bossEnemy.png` vb.) projenin çıktı dizininde olduğundan emin olun.
4.  Projeyi derleyin ve başlatın.

## 👤 Geliştirici
* **İsim:** NTOOUKAN GENI MOUMIN
* **Bölüm:** Software Engineering
* **Öğrenci No:** 220229079
