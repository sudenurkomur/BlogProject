# 📝 BlogProject

Bu proje, kullanıcıların blog yazıları oluşturabileceği, yorum yapabileceği ve özel olarak **Gemini destekli yazım düzeltme** özelliğini kullanabileceği bir ASP.NET Core MVC projesidir.

## 🚀 Proje Özellikleri

- 🔐 **Kullanıcı Giriş / Kayıt Sistemi**
  - Cookie tabanlı kimlik doğrulama
  - Role bazlı yetkilendirme (`User`, `Premium`)
- 🧑‍💻 **Blog Yönetimi**
  - Blog ekleme, silme, güncelleme
  - Yalnızca blog sahibinin düzenleyebilmesi
- 💬 **Yorum Sistemi**
  - Yorum yapma ve nested (cevaplı) yorumlar
  - Yorum sahibi dışındakilerin düzenleme yapamaması
- 🧠 **Gemini API Entegrasyonu**
  - Premium kullanıcılar için "Yazım Hatalarını Düzelt" butonu
  - Gemini API üzerinden içerik düzeltme
- 📄 **Kategoriye Göre Filtreleme**
  - Yeni kategori ekleyebilme
  - Aynı kategori varsa tekrar eklememe kontrolü
- 🆔 **Veri Tasarımı**
  - Tüm entity ID’leri Guid olarak tanımlanmıştır.
  - Blog görselleri veritabanında Base64 formatında saklanmaktadır.
- 🌐 **RESTful API Geliştirme**
  - JWT Token korumalı API endpoint’leri (GET, POST, PUT, DELETE)
- 💎 **Frontend**
  - Bootstrap ile responsive tasarım
  - Navbar, footer, welcome sayfası ve özel alert stilleri
- 💰 **Premium Üyelik Simülasyonu**
  - Ödeme simülasyonu sonrası rol güncelleme
  - Navbar’da “⭐ Premiumsun!” uyarısı
 
## 📸 Ekran Görüntüleri

### 🏠 Ana Ekran
![Ana Ekran](https://raw.githubusercontent.com/sudenurkomur/BlogProject/main/assets/AnaEkran.png)

### 📃 Blog Detayı
![Blog Detayı](https://raw.githubusercontent.com/sudenurkomur/BlogProject/main/assets/BlogDetayı.png)

### 📋 Blog Listesi
![Blog Listesi](https://raw.githubusercontent.com/sudenurkomur/BlogProject/main/assets/BlogListesi.png)

### 🔐 Giriş Yap
![Giriş Yap](https://raw.githubusercontent.com/sudenurkomur/BlogProject/main/assets/GirişYap.png)

### 📝 Kayıt Ol
![Kayıt Ol](https://raw.githubusercontent.com/sudenurkomur/BlogProject/main/assets/KayıtOl.png)

### 💎 Premium Üyelik
![Premium Ol](https://raw.githubusercontent.com/sudenurkomur/BlogProject/main/assets/PremiumOl.png)

### 👤 Profil Sayfası
![Profil](https://raw.githubusercontent.com/sudenurkomur/BlogProject/main/assets/Profil.png)


## 📦 Kullanılan Teknolojiler

- ASP.NET Core MVC (.NET 8.0)
- Entity Framework Core
- MS SQL Server
- FluentValidation
- JWT Authentication
- Bootstrap + LibMan
- Gemini API (Gemini entegrasyonu)

## 🚀 Kurulum

1. **Projeyi klonla**
```bash
git clone https://github.com/sudenurkomur/BlogProject.git

dotnet restore

dotnet ef database update

dotnet run


