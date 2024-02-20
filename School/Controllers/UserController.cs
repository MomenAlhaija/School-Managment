using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School.BL.DTO;
using School.BL.Service;
using System.Runtime.Intrinsics;

namespace School.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        public UserController(IUserService userService,ILoginService loginService)
        {
             _userService=userService;
            _loginService=loginService;
        }
        public async Task<IActionResult> Index()
        {
            var users=await _userService.GetUsers();
            return View(users);
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> AddUser(UserDTO user)
        {
            try
            {
                await _userService.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(user);
            }
        }
        public IActionResult EditUser(int id)
        {
            var user=_userService.GetUserById(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UserDTO user)
        {
            try
            {
                await _userService.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(user);
            }
        }
        public async Task Delete(int id)
        {
           await _userService.DeleteUser(id);
        }
        public IActionResult ResetPassword(int userId,string? returnPath)
        {
            if (returnPath.IsNullOrEmpty())
                returnPath = Url.Action("Index", "User");
            ViewBag.ReturnUrl= returnPath;
            var user = _userService.GetUserById(userId);
            var model = new ResetPasswordDTO();
            model.UserId=userId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO password)
        {

            bool isSucess= await _userService.ResetPassword(password);
            ViewBag.Message = isSucess ? "The Password Reset Correctly" : "Failed Reset Password";
            return View(password);
        }
        public async Task<IActionResult> SelfResetPassword()
        {
            var passwordDto=new ResetPasswordDTO();
            passwordDto.UserId =(int)_loginService.GetUserIdFromSession()!.Value;
            passwordDto.ReturnUrl= ViewBag.ReturnUrl = Url.Action("SelfResetPassword", "User");
            return View(passwordDto);
        }
        [HttpPost]
        public async Task<IActionResult> SelfResetPassword(ResetPasswordDTO passwordDTO)
        {
          bool isSucess= await _userService.ResetPassword(passwordDTO);
          ViewBag.Message= isSucess ? "The Password Reset Correctly" : "Failed Reset Password";
          return View(passwordDTO);
        }
    }
}
