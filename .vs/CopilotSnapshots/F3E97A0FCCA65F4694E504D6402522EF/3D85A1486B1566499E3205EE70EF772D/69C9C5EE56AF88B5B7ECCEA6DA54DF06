using AppSecAssignment.Models;
using AppSecAssignment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AppSecAssignment.Services; // For Protect.cs and DigitalSignature.cs
using System.Web; // For basic sanitization
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AppSecAssignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly RecaptchaService _recaptchaService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            AuthDbContext context,
            IWebHostEnvironment environment,
            RecaptchaService recaptchaService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _environment = environment;
            _recaptchaService = recaptchaService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents CSRF Attacks
        public async Task<IActionResult> Register(RegisterViewModel model, string recaptchaToken)
        {
            // Verify reCAPTCHA (skip if token is null/empty for development)
            if (!string.IsNullOrEmpty(recaptchaToken))
            {
                if (!await _recaptchaService.VerifyToken(recaptchaToken))
                {
                    ModelState.AddModelError("", "reCAPTCHA verification failed. Please try again.");
                    return View(model);
                }
            }

            if (ModelState.IsValid)
            {
                // 1. Check for Duplicate Email
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email address is already in use.");
                    return View(model);
                }

                // 2. Handle Photo Upload
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    // SECURITY: Server-side validation for .jpg only
                    var extension = Path.GetExtension(model.Photo.FileName).ToLowerInvariant();
                    if (extension != ".jpg")
                    {
                        ModelState.AddModelError("Photo", "Only .jpg files are allowed.");
                        return View(model);
                    }

                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    // Create folder if it doesn't exist
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Use GUID to prevent filename conflicts
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Photo.CopyToAsync(fileStream);
                    }
                }

                // 3. Create the User Object
                // Prepare the About Me text (Sanitize it first so we sign the sanitized version)
                string safeAboutMe = System.Net.WebUtility.HtmlEncode(model.AboutMe);

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = System.Net.WebUtility.HtmlEncode(model.FullName), // Basic XSS sanitization
                    Gender = model.Gender,
                    MobileNo = System.Net.WebUtility.HtmlEncode(model.MobileNo),
                    DeliveryAddress = System.Net.WebUtility.HtmlEncode(model.DeliveryAddress),

                    // SECURITY: Encrypt the Credit Card (AES)
                    CreditCardNo = Protect.Encrypt(model.CreditCardNo),

                    PhotoPath = uniqueFileName,

                    // Store the sanitized text
                    AboutMe = safeAboutMe,

                    // SECURITY: Digital Signature (RSA)
                    // We sign the 'safeAboutMe' because that is what we are saving to the DB.
                    AboutMeSignature = DigitalSignature.SignData(safeAboutMe)
                };

                // 4. Save to AspNetUsers via Identity
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // 5. Create Session (Auto-login after registration)
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("AuthToken", Guid.NewGuid().ToString());

                // 6. Redirect to Home
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string recaptchaToken)
        {
            // Verify reCAPTCHA (skip if token is null/empty for development)
            if (!string.IsNullOrEmpty(recaptchaToken))
            {
                if (!await _recaptchaService.VerifyToken(recaptchaToken))
                {
                    ModelState.AddModelError("", "reCAPTCHA verification failed. Please try again.");
                    return View(model);
                }
            }

            if (ModelState.IsValid)
            {
                // 1. Check if user exists
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // SECURITY: Do not reveal that the user does not exist. Use a generic message.
                    ModelState.AddModelError("", "Invalid Login Attempt");
                    return View(model);
                }

                // 2. Check for Account Lockout
                if (await _userManager.IsLockedOutAsync(user))
                {
                    ModelState.AddModelError("", "Account locked out. Try again later.");
                    return View(model);
                }

                // 3. Verify Password (Identity)
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // SUCCESSFUL LOGIN

                    // Reset failure count
                    await _userManager.ResetAccessFailedCountAsync(user);

                    // Create Session
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("AuthToken", Guid.NewGuid().ToString());

                    // AUDIT LOG: Record the login
                    var audit = new AuditLog
                    {
                        UserId = user.Email,
                        Action = "Login Success",
                        Timestamp = DateTime.Now
                    };
                    _context.AuditLogs.Add(audit);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // FAILED LOGIN

                    // Increment failure count and apply lockout if needed
                    await _userManager.AccessFailedAsync(user);

                    // AUDIT LOG: Record failed login attempt
                    var failedAudit = new AuditLog
                    {
                        UserId = user.Email,
                        Action = "Login Failed",
                        Timestamp = DateTime.Now
                    };
                    _context.AuditLogs.Add(failedAudit);

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        // AUDIT LOG: Record the lockout
                        var audit = new AuditLog
                        {
                            UserId = user.Email,
                            Action = "Account Locked",
                            Timestamp = DateTime.Now
                        };
                        _context.AuditLogs.Add(audit);
                    }

                    await _context.SaveChangesAsync();

                    ModelState.AddModelError("", "Invalid Login Attempt");
                }
            }
            return View(model);
        }

        // Helper for Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}