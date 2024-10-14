using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Social_Media_Platform.Models;
using Social_Media_Platform.Services;
using Social_Media_Platform.ViewModels;
using System.Security.Claims;

namespace Social_Media_Platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMailingService mailingService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            ,IMailingService mailingService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailingService = mailingService;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? ReturnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {


                var result = await signInManager.PasswordSignInAsync(model.Email, model.password,model.RememberMe,false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (!user.EmailConfirmed && await userManager.CheckPasswordAsync(user, model.password))
                    {
                        return RedirectToAction("EmailNotConfirmed", "Account");
                    }
                    else
                    {
                    return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            
            if(signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    City = model.City,
                    gender = (Gender)model.Gender,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = (DateTime)model.Birthday,
                };
                if(model.Photo != null)
                {
                    var uploadPath = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                    var fileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                    var filePath = Path.Combine(uploadPath, fileName);
                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                    
                    user.ImagePath = filePath;
                }
                var result = await userManager.CreateAsync(user, model.password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("EmailNotConfirmed", "Account");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> EmailNotConfirmed()
        {
            var user = await userManager.GetUserAsync(User);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token },Request.Scheme);
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplate.html";

            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("[URL]", confirmationLink);
            await mailingService.SendEmailAsync(user.Email, "Email Confirmation", mailText);
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string ReturnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = ReturnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            if(remoteError!= null)
            {
                ModelState.AddModelError("", $"Error from external provider: {remoteError}");
                return View("Login",model);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", $"Couldn't retrieve Login information");
                return View("Login", model);
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if(email == null)
                {
                    ModelState.AddModelError("", "Email info couldn't be retrieved");
                    return View("Login", model);
                }
                else
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if(user == null)
                    {
                        return RedirectToAction("CompleteProfile", "Account");
                    }
                    if (!user.EmailConfirmed) user.EmailConfirmed = true;
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: true);

                    return RedirectToAction("Index", "Home");
                    

                }
            }
            
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CompleteProfile() { 
        
            var info = await signInManager.GetExternalLoginInfoAsync();
            var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            RegisterViewModel model = new RegisterViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email =  email
            };
            return View("CompleteProfile",model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CompleteProfile(RegisterViewModel model)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                City = model.City,
                gender = (Gender)model.Gender,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birthday = (DateTime)model.Birthday,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user);

            await userManager.AddLoginAsync(user, info);
            await signInManager.SignInAsync(user, isPersistent: true);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("EmailConfirmed");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user !=null && user.EmailConfirmed)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var userId = user.Id;

                    var url = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token },Request.Scheme);
                    var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\PasswordResetTemplate.html";

                    var str = new StreamReader(filePath);

                    var mailText = str.ReadToEnd();
                    str.Close();

                    mailText = mailText.Replace("[URL]", url);
                    await mailingService.SendEmailAsync(user.Email, "Email Confirmation", mailText);
                    return View("ForgotPasswordConfirmation");

                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string Email,string Token)
        {
            if(Email == null || Token == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null) {
                    var result = await userManager.ResetPasswordAsync(user,model.Token,model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
                return View("ResetPasswordConfirmation");

            }
            return View(model);
        }
    }
}
