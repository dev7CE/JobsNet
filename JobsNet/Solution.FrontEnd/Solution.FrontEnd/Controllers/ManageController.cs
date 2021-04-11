using Microsoft.AspNetCore.Mvc;
using Solution.FrontEnd.Models.ManageViewModels;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Solution.FrontEnd.Models;

namespace Solution.FrontEnd.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger _logger;
        public ManageController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<ManageController>();
        }
        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    string redirectToController = (User.IsInRole(RoleNames.ROLE_EMPLEADOR)) ? "Empresas" : string.Empty;
                    redirectToController = (User.IsInRole(RoleNames.ROLE_OFERENTE)) ? "Oferentes" : redirectToController;
                    return RedirectToAction(nameof(Index), redirectToController, new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }
        #region Helpers
        private Task<IdentityUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }
        #endregion
    }
}
