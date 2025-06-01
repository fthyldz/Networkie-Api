# Networkie-Api

## Teknolojiler ve Sürümler

- .NET 8.0
- PostgreSQL (Supabase)
- Docker

## Kullanılan Kütüphaneler

### API Katmanı
- Carter (8.1.0) - Minimal API desteği
- Microsoft.EntityFrameworkCore.Tools (8.0.15)
- System.Text.Json (8.0.5)

### Application Katmanı
- FluentValidation (11.11.0)
- FluentValidation.AspNetCore (11.3.0)
- FluentValidation.DependencyInjectionExtensions (11.11.0)
- MediatR (12.4.1)
- Microsoft.Extensions.DependencyInjection.Abstractions (8.0.2)

### Infrastructure Katmanı
- BCrypt.Net-Next (4.0.3)
- brevo_csharp (1.0.0)
- MailKit (4.12.1)
- Microsoft.AspNetCore.Authentication.JwtBearer (8.0.14)

### Persistence Katmanı
- Microsoft.EntityFrameworkCore.InMemory (8.0.14)
- Npgsql.EntityFrameworkCore.PostgreSQL (8.0.11)

## Proje Yapısı

Proje, Clean Architecture prensiplerine uygun olarak aşağıdaki katmanlardan oluşmaktadır:

- **Networkie.Api**: API katmanı, HTTP isteklerini karşılar
- **Networkie.Application**: İş mantığı katmanı
- **Networkie.Infrastructure**: Altyapı servisleri (Email, Token, Crypto vb.)
- **Networkie.Persistence**: Veritabanı işlemleri
- **Networkie.Entities**: Domain modelleri
- **Networkie.Persistence.Abstractions**: Repository ve UnitOfWork arayüzleri
- **Networkie.Persistence.EntityFrameworkCore**: Entity Framework Core implementasyonları

## Çalıştırma

### Geliştirme Ortamı

1. PostgreSQL veritabanını kurun ve bağlantı bilgilerini `appsettings.Development.json` dosyasında güncelleyin
2. Projeyi derleyin:
```bash
dotnet build
```
3. API'yi çalıştırın:
```bash
dotnet run --project Networkie.Api
```

### Docker ile Çalıştırma

1. Docker imajını oluşturun:
```bash
docker build -t networkie-api .
```

2. Container'ı çalıştırın:
```bash
docker run -p 5003:5003 networkie-api
```

## Özellikler

- JWT tabanlı kimlik doğrulama
- Email doğrulama sistemi
- Rol tabanlı yetkilendirme
- CRUD operasyonları
- Sayfalama ve filtreleme
- CORS yapılandırması
- In-memory önbellek
- FluentValidation ile veri doğrulama
- MediatR ile CQRS pattern implementasyonu
