using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;
using WebPowerApp.Context;
using WebPowerApp.Interfaces;
using WebPowerApp.Services;

namespace WebPowerApp.Controllers
{
    public class LoggedController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public LoggedController( ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            string loggedUser = string.Empty;
            if (HttpContext.Session.GetString("logged_username_session") != null)
                loggedUser = HttpContext.Session.GetString("logged_username_session");
            else
                loggedUser = HttpContext.Request.Cookies["LOGGED_USERNAME_COOKIE"];

            ViewBag.loggedUser = loggedUser;

            var identity = new GenericIdentity(loggedUser);
            var principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
            //https://dotnettutorials.net/lesson/authentication-and-authorization-in-web-api/


            string loggedUserRole = string.Empty;
            if (HttpContext.Session.GetString("logged_user_role_session") != null)
                loggedUserRole = HttpContext.Session.GetString("logged_user_role_session");
            else
                loggedUserRole = HttpContext.Request.Cookies["LOGGED_USER_ROLE_COOKIE"];

            ViewBag.loggedUserRole = loggedUserRole;

            var getLoggedUserName = (from u in _dbContext.Employees
                                     where u.IsActive == true && u.Username == loggedUser
                                     select u.FullName).FirstOrDefault();
            ViewBag.getLoggedUserName = getLoggedUserName;



            return View();
        }
    }
}
