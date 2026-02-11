# ?? Quick Start: Push to GitHub

## **Option 1: Use PowerShell Script (Recommended)**

### **Step 1: Run the script**
```powershell
cd "C:\2025-Semester-2\Application Security\Assignment 1\AppSecAssignment"
.\push-to-github.ps1
```

The script will:
- ? Initialize Git repository
- ? Add remote repository
- ? Warn about sensitive data
- ? Stage and commit all files
- ? Push to GitHub

---

## **Option 2: Manual Commands**

### **Step 1: Open PowerShell**
Press `Win + X` ? Select "Windows PowerShell" or "Terminal"

### **Step 2: Navigate to project**
```powershell
cd "C:\2025-Semester-2\Application Security\Assignment 1\AppSecAssignment"
```

### **Step 3: Run these commands one by one**
```bash
# Initialize Git
git init

# Add remote repository
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git

# Stage all files
git add .

# Commit with message
git commit -m "Initial commit: Fresh Farm Market - Secure Web Application"

# Set main branch
git branch -M main

# Push to GitHub
git push -u origin main
```

---

## ?? **IMPORTANT: Make Repository Private!**

1. Go to: https://github.com/T-Kishan/AppSecAssn2/settings
2. Scroll to **"Danger Zone"**
3. Click **"Change visibility"**
4. Select **"Make private"**
5. Type the repository name to confirm
6. Click **"I understand, change repository visibility"**

---

## ?? **Why Private?**

Your project contains:
- ? RSA Private Keys
- ? AES Encryption Keys
- ? reCAPTCHA Secrets
- ? Database Connection Strings

**Private repo = Keys stay safe!**

---

## ?? **Add Instructor as Collaborator**

1. Go to: https://github.com/T-Kishan/AppSecAssn2/settings/access
2. Click **"Add people"**
3. Enter instructor's GitHub username
4. Select **"Write"** or **"Read"** access
5. Click **"Add to repository"**

---

## ?? **Future Updates**

After initial push, use these commands:

```bash
# Stage changes
git add .

# Commit changes
git commit -m "Description of changes"

# Push to GitHub
git push
```

---

## ?? **Troubleshooting**

### **Authentication Error**
GitHub no longer accepts passwords. Use **Personal Access Token**:

1. Go to: https://github.com/settings/tokens
2. Click **"Generate new token (classic)"**
3. Select scopes: `repo` (full control)
4. Click **"Generate token"**
5. Copy the token (you won't see it again!)
6. Use token as password when pushing

### **"Remote origin already exists"**
```bash
git remote remove origin
git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
```

### **"refusing to merge unrelated histories"**
```bash
git pull origin main --allow-unrelated-histories
git push -u origin main
```

### **Check what will be committed**
```bash
git status
```

### **View commit history**
```bash
git log --oneline
```

---

## ? **Verification Checklist**

After pushing:
- [ ] Visit https://github.com/T-Kishan/AppSecAssn2
- [ ] Verify files are there
- [ ] Check repository is PRIVATE (lock icon)
- [ ] Ensure no sensitive data in commit history
- [ ] Add instructor as collaborator
- [ ] Test clone/pull to verify it works

---

## ?? **Need Help?**

See detailed guide: `GITHUB_SETUP_GUIDE.md`

---

**Repository**: https://github.com/T-Kishan/AppSecAssn2  
**Created**: January 2025
