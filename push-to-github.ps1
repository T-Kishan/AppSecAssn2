# ============================================
# Git Setup and Push Script
# Repository: https://github.com/T-Kishan/AppSecAssn2.git
# ============================================

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Fresh Farm Market - GitHub Setup Script" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Change to project directory
$projectPath = "C:\2025-Semester-2\Application Security\Assignment 1\AppSecAssignment"
Write-Host "Navigating to project directory..." -ForegroundColor Yellow
Set-Location $projectPath

# Check if .git exists
if (Test-Path ".git") {
    Write-Host "Git repository already initialized." -ForegroundColor Green
} else {
    Write-Host "Initializing Git repository..." -ForegroundColor Yellow
    git init
}

# Check if remote exists
$remotes = git remote -v 2>$null
if ($remotes -match "origin") {
    Write-Host "Remote 'origin' already exists:" -ForegroundColor Green
    git remote -v
    
    $response = Read-Host "`nDo you want to update the remote URL? (y/n)"
    if ($response -eq "y") {
        git remote remove origin
        git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
        Write-Host "Remote updated!" -ForegroundColor Green
    }
} else {
    Write-Host "Adding remote repository..." -ForegroundColor Yellow
    git remote add origin https://github.com/T-Kishan/AppSecAssn2.git
    Write-Host "Remote added successfully!" -ForegroundColor Green
}

Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  SECURITY WARNING" -ForegroundColor Red
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Your project contains sensitive data:" -ForegroundColor Yellow
Write-Host "  - RSA Private Keys (DigitalSignature.cs)" -ForegroundColor Yellow
Write-Host "  - AES Encryption Keys (Protect.cs)" -ForegroundColor Yellow
Write-Host "  - reCAPTCHA Keys (appsettings.json)" -ForegroundColor Yellow
Write-Host "  - Database Connection Strings" -ForegroundColor Yellow
Write-Host ""
Write-Host "RECOMMENDATION: Make your repository PRIVATE" -ForegroundColor Red
Write-Host ""

$visibility = Read-Host "Is your GitHub repository PRIVATE? (y/n)"

if ($visibility -ne "y") {
    Write-Host ""
    Write-Host "WARNING: Public repository detected!" -ForegroundColor Red
    Write-Host "You should:" -ForegroundColor Yellow
    Write-Host "  1. Go to https://github.com/T-Kishan/AppSecAssn2/settings" -ForegroundColor Yellow
    Write-Host "  2. Scroll to 'Danger Zone'" -ForegroundColor Yellow
    Write-Host "  3. Click 'Change visibility' -> 'Make private'" -ForegroundColor Yellow
    Write-Host ""
    
    $continue = Read-Host "Continue anyway? (y/n)"
    if ($continue -ne "y") {
        Write-Host "Aborted. Please make your repository private first." -ForegroundColor Red
        exit
    }
}

Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Preparing to commit and push..." -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Check git status
Write-Host "Current Git Status:" -ForegroundColor Yellow
git status --short

Write-Host ""
$proceed = Read-Host "Do you want to stage all files and commit? (y/n)"

if ($proceed -eq "y") {
    # Stage all files
    Write-Host "`nStaging all files..." -ForegroundColor Yellow
    git add .
    
    # Create commit
    Write-Host "`nCreating commit..." -ForegroundColor Yellow
    git commit -m "Initial commit: Fresh Farm Market - Secure ASP.NET Core Application

Features:
- SHA-512 password hashing
- AES-256 credit card encryption  
- RSA-2048 digital signatures with verification
- Google reCAPTCHA v3 integration
- Account lockout mechanism (3 attempts, 10-min duration)
- Comprehensive audit logging with dashboard
- Session security (HttpOnly, Secure cookies)
- Security headers (X-Frame-Options, CSP, HSTS)
- CSRF protection on all POST methods
- XSS prevention via HtmlEncode
- Custom error pages (404, etc.)
- Input validation & file upload restrictions

Technical Stack:
- .NET 9.0
- ASP.NET Core MVC
- Entity Framework Core 9.0
- SQL Server LocalDB
- Bootstrap 5"
    
    # Set main branch
    Write-Host "`nSetting main branch..." -ForegroundColor Yellow
    git branch -M main
    
    # Push to GitHub
    Write-Host "`nPushing to GitHub..." -ForegroundColor Yellow
    Write-Host "You may be prompted for GitHub credentials..." -ForegroundColor Cyan
    Write-Host ""
    
    git push -u origin main
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "================================================" -ForegroundColor Green
        Write-Host "  SUCCESS! Your code is now on GitHub!" -ForegroundColor Green
        Write-Host "================================================" -ForegroundColor Green
        Write-Host ""
        Write-Host "Repository URL: https://github.com/T-Kishan/AppSecAssn2" -ForegroundColor Cyan
        Write-Host ""
        Write-Host "Next steps:" -ForegroundColor Yellow
        Write-Host "  1. Visit your repository to verify files" -ForegroundColor Yellow
        Write-Host "  2. Ensure repository is PRIVATE" -ForegroundColor Yellow
        Write-Host "  3. Add instructor as collaborator (Settings > Collaborators)" -ForegroundColor Yellow
        Write-Host ""
    } else {
        Write-Host ""
        Write-Host "================================================" -ForegroundColor Red
        Write-Host "  ERROR: Push failed!" -ForegroundColor Red
        Write-Host "================================================" -ForegroundColor Red
        Write-Host ""
        Write-Host "Common solutions:" -ForegroundColor Yellow
        Write-Host "  1. Check your GitHub credentials" -ForegroundColor Yellow
        Write-Host "  2. Generate a Personal Access Token:" -ForegroundColor Yellow
        Write-Host "     https://github.com/settings/tokens" -ForegroundColor Yellow
        Write-Host "  3. Use token as password when prompted" -ForegroundColor Yellow
        Write-Host ""
    }
} else {
    Write-Host "`nAborted by user." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
