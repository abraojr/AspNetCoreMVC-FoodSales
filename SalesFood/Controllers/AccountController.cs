using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesFood.ViewModels;

namespace SalesFood.Controllers;

public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : Controller
{
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }

        var user = await userManager.FindByNameAsync(loginViewModel.UserName);

        if (user != null)
        {
            var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return Redirect(loginViewModel.ReturnUrl);
            }
        }

        ModelState.AddModelError("", "Login failed");
        return View(loginViewModel);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(LoginViewModel registerViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = registerViewModel.UserName,
            };

            var result = await userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Member");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("Register", "Register failed");
            }
        }

        return View(registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.User = null;

        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}