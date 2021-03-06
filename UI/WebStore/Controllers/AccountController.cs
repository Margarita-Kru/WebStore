using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(
            UserManager<User> UserManager,
            SignInManager<User> SignInManager,
            ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }


        #region Register
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            using (_Logger.BeginScope("Регистрация пользователя {0}", Model.UserName))
            {
                var user = new User
                {
                    UserName = Model.UserName

                };

                _Logger.LogInformation("Регистрация пользователя {0}", user.UserName);

                var register_result = await _UserManager.CreateAsync(user, Model.Password);
                if (register_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                    await _UserManager.AddToRoleAsync(user, Role.Users);

                    _Logger.LogInformation("Пользователю {0} назначена роль {1}", user.UserName, Role.Users);

                    await _SignInManager.SignInAsync(user, false);

                    _Logger.LogInformation("Пользователm {0} вошел в систему после регистрации", user.UserName);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in register_result.Errors)
                    ModelState.AddModelError("", error.Description);

                _Logger.LogWarning("Ошибка при регистрации пользователя {0}: {1}", user.UserName,
                    string.Join(",", register_result.Errors.Select(err => err.Description)));

            }
            return View(Model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false);
            if (login_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно вошел в систему", Model.UserName);
                return LocalRedirect(Model.ReturnUrl ?? "/");
            }
            ModelState.AddModelError("", "Ошибка ввода имени пользователя или пароля");

            _Logger.LogWarning("Ошибка при входе в систему пользователя {0}", Model.UserName);

            return View(Model);
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity!.Name;
            await _SignInManager.SignOutAsync();

            _Logger.LogInformation("Пользователь {0} успешно вышел из системы", user_name);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            _Logger.LogWarning("Отказано в доступе {0} к url:{1}", User.Identity!.Name, HttpContext.Request.Path);

            return View();
        }
    }
}
