# Fresh Farm Market - Secure Web Application

A secure ASP.NET Core 9.0 web application demonstrating enterprise-level security practices including encryption, authentication, and audit logging.

## ?? Security Features

### **1. Password Security**
- ? SHA512 hashing for password storage
- ? Minimum 12 characters with complexity requirements
- ? Passwords never stored in plain text

### **2. Data Encryption**
- ? AES-256 encryption for credit card numbers
- ? Built-in .NET `System.Security.Cryptography`
- ? Secure key and IV management

### **3. Authentication & Authorization**
- ? Session-based authentication
- ? HttpOnly cookies to prevent XSS
- ? Account lockout after 3 failed login attempts
- ? 10-minute lockout duration

### **4. Audit Logging**
- ? Login success/failure tracking
- ? Account lockout events logged
- ? Timestamp tracking for forensics

### **5. Input Validation & XSS Protection**
- ? Server-side file validation (.jpg only)
- ? HtmlEncode for user-generated content
- ? CSRF protection (ValidateAntiForgeryToken)
- ? Model validation with Data Annotations

### **6. Data Integrity**
- ? RSA-2048 digital signatures
- ? SHA256 signature hashing
- ? Data tampering detection

### **7. Security Headers**
- ? X-Frame-Options: DENY
- ? X-Content-Type-Options: nosniff
- ? X-XSS-Protection: 1; mode=block
- ? Referrer-Policy: no-referrer
- ? HSTS (HTTP Strict Transport Security)

### **8. Anti-Bot Protection**
- ? Google reCAPTCHA v3 integration
- ? Score-based verification (threshold: 0.5)

## ??? Technology Stack

- **Framework:** ASP.NET Core 9.0 (MVC)
- **Database:** SQL Server (LocalDB)
- **ORM:** Entity Framework Core 9.0
- **Authentication:** Custom session-based
- **Encryption:** Built-in .NET Cryptography
- **Anti-Bot:** Google reCAPTCHA v3

## ?? NuGet Packages

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.11" />
```

## ?? Setup Instructions

### **1. Prerequisites**
- .NET 9.0 SDK
- SQL Server LocalDB
- Visual Studio 2022 or later

### **2. Database Setup**
```bash
# Run migrations
dotnet ef database update
```

### **3. Configure reCAPTCHA**
1. Get keys from https://www.google.com/recaptcha/admin/create
2. Copy `appsettings.example.json` to `appsettings.json`
3. Replace `YOUR_SITE_KEY_HERE` and `YOUR_SECRET_KEY_HERE` with real keys

### **4. Run the Application**
```bash
dotnet run
```

Navigate to: `https://localhost:7264`

## ?? Application Features

### **Registration**
- Full Name
- Email (unique)
- Password (12+ chars, mixed case, numbers, special chars)
- Credit Card (encrypted)
- Gender
- Mobile Number
- Delivery Address
- Photo Upload (.jpg only)
- About Me (XSS protected)

### **Login**
- Email/Password authentication
- Failed attempt tracking
- Account lockout mechanism
- Audit logging

### **Home Dashboard**
- User profile display
- Decrypted credit card view
- Data integrity verification
- Photo display

## ?? Cryptography Implementation

### **Password Hashing (SHA512)**
```csharp
using (SHA512 sha512 = SHA512.Create())
{
    byte[] hash = sha512.ComputeHash(bytes);
    return Convert.ToBase64String(hash);
}
```

### **Credit Card Encryption (AES-256)**
```csharp
using (Aes aes = Aes.Create())
{
    aes.Key = Encoding.UTF8.GetBytes(Key); // 256-bit key
    aes.IV = Encoding.UTF8.GetBytes(IV);   // 128-bit IV
    // Encryption logic...
}
```

### **Digital Signature (RSA-2048)**
```csharp
using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
{
    byte[] signature = rsa.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
}
```

## ?? Database Schema

### **Users Table**
- Id (PK)
- FullName
- Email (Unique)
- PasswordHash (SHA512)
- CreditCardNo (AES Encrypted)
- Gender
- MobileNo
- DeliveryAddress
- PhotoPath
- AboutMe (XSS Sanitized)
- FailedLoginCount
- LockoutEnd
- AboutMeSignature (RSA)

### **AuditLogs Table**
- Id (PK)
- UserId
- Action (Login Success/Failed/Locked)
- Timestamp

## ?? Academic Compliance

This project demonstrates compliance with:
- ? Practical 12: Error Handling & Status Pages
- ? Practical 13: Data Encryption & Protection
- ? Practical 14/15: Security Headers & Hardening

## ?? License

This is an academic project for educational purposes.

## ????? Author

**Course:** Application Security  
**Semester:** Semester 2, 2025  
**Assignment:** Fresh Farm Market Secure Web Application
