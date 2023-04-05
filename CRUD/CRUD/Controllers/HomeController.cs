using CRUD.Models;
using CRUD.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace CRUD.Controllers
{
    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly IRepository<tbl_user> Tbl_user;

        public HomeController(IRepository<tbl_user> Tbl_user)
        {
            this.Tbl_user = Tbl_user;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(tbl_user u)
        {
            if (u.user_email != null && u.user_password != null)
            {
                tbl_user? row = await Tbl_user.ChkCredentials(u.user_email, u.user_password ?? "");
                if (row == null)
                {
                    TempData["msg1"] = "User Not Found !!!";
                    return RedirectToAction("Login", "Home");
                } 
                else
                { 
                    List<Claim> claims = new List<Claim>()
                {
                   new Claim(ClaimTypes.NameIdentifier,row.user_name?? "ABC Name")
                   //,new Claim(ClaimTypes.Role,row.user_role?? "ABC Role")
                };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = false
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                     
                        return RedirectToAction("Index", "Home");
                    
                }

            }
            else
            {
                TempData["msg1"] = "User Not Found !!!";
                return RedirectToAction("Login", "Home");
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Remove("Role");
            //HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}