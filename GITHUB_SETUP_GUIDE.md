# ?? GitHub Repository Setup Guide
## Linking Your Project to: https://github.com/T-Kishan/AppSecAssn2.git

---

## ?? **IMPORTANT: Before Pushing to GitHub**

### **Security Concerns**
Your project contains **sensitive data** that should NOT be committed to a public repository:

1. **RSA Private Keys** in `Services/DigitalSignature.cs`
2. **AES Encryption Keys** in `Services/Protect.cs`
3. **reCAPTCHA Keys** in `appsettings.json`
4. **Database Connection Strings** in `appsettings.json`

### **Two Options:**

#### **Option 1: Make Repository PRIVATE** (Recommended for Assignment)
- This keeps your keys safe
- Instructor can still access it

#### **Option 2: Remove Sensitive Data** (Required for Public Repos)
- Replace real keys with placeholders
- Use environment variables or Azure Key Vault

---

## ?? **Step-by-Step Setup**

### **Step 1: Open Terminal in Your Project Directory**

Open PowerShell or Command Prompt and navigate to:
```bash
cd "C:\2025-Semester-2\Application Security\Assignment 1\AppSecAssignment"
```

### **Step 2: Initialize Git Repository**

```bash
git init
```

### **Step 3: Add Remote Repository**

```bash
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
```

### **Step 4: Create .gitignore (Already Exists)**

Your `.gitignore` is already set up. It will exclude:
- Build files (`bin/`, `obj/`)
- User-specific files (`.vs/`, `*.user`)
- Database files (`*.mdf`, `*.ldf`)
- User uploads (`wwwroot/uploads/`)

### **Step 5: Stage All Files**

```bash
git add .
```

### **Step 6: Create Initial Commit**

```bash
git commit -m "Initial commit: Fresh Farm Market - Secure Web Application"
```

### **Step 7: Set Main Branch**

```bash
git branch -M main
```

### **Step 8: Push to GitHub**

```bash
git push -u origin main
```

---

## ?? **SECURITY CHECKLIST**

Before pushing, verify you're handling sensitive data correctly:

### **If Repository is PRIVATE (Recommended):**
? You can push as-is (keys are safe in private repo)

### **If Repository is PUBLIC:**
? **DO NOT PUSH YET!** You must:

1. **Update `appsettings.json`:**
```json
{
  "ConnectionStrings": {
    "AuthConnectionString": "YOUR_CONNECTION_STRING_HERE"
  },
  "ReCaptcha": {
    "SiteKey": "YOUR_SITE_KEY_HERE",
    "SecretKey": "YOUR_SECRET_KEY_HERE"
  }
}
```

2. **Update `Services/Protect.cs`:**
```csharp
// EXAMPLE KEYS - Replace with your own
private static readonly byte[] Key = Encoding.UTF8.GetBytes("EXAMPLE-32-CHAR-KEY-REPLACE-THIS!");
private static readonly byte[] IV = Encoding.UTF8.GetBytes("EXAMPLE-16-CHARS!");
```

3. **Update `Services/DigitalSignature.cs`:**
```csharp
// EXAMPLE RSA KEY - Generate your own
private const string PRIVATE_KEY = @"REPLACE_WITH_YOUR_KEY";
private const string PUBLIC_KEY = @"REPLACE_WITH_YOUR_KEY";
```

---

## ?? **Quick Commands (Copy & Paste)**

### **All-in-One Setup:**
```bash
cd "C:\2025-Semester-2\Application Security\Assignment 1\AppSecAssignment"
git init
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
git add .
git commit -m "Initial commit: Fresh Farm Market - Secure ASP.NET Core Application

Features:
- SHA-512 password hashing
- AES-256 credit card encryption
- RSA-2048 digital signatures
- Google reCAPTCHA v3
- Account lockout & audit logging
- Session security with HTTPS-only cookies
- Security headers (X-Frame-Options, CSP, etc.)
- Custom error pages
- Input validation & XSS protection
- CSRF protection on all POST methods"
git branch -M main
git push -u origin main
```

