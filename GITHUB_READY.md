# ? GitHub Repository Setup - COMPLETE

## ?? **Everything is Ready!**

I've created all the necessary files for you to push your project to GitHub.

---

## ?? **Files Created**

| File | Purpose |
|------|---------|
| `README.md` | Main project documentation for GitHub |
| `LICENSE` | Academic project license |
| `.gitattributes` | Line ending configuration |
| `GITHUB_SETUP_GUIDE.md` | Detailed setup instructions |
| `QUICK_START_GITHUB.md` | Quick reference guide |
| `push-to-github.ps1` | Automated PowerShell script |

Plus your existing:
- ? `.gitignore` (already configured)
- ? All project files
- ? Documentation files

---

## ?? **Next Steps**

### **EASIEST METHOD: Use the PowerShell Script**

1. **Open PowerShell** in your project directory
2. **Run the script:**
   ```powershell
   cd "C:\2025-Semester-2\Application Security\Assignment 1\AppSecAssignment"
   .\push-to-github.ps1
   ```
3. **Follow the prompts**
4. **Done!** ?

### **MANUAL METHOD: Copy & Paste Commands**

Open PowerShell and run:

```powershell
cd "C:\2025-Semester-2\Application Security\Assignment 1\AppSecAssignment"
git init
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
git add .
git commit -m "Initial commit: Fresh Farm Market - Secure Web Application"
git branch -M main
git push -u origin main
```

---

## ?? **CRITICAL: Make Repository Private!**

### **After pushing, immediately:**

1. Go to: **https://github.com/T-Kishan/AppSecAssn2/settings**
2. Scroll to **"Danger Zone"** (bottom of page)
3. Click **"Change visibility"**
4. Select **"Make private"**
5. Type `T-Kishan/AppSecAssn2` to confirm
6. Click **"I understand, change repository visibility"**

### **Why?**
Your project contains sensitive data:
- ?? RSA Private Keys (`DigitalSignature.cs`)
- ?? AES Encryption Keys (`Protect.cs`)
- ?? reCAPTCHA Secret Keys (`appsettings.json`)
- ?? Database Connection Strings

**Private repository keeps these safe!**

---

## ?? **Authentication**

GitHub requires a **Personal Access Token** (not password):

### **Generate Token:**
1. Go to: https://github.com/settings/tokens
2. Click **"Generate new token (classic)"**
3. Name it: `AppSecAssn2`
4. Select scope: **`repo`** (full control of private repositories)
5. Click **"Generate token"**
6. **Copy the token** (you won't see it again!)

### **Use Token:**
When prompted for password during `git push`, paste the token.

### **Save Token (Optional):**
```bash
git config credential.helper store
```
Next time you push, credentials will be saved.

---

## ?? **Add Instructor as Collaborator**

After making repository private:

1. Go to: https://github.com/T-Kishan/AppSecAssn2/settings/access
2. Click **"Invite a collaborator"**
3. Enter instructor's GitHub username
4. Select access level:
   - **Read**: View code only
   - **Write**: Can make changes
5. Click **"Add [username] to this repository"**
6. Instructor will receive email invitation

---

## ? **Verification Checklist**

After pushing, verify:

- [ ] Repository exists: https://github.com/T-Kishan/AppSecAssn2
- [ ] All files are present (check file count)
- [ ] `README.md` displays properly on main page
- [ ] Repository shows ?? **Private** badge
- [ ] No sensitive data visible in code
- [ ] `.gitignore` is working (no `bin/`, `obj/` folders)
- [ ] Instructor added as collaborator

---

## ?? **Future Updates**

After the initial push, to make changes:

```bash
# Stage all changes
git add .

# Or stage specific file
git add path/to/file.cs

# Commit with message
git commit -m "Description of changes"

# Push to GitHub
git push
```

### **Good Commit Messages:**
? `"Fix: RSA key persistence issue"`  
? `"Add: Audit log viewer dashboard"`  
? `"Update: Security headers configuration"`  
? `"changes"`  
? `"update"`  
? `"fix bug"`

---

## ?? **Common Issues**

### **Issue 1: "remote origin already exists"**
```bash
git remote remove origin
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
```

### **Issue 2: "failed to push some refs"**
```bash
git pull origin main --allow-unrelated-histories
git push -u origin main
```

### **Issue 3: "Authentication failed"**
- Make sure you're using Personal Access Token, not password
- Generate new token at: https://github.com/settings/tokens
- Token needs `repo` scope

### **Issue 4: "Permission denied"**
- Check you have write access to the repository
- Verify repository ownership

### **Issue 5: Large file warning**
```bash
# Check what's being committed
git status

# Check file sizes
git ls-files -s

# Remove large file from staging
git reset HEAD path/to/large/file
```

---

## ?? **What Will Be Pushed**

### **Included (Good):**
? Source code (`.cs`, `.cshtml`)  
? Configuration files (`Program.cs`, `appsettings.json`)  
? Documentation (`.md` files)  
? Migrations  
? Views and Razor Pages  
? Services and Models  
? `.gitignore` and `.gitattributes`

### **Excluded (Good):**
? `bin/` and `obj/` folders  
? `.vs/` folder  
? Database files (`.mdf`, `.ldf`)  
? User uploads (`wwwroot/uploads/`)  
? `appsettings.Development.json`  
? Build artifacts  

---

## ?? **For Your Assignment Submission**

### **What to Submit:**

1. **GitHub Repository Link:**
   ```
   https://github.com/T-Kishan/AppSecAssn2
   ```

2. **Ensure Repository is Private**

3. **Add Instructor as Collaborator**

4. **Include in Documentation:**
   - Repository URL
   - Setup instructions (already in README.md)
   - Security features implemented
   - Testing evidence (screenshots)

---

## ?? **Take Screenshots**

Before submitting, capture:
- ? GitHub repository page (showing private badge)
- ? File structure
- ? README.md display
- ? Commit history
- ? Collaborators page (showing instructor)

---

## ?? **Summary**

You now have:

1. ? **Complete project documentation** (README.md)
2. ? **Automated push script** (push-to-github.ps1)
3. ? **Quick reference guide** (QUICK_START_GITHUB.md)
4. ? **Detailed setup guide** (GITHUB_SETUP_GUIDE.md)
5. ? **License file** (LICENSE)
6. ? **Git configuration** (.gitignore, .gitattributes)

### **Ready to push?**

**Option 1 (Easiest):**
```powershell
.\push-to-github.ps1
```

**Option 2 (Manual):**
```bash
git init
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
git add .
git commit -m "Initial commit: Fresh Farm Market - Secure Web Application"
git branch -M main
git push -u origin main
```

---

## ?? **You're All Set!**

Your project is ready to be pushed to GitHub with:
- ? Professional README
- ? Complete documentation
- ? Proper git configuration
- ? Security considerations documented

**Good luck with your assignment!** ??

---

**Repository**: https://github.com/T-Kishan/AppSecAssn2  
**Status**: Ready to Push  
**Created**: January 2025
