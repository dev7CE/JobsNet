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
using Solution.FrontEnd.DAL;

namespace Solution.FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private UsuariosRepository _repositoryUsuarios;
        private EmpresasRepository _repositoryEmpresas;
        private OferentesRepository _repositoryOferentes;

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
            _repositoryUsuarios = new UsuariosRepository();
            _repositoryEmpresas = new EmpresasRepository();
            _repositoryOferentes = new OferentesRepository();
        }
        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            model.RememberMe = true;
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            return View(model);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password, 
                model.RememberMe, 
                lockoutOnFailure: false
            );
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(await _userManager.IsInRoleAsync(user, RoleNames.ROLE_EMPLEADOR))
                return RedirectToAction("Index", "PuestosTrabajo",null);
                
                _logger.LogInformation(1, "Usuario inició sesión.");
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Intento inválido de inicio de sesión.");
                return View(model);
            }
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
            
            if(!(await DALRegister(model, user)))
            {
                await _userManager.DeleteAsync(user);
                AddErrors(result);
                CargarRoles();
                return View(model);
            }
            returnUrl = (model.Role.Equals(RoleNames.ROLE_EMPLEADOR.ToString())) ? "PuestosTrabajo/Index" : string.Empty;
            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation(3, "User created a new account with password.");
            return RedirectToLocal(returnUrl);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(PuestosTrabajoController.All), "PuestosTrabajo");
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
            returnUrl = (string.IsNullOrEmpty(returnUrl)) ? string.Empty : returnUrl;
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else if (returnUrl.Equals("PuestosTrabajo/Index"))
            {
                return RedirectToAction(nameof(PuestosTrabajoController.Index), "PuestosTrabajo");
            }
            else
            {
                return RedirectToAction(nameof(PuestosTrabajoController.All), "PuestosTrabajo");
            }
        }
        private void CargarRoles()
        {
            ViewBag.Name = new SelectList(_roleManager.Roles.Where(
                r => !r.NormalizedName.Contains(RoleNames.ROLE_ADMINISTRATOR.ToUpper())).ToList()
                , "Name", "Name");
        }
        private async Task<bool> DALRegister (RegisterViewModel model, IdentityUser user)
        {
            bool DALSuccessedResult = false;

            DALSuccessedResult = await _repositoryUsuarios
                .CreateUsuario( new Usuarios { UserName = model.Email });
            
            if (!DALSuccessedResult)
            return DALSuccessedResult;

            DALSuccessedResult = false;

            if(RoleNames.ROLE_EMPLEADOR.Equals(model.Role))
            DALSuccessedResult = await _repositoryEmpresas.CreateEmpresa( new Empresas 
            { 
                NombreEmpresa = model.Email, 
                UserName = model.Email 
            });

            if(RoleNames.ROLE_OFERENTE.Equals(model.Role))
            DALSuccessedResult = await _repositoryOferentes.CreateOferente( new Oferentes 
            { 
                Nombre = model.Email, 
                UserName = model.Email, 
                UrlFoto = "facedefault.png",
                UrlCurriculo = "urlCV"
            });

            if (!DALSuccessedResult)
            await _repositoryUsuarios.DeleteUsuario(model.Email);
            
            return DALSuccessedResult;
        }
        #endregion
    }
}
