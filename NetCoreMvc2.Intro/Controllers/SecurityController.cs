using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreMvc2.Intro.Identity;
using NetCoreMvc2.Intro.Models.Security;

namespace NetCoreMvc2.Intro.Controllers
{
    public class SecurityController : Controller
    {
        // User Manager ve SignInManager Identity yapılandırmasıyla db ile bağlantılı çalışıuyorlar (AddIdentity olarak Startup ta yapılandırıldı)
        private UserManager<AppIdentityUser> _userManager;
        private SignInManager<AppIdentityUser> _signInManager;
        public SecurityController(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Login için get metodu ==> view gösterilecek, sadece arayüz
        public IActionResult Login()
        {
            return View();
        }

        // Login post metodu ==> asenkron çalısıcak, parametre login view Model alıyor
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) // Gelen Model içinde belirlediğimiz zorunluluklar ModelState ile geliyor ve IsValid ile kontrol ediliyor (Required??)
            {
                // return View larda alınan view geri gönderiliyor ki o ana kadar girilen bilgiler kaybolmasın
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null) // Kullanıcı varsa...
            {
                if (!await _userManager.IsEmailConfirmedAsync(user)) // Kullanıcı Emailini confirm etmemişse...
                {
                    ModelState.AddModelError(String.Empty, "Confirm Your Email!");
                    return View(loginViewModel);
                }
            }

            // manager sign in işlemini gerçekleştiriyor
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Student"); // başarılıysa yönlendirme yap, toast message vs...
            }

            ModelState.AddModelError(String.Empty, "Login Failed!!!");
            // return View larda alınan view geri gönderiliyor ki o ana kadar girilen bilgiler kaybolmasın
            return View(loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Student");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) // Gelen Model içinde belirlediğimiz zorunluluklar ModelState ile geliyor ve IsValid ile kontrol ediliyor (Required??)
            {
                // return View larda alınan view geri gönderiliyor ki o ana kadar girilen bilgiler kaybolmasın
                return View(registerViewModel);
            }

            var user = new AppIdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                Age = registerViewModel.Age
            };

            // Kullanıcı yaratılıyor
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                // Emaile gönderilecek confirmation code generate edildi
                var confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Emaile gidecek olan URL oluşturuluyor. User id ve ConfCode gönderiliyor ve Action olarak ConfirmEmail fonksiyonu atandı.s
                var callBackUrl = Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationCode.Result });

                // Send URL via Email

                return RedirectToAction("Index", "Student"); // başarılıyla ilgili sayfaya yönlendir
            }

            // return View larda alınan view geri gönderiliyor ki o ana kadar girilen bilgiler kaybolmasın
            return View(registerViewModel);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Student"); // error sayfasına vs yönlendirme yapılır
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException("Can not find the user!");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return View("ConfirmEmail"); // başarılıyla ilgili sayfaya yönlendir
            }

            return RedirectToAction("Index", "Student"); // Hata alındıysa bir error sayfasına vs yönlendirme yapılır
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return View(); // emailin zorunluluğu kontrol edilmeli
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return View();
            }

            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl = Url.Action("ResetPassword", "Security", new { userId = user.Id, code = confirmationCode });

            // Send URL via Email

            return RedirectToAction("ForgotPasswordEmailSent");
        }

        public IActionResult ForgotPasswordEmailSent()
        {
            return View();
        }

        public IActionResult ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
            {
                throw new ApplicationException("User id and Code must be supplied for reset password"); // error sayfasına vs yönlendirme yapılır
            }

            var model = new ResetPasswordViewModel { Code = code };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid) // Gelen Model içinde belirlediğimiz zorunluluklar ModelState ile geliyor ve IsValid ile kontrol ediliyor (Required??)
            {
                // return View larda alınan view geri gönderiliyor ki o ana kadar girilen bilgiler kaybolmasın
                return View(resetPasswordViewModel);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            if (user == null)
            {
                throw new ApplicationException("Can not find the user!");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirm"); // başarılıyla ilgili sayfaya yönlendir
            }

            return View();
        }

        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }



    }
}