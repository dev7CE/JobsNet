using System.Threading.Tasks;
using Solution.FrontEnd.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solution.FrontEnd.Controllers;
using Solution.FrontEnd.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Solution.FrontEnd.Models;

namespace Solution.FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        
        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILoggerFactory loggerFactory, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _roleManager = roleManager;
        }
        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            CargarRoles();
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                // If we got this far, something failed, redisplay form
                CargarRoles();
                return View(model);
            }
            
            var user = new IdentityUser 
            { 
                UserName = model.Email, 
                Email = model.Email, 
                EmailConfirmed = true 
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
            {
                AddErrors(result);
                CargarRoles();
                return View(model);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, model.Role);  
            
            if(!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                AddErrors(result);
                CargarRoles();
                return View(model);
            }
            
            
            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation(3, "User created a new account with password.");
            return RedirectToLocal(returnUrl);
        }
        
        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        private void CargarRoles()
        {
            ViewBag.Name = new SelectList(_roleManager.Roles.Where(
                r => !r.NormalizedName.Contains(RoleNames.ROLE_ADMINISTRATOR.ToUpper())).ToList()
                , "Name", "Name");
        }
        #endregion
    }
}
