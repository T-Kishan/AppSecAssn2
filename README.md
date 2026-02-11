# ?? Fresh Farm Market - Secure Web Application

A production-ready ASP.NET Core 9.0 web application demonstrating enterprise-level security practices including encryption, digital signatures, authentication, and comprehensive audit logging.

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13.0-green)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-Academic-yellow)](LICENSE)

---

## ?? **Table of Contents**

- [Features](#-features)
- [Security Implementation](#-security-implementation)
- [Technology Stack](#-technology-stack)
- [Installation](#-installation)
- [Configuration](#-configuration)
- [Usage](#-usage)
- [Database Schema](#-database-schema)
- [Security Audit](#-security-audit)
- [Screenshots](#-screenshots)
- [Contributing](#-contributing)
- [License](#-license)

---

## ? **Features**

### **User Management**
- ? User registration with photo upload (.jpg only)
- ? Secure login with session management
- ? Account lockout after 3 failed attempts
- ? Password complexity enforcement (12+ chars, mixed case, numbers, special chars)
- ? Automatic data integrity verification on login

### **Security Features**
- ? SHA-512 password hashing (never stored in plain text)
- ? AES-256 credit card encryption
- ? RSA-2048 digital signatures for data integrity
- ? Google reCAPTCHA v3 bot protection
- ? CSRF protection on all POST methods
- ? XSS prevention via HtmlEncode
- ? Security headers (X-Frame-Options, CSP, HSTS, etc.)
- ? HTTPS-only session cookies
- ? 10-minute session timeout

### **Audit & Monitoring**
- ? Comprehensive audit logging (login success/failure/lockout)
- ? Visual audit log viewer dashboard
- ? Real-time data integrity verification

---

## ?? **Security Implementation**

### **1. Cryptography**

#### **Password Hashing (SHA-512)**
```csharp
using (SHA512 sha512 = SHA512.Create())
{
    byte[] hash = sha512.ComputeHash(bytes);
    return Convert.ToBase64String(hash);
}
```

#### **Credit Card Encryption (AES-256)**
```csharp
using (Aes aes = Aes.Create())
{
    aes.Key = Encoding.UTF8.GetBytes(Key); // 256-bit key
    aes.IV = Encoding.UTF8.GetBytes(IV);   // 128-bit IV
    // Encryption/Decryption logic
}
```

#### **Digital Signatures (RSA-2048)**
```csharp
using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
{
    byte[] signature = rsa.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    return Convert.ToBase64String(signature);
}
```

### **2. Authentication & Authorization**
- Session-based authentication
- HttpOnly + Secure cookies
- Account lockout mechanism (3 attempts, 10-minute duration)
- Audit logging for all authentication events

### **3. Input Validation**
- Server-side file validation (.jpg only)
- Password regex: `^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}$`
- Email duplicate checking
- HtmlEncode on all user-generated content

### **4. Security Headers**
```csharp
X-Frame-Options: DENY
X-Content-Type-Options: nosniff
X-XSS-Protection: 1; mode=block
Referrer-Policy: no-referrer
Strict-Transport-Security: max-age=31536000
```

---

## ??? **Technology Stack**

| Component | Technology |
|-----------|------------|
| **Framework** | ASP.NET Core 9.0 (MVC) |
| **Language** | C# 13.0 |
| **Database** | SQL Server (LocalDB) |
| **ORM** | Entity Framework Core 9.0 |
| **Authentication** | Custom Session-based |
| **Encryption** | .NET System.Security.Cryptography |
| **Bot Protection** | Google reCAPTCHA v3 |
| **Frontend** | Bootstrap 5, jQuery |

### **NuGet Packages**
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.11" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.11" />
```

---

## ?? **Installation**

### **Prerequisites**
- .NET 9.0 SDK
- SQL Server LocalDB
- Visual Studio 2022 (or VS Code with C# extensions)
- Git

### **Setup Steps**

1. **Clone the repository**
```bash
git clone https://github.com/T-Kishan/AppSecAssn2.git
cd AppSecAssn2/AppSecAssignment
```

2. **Restore NuGet packages**
```bash
dotnet restore
```

3. **Update Database**
```bash
dotnet ef database update
```

4. **Configure settings** (See [Configuration](#-configuration) section)

5. **Run the application**
```bash
dotnet run
```

6. **Navigate to**
```
https://localhost:7264
```

---

## ?? **Configuration**

### **1. Update `appsettings.json`**

```json
{
  "ConnectionStrings": {
    "AuthConnectionString": "Server=(localdb)\\mssqllocaldb;Database=FreshFarmMarketDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "ReCaptcha": {
    "SiteKey": "YOUR_RECAPTCHA_SITE_KEY",
    "SecretKey": "YOUR_RECAPTCHA_SECRET_KEY"
  }
}
```

### **2. Get reCAPTCHA Keys**
1. Visit https://www.google.com/recaptcha/admin/create
2. Register your domain
3. Select reCAPTCHA v3
4. Copy Site Key and Secret Key to `appsettings.json`

### **3. Security Keys (Production)**
?? **For production deployments**, replace hardcoded keys in:
- `Services/Protect.cs` (AES keys)
- `Services/DigitalSignature.cs` (RSA keys)

Use Azure Key Vault or environment variables instead.

---

## ?? **Usage**

### **Registration**
1. Navigate to `/Account/Register`
2. Fill in required fields:
   - Full Name
   - Email (unique)
   - Password (12+ chars, mixed case, numbers, special chars)
   - Credit Card (will be encrypted)
   - Gender, Mobile, Delivery Address
   - Photo (.jpg only)
   - About Me
3. Complete reCAPTCHA
4. Submit

### **Login**
1. Navigate to `/Account/Login`
2. Enter email and password
3. Complete reCAPTCHA
4. ?? Account locks after 3 failed attempts (10-minute duration)

### **View Audit Logs**
1. Navigate to `/Home/AuditLogs`
2. View color-coded authentication events:
   - ?? Green: Login Success
   - ?? Yellow: Login Failed
   - ?? Red: Account Locked

---

## ??? **Database Schema**

### **Users Table**
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary Key |
| FullName | nvarchar(max) | User's full name (XSS sanitized) |
| Email | nvarchar(max) | Unique email address |
| PasswordHash | nvarchar(max) | SHA-512 hashed password |
| CreditCardNo | nvarchar(max) | AES-256 encrypted card number |
| Gender | nvarchar(max) | M/F |
| MobileNo | nvarchar(max) | Phone number |
| DeliveryAddress | nvarchar(max) | Shipping address |
| PhotoPath | nvarchar(max) | Uploaded photo filename |
| AboutMe | nvarchar(max) | User bio (XSS sanitized) |
| AboutMeSignature | nvarchar(max) | RSA-2048 digital signature |
| FailedLoginCount | int | Failed login attempts counter |
| LockoutEnd | datetime2 | Account lockout expiration |

### **AuditLogs Table**
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary Key |
| UserId | nvarchar(max) | User email |
| Action | nvarchar(max) | Event type (Login Success/Failed/Locked) |
| Timestamp | datetime2 | Event timestamp |

---

## ?? **Security Audit**

### **Compliance Checklist**

| Security Feature | Status | Evidence |
|------------------|--------|----------|
| Password Hashing (SHA-512) | ? | `Protect.cs` Line 9-17 |
| Credit Card Encryption (AES-256) | ? | `Protect.cs` Line 64-88 |
| Digital Signatures (RSA-2048) | ? | `DigitalSignature.cs` |
| Password Complexity | ? | `RegisterViewModel.cs` Line 31 |
| File Upload Validation | ? | `AccountController.cs` Line 57-62 |
| XSS Prevention | ? | `AccountController.cs` Line 82, 88-91 |
| CSRF Protection | ? | `AccountController.cs` Line 29, 137 |
| Account Lockout | ? | `AccountController.cs` Line 203-216 |
| Audit Logging | ? | `AuditLog.cs`, `AccountController.cs` |
| Session Security | ? | `Program.cs` Line 23-28 |
| Security Headers | ? | `Program.cs` Line 50-56 |
| reCAPTCHA v3 | ? | `Login.cshtml`, `Register.cshtml` |
| Error Handling | ? | `Program.cs` Line 44 |
| Email Duplicate Check | ? | `AccountController.cs` Line 44 |

**Overall Compliance**: ? **100%** (14/14 features implemented)

---

## ?? **Screenshots**

### **Registration Page**
- Comprehensive form with validation
- reCAPTCHA v3 integration
- File upload (.jpg only)

### **Login Page**
- Clean, responsive design
- Failed attempt tracking
- Account lockout notification

### **Home Dashboard**
- User profile display
- Data integrity verification badge
- Decrypted credit card view (for demonstration)

### **Audit Logs**
- Color-coded event table
- Last 50 authentication events
- Timestamp tracking

---

## ?? **Contributing**

This is an academic project. Contributions are not currently accepted.

---

## ?? **License**

This project is for educational purposes only.

**Academic Project** - Application Security Course  
**Semester**: Semester 2, 2025  
**Institution**: [Your Institution Name]

---

## ?? **Author**

**T-Kishan**  
GitHub: [@T-Kishan](https://github.com/T-Kishan)

---

## ?? **References**

- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [.NET Security Best Practices](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [Microsoft Cryptography Documentation](https://docs.microsoft.com/en-us/dotnet/standard/security/cryptography-model)

---

## ?? **Disclaimer**

This application is designed for **educational purposes** to demonstrate security best practices. 

For production use:
- Replace hardcoded encryption keys with Azure Key Vault
- Implement PCI-DSS compliance for credit card handling
- Add additional security layers (rate limiting, etc.)
- Conduct professional security audit
- Implement proper logging and monitoring

---

**Last Updated**: January 2025  
**Status**: ? Production-Ready (Academic)  
**Version**: 1.0.0
