using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlickrClone.Models;
using Microsoft.AspNetCore.Identity;
using FlickrClone.ViewModels;

namespace FlickrClone.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }


        //routing to account index.
        public IActionResult Index()
        {
            return View();
        }

        //if register was successful it will rout to index if not will stay in on same page.

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        //if signin was successful it will rout to index if not will stay in on same page.

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //give the user the ability to log out. Add a LogOff
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

//The controller holds private references to ApplicationDbContext, UserManager, and SignInManager, and then uses dependency injection in the constructor to configure these services for us.


//We're using SignInManager's asynchronous PasswordSignInAsync method to sign a user in with their credentials. The method takes four parameters: userName, password, isPersistent, lockoutOnFailure. For now we're only handling username and password, so we set default values for isPersistent and lockoutOnFailure.