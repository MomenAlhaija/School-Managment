using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using School.BL.DTO;
using School.BL.Enum;
using School.BL.Service;
using System.Reflection.Metadata.Ecma335;

namespace School.Controllers
{
    public class LogInController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        public LogInController(IUserService userService,ILoginService loginService)
        {
           _userService = userService;
           _loginService = loginService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login) 
        {
            try
            {
                if (await _loginService.AuthenticateUser(login.Email, login.Password))
                {
                    int? usertype = (int?)(_loginService.GetUserTypeFromSession());
                    if (usertype.HasValue)
                        switch ((UserType)(usertype))
                        {
                            case UserType.Admin:
                                return RedirectToAction("Index", "Material");
                            case UserType.Teacher:
                                return RedirectToAction("Index", "TeacherClass");
                            case UserType.Student:
                                return RedirectToAction("Index", "MarksStudent");
                        }
                }
                ViewBag.ErrorMessage = "The Email or Password Incorrect";
                return View(login);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(login);
            }
        }
        public async Task<IActionResult> Logout()
        {
            _loginService.Logout();
            return RedirectToAction("Index", "Home");
        }  
    }
}
