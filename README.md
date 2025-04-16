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
  - OpenAI API üzerinden içerik düzeltme
- 📄 **Kategoriye Göre Filtreleme**
  - Yeni kategori ekleyebilme
  - Aynı kategori varsa tekrar eklememe kontrolü
- 🌐 **RESTful API Geliştirme**
  - JWT Token korumalı API endpoint’leri (GET, POST, PUT, DELETE)
- 💎 **Frontend**
  - Bootstrap ile responsive tasarım
  - Navbar, footer, welcome sayfası ve özel alert stilleri
- 💰 **Premium Üyelik Simülasyonu**
  - Ödeme simülasyonu sonrası rol güncelleme
  - Navbar’da “⭐ Premiumsun!” uyarısı

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