---

## ?? **After Initial Push - Making Updates**

### **Stage Changes:**
```bash
git add .
```

### **Commit Changes:**
```bash
git commit -m "Description of your changes"
```

### **Push to GitHub:**
```bash
git push
```

---

## ?? **What Will Be Pushed**

### **Included Files:**
? Source code (`.cs` files)
? Views (`.cshtml` files)
? Configuration files (`Program.cs`, `appsettings.json`)
? Migrations
? `README.md`, `IMPLEMENTATION_STATUS.md`, etc.
? Documentation files

### **Excluded Files (by .gitignore):**
? `bin/` and `obj/` folders
? `.vs/` folder
? Database files (`.mdf`, `.ldf`)
? User uploads (`wwwroot/uploads/`)
? `appsettings.Development.json`

---

## ?? **Common Issues & Solutions**

### **Issue 1: "fatal: remote origin already exists"**
**Solution:**
```bash
git remote remove origin
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
```

### **Issue 2: "refusing to merge unrelated histories"**
**Solution:**
```bash
git pull origin main --allow-unrelated-histories
git push -u origin main
```

### **Issue 3: Authentication Required**
**Solution:**
- GitHub will prompt for credentials
- Use **Personal Access Token** instead of password
- Generate at: https://github.com/settings/tokens

### **Issue 4: Large Files Warning**
**Solution:**
- Check what's being committed: `git status`
- Ensure `.gitignore` is working
- Remove large files from staging: `git reset HEAD path/to/file`

---

## ?? **Recommended: Create README.md for GitHub**

Create a `README.md` in the root directory with project information:

```markdown
# Fresh Farm Market - Secure Web Application

A secure ASP.NET Core 9.0 web application demonstrating enterprise-level security practices.

## ?? Security Features

- **Password Security**: SHA-512 hashing, 12+ character complexity
- **Data Encryption**: AES-256 for credit cards, RSA-2048 signatures
- **Authentication**: Session-based with account lockout (3 attempts)
- **Audit Logging**: Comprehensive event tracking
- **Bot Protection**: Google reCAPTCHA v3
- **XSS Prevention**: HtmlEncode on all user inputs
- **CSRF Protection**: Anti-forgery tokens on POST methods
- **Security Headers**: X-Frame-Options, CSP, HSTS, etc.

## ?? Technologies

- .NET 9.0
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap 5
- Google reCAPTCHA v3

## ?? Installation

1. Clone the repository
2. Update `appsettings.json` with your configuration
3. Run migrations: `dotnet ef database update`
4. Run the application: `dotnet run`

## ?? Academic Project

This is an educational project for Application Security coursework.
```

---

## ? **Make Repository Private (Recommended)**

1. Go to: https://github.com/T-Kishan/AppSecAssn2
2. Click **Settings**
3. Scroll to **Danger Zone**
4. Click **Change visibility** ? **Make private**
5. Confirm

This keeps your encryption keys and reCAPTCHA tokens safe!

---

## ?? **Final Checklist**

Before pushing:
- [ ] Decide: Private or Public repository?
- [ ] If public: Remove/replace sensitive keys
- [ ] Verify `.gitignore` is working
- [ ] Create meaningful commit message
- [ ] Test that application still runs after changes

After pushing:
- [ ] Verify files on GitHub
- [ ] Check no sensitive data is exposed
- [ ] Add README.md with project description
- [ ] Add instructor as collaborator (if private)

---

## ?? **Need Help?**

If you encounter issues:
1. Check git status: `git status`
2. Check remote: `git remote -v`
3. Check log: `git log --oneline`
4. Force push (use carefully): `git push -f origin main`

---

**Created**: January 2025
**Repository**: https://github.com/T-Kishan/AppSecAssn2.git
**Author**: T-Kishan
