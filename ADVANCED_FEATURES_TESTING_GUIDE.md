# Advanced Features Testing Guide

## Feature 4.2: Multiple Login Detection

### How It Works:
- When you login from a new browser/device, ALL previous sessions are terminated
- Previous browser windows will be logged out automatically
- Audit log records "Previous session terminated"

### Testing Steps:

1. **Chrome - First Login:**
   - Open Chrome
   - Navigate to `https://localhost:XXXX/Account/Login`
   - Login with your credentials
   - You should see Home page with your profile

2. **Edge - Second Login (Triggers Detection):**
   - Open Microsoft Edge (or Firefox)
   - Navigate to `https://localhost:XXXX/Account/Login`
   - Login with **THE SAME** credentials
   - You should see Home page

3. **Chrome - Verify Logout:**
   - Go back to Chrome window
   - Click on any link (e.g., "Home" or "Privacy")
   - You should be **automatically redirected to Login page**
   - You should see warning: "?? Session Expired: Your account was logged in from another device/browser"

4. **Verify Audit Log:**
   - Login again in Chrome
   - Go to `/Home/AuditLogs`
   - You should see entries like:
     - "Previous session terminated (Multiple login from ::1)"
     - "Login Success"

### Expected Result:
? Chrome session is invalidated when you login from Edge
? Warning message appears on login page
? Audit log shows session termination

---

## Feature 4.3: Two-Factor Authentication (2FA)

### How It Works:
- Toggle 2FA on/off from Home page
- When enabled, login requires entering a 6-digit code
- Code is displayed in a popup (demo mode - replace with SMS/Email in production)

### Testing Steps:

#### **Step 1: Enable 2FA**
1. Login to your account
2. On Home page, find "Security Settings" section
3. Toggle **"Enable Two-Factor Authentication (2FA)"** to ON
4. You should see alert: "2FA enabled successfully!"

#### **Step 2: Logout and Test 2FA Login**
1. Click **Logout**
2. Login with your email/password
3. Instead of going to Home page, you should be redirected to **2FA Verification** page
4. You'll see an alert with a 6-digit code (e.g., "Demo: 123456")
5. Enter the code in the input box
6. Click **"Verify Code"**
7. You should be logged in and redirected to Home page

#### **Step 3: Test Invalid Code**
1. Logout and login again
2. Note the 6-digit code shown in the alert
3. Enter a **wrong code** (e.g., "999999")
4. Click "Verify Code"
5. You should see error: "Invalid or expired code"

#### **Step 4: Test Expired Code**
1. Logout and login again
2. Note the 6-digit code
3. **Wait 6 minutes** (code expires in 5 minutes)
4. Enter the code
5. You should see error: "Invalid or expired code"

#### **Step 5: Disable 2FA**
1. Login successfully
2. On Home page, toggle **"Enable Two-Factor Authentication (2FA)"** to OFF
3. Logout and login again
4. You should go **directly to Home page** (no 2FA verification)

### Expected Results:
? 2FA toggle works on Home page
? Login requires 6-digit code when 2FA is enabled
? Code is displayed in demo alert
? Wrong code shows error message
? Expired code (after 5 min) shows error
? Disabling 2FA allows normal login

---

## Feature 4.1: Password History (Prevent Reuse)

### Testing Steps:

#### **Step 1: Change Password First Time**
1. Login to your account
2. Click **"Change Password"** in navigation
3. Enter:
   - Current Password: (your current password)
   - New Password: `NewPass@123456`
   - Confirm: `NewPass@123456`
4. Click "Change Password"
5. You should see: "Password changed successfully!"

#### **Step 2: Try to Reuse Same Password**
1. Click "Change Password" again
2. Enter:
   - Current Password: `NewPass@123456`
   - New Password: `NewPass@123456` (same as current)
   - Confirm: `NewPass@123456`
3. Click "Change Password"
4. You should see error: **"Cannot reuse last 2 passwords."**

#### **Step 3: Change to Second New Password**
1. Enter:
   - Current Password: `NewPass@123456`
   - New Password: `SecondPass@789`
   - Confirm: `SecondPass@789`
2. Click "Change Password"
3. Should succeed

#### **Step 4: Try to Reuse First Password**
1. Click "Change Password" again
2. Enter:
   - Current Password: `SecondPass@789`
   - New Password: `NewPass@123456` (first password)
   - Confirm: `NewPass@123456`
3. Click "Change Password"
4. You should see error: **"Cannot reuse last 2 passwords."**

#### **Step 5: Change to Third Password (Should Work)**
1. Enter:
   - Current Password: `SecondPass@789`
   - New Password: `ThirdPass@999`
   - Confirm: `ThirdPass@999`
2. Should succeed

#### **Step 6: Now First Password Can Be Reused**
1. Change password again:
   - Current: `ThirdPass@999`
   - New: `NewPass@123456` (first password - now >2 passwords ago)
2. This should **NOW WORK** (only last 2 are blocked)

### Expected Results:
? Cannot reuse current password
? Cannot reuse previous password (last 2)
? Can reuse password from 3+ changes ago
? Error message: "Cannot reuse last 2 passwords."

---

## Troubleshooting

### 2FA Not Working?
1. Check if `TwoFactorEnabled` is set in database:
   ```sql
   SELECT Email, TwoFactorEnabled FROM AspNetUsers;
   ```
2. Check Audit Logs for "2FA Enabled/Disabled" entries

### Multiple Login Not Detecting?
1. Check `UserSessions` table:
   ```sql
   SELECT * FROM UserSessions WHERE IsActive = 1;
   ```
2. Make sure you're using **different browsers** (not tabs in same browser)
3. Check if middleware is registered in `Program.cs`

### Password History Not Working?
1. Check `PasswordHistories` table:
   ```sql
   SELECT UserId, CreatedDate FROM PasswordHistories ORDER BY CreatedDate DESC;
   ```
2. Verify migration was applied: `Update-Database`

---

## Production Deployment Notes

### For 2FA:
Replace demo alert with actual SMS/Email service:
```csharp
// In AccountController.cs, replace:
TempData["2FAMessage"] = $"Verification code sent! (Demo: {code})";

// With:
await _emailService.SendAsync(user.Email, "Your 2FA Code", $"Your code is: {code}");
// Or SMS:
await _smsService.SendAsync(user.PhoneNumber, $"Your verification code is: {code}");
```

### For Multiple Logins:
- Current implementation forces logout from ALL other devices
- Alternative: Allow multiple active sessions and just log them
- Adjust `SessionValidationMiddleware` as needed

---

## Summary Checklist

- [ ] Multiple Login Detection tested in 2+ browsers
- [ ] Session expired message displays correctly
- [ ] Audit log shows "Previous session terminated"
- [ ] 2FA toggle works on Home page
- [ ] 2FA code displayed in alert
- [ ] Valid 2FA code allows login
- [ ] Invalid 2FA code shows error
- [ ] Expired code (5+ min) shows error
- [ ] Cannot reuse last 2 passwords
- [ ] Can reuse password from 3+ changes ago
- [ ] All features logged in Audit Logs

---

**All features are now fully implemented and ready for testing!** ??
