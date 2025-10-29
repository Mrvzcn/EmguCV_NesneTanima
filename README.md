# EmguCv Nesne (Yüz) Tanıma
* Bilgisayar kamerasından **canlı görüntü** alır ve **Haar Cascade** algoritması ile yüzleri tespit eder.  
* Gerçek zamanlı görüntü işleme örneğidir.
## 🧩 Özellikler
* 🎥 Bilgisayar kamerasından canlı görüntü akışı.  
* 👁️ Haar Cascade modeliyle yüz tanıma.  
* 🟥 Tespit edilen yüzlerin etrafına kırmızı dikdörtgen çizimi.  
* ⏯️ Kamera başlat/durdur buton kontrolü.  
* 🧹 Kaynak yönetimi (kapanışta bellek serbest bırakılır).  
## ⚙️ Kullanılan Teknolojiler
* C# (.NET 6 veya .NET Framework 4.8)  
* Emgu.CV (OpenCV .NET Wrapper)
* Windows Forms
## 🖥️ Arayüz
* PictureBox: Kameradan alınan görüntüyü gösterir.
* Button: Kamerayı başlatır veya durdurur.
* Form1: Görüntü yakalama, yüz tespiti ve UI güncellemelerini yönetir.
## 🧠 Kod Yapısı
* Form1.cs → Kamera kontrolü, görüntü işleme, yüz tanıma.
* Form1.Designer.cs → Form bileşenlerinin tasarımı.
* haarcascade_frontalface_default.xml → Haar Cascade model dosyası.
## 🧑‍💻 Geliştirici Bilgileri
Merve Özcan  
İletişim: mrvzcn10@gmail.com
