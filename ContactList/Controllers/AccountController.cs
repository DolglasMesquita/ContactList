using ContactList.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM); 

            var usuario = await _userManager.FindByNameAsync(loginVM.UserName);

            if (usuario != null)
            {
                var resultado = await _signInManager.PasswordSignInAsync(usuario, loginVM.Password, false, false);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Usuário ou senha inválidos ou não encontrados");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var usuario = new IdentityUser() { UserName = registroVM.UserName };
                var resultado = await _userManager.CreateAsync(usuario, registroVM.Password);
                TempData["alerta"] = "Cadastro realizado com sucesso";

                if (resultado.Succeeded) return RedirectToAction("Index", "Home");
                else ModelState.AddModelError("", "Usuário já cadastrado");
            }

            return View(registroVM);
            
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
