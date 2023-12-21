using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest;
using System.Diagnostics;
using System.Reflection;
using WebPowerApp.Context;
using WebPowerApp.Interfaces;
using WebPowerApp.PowerBIModels;
using WebPowerApp.Services;
using WebPowerApp.ViewModel;

namespace WebPowerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogin _loginUser;
        private readonly ApplicationDBContext _dbContext;
        private string m_errorMessage;
        IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDBContext dbContext, ILogin loguser)
        {
            _logger = logger;
            m_errorMessage = ConfigValidatorService.GetWebConfigErrors();
            _configuration = configuration;
            _dbContext = dbContext;
            _loginUser = loguser;
        }


        public IActionResult Index(string returnUrl = "")
        {
            if (HttpContext.Request.Cookies["LOGGED_USERNAME_COOKIE"] != null)
                return RedirectToAction("Index", "Logged");

            if (HttpContext.Session.GetString("logged_username_session") != null)
                return RedirectToAction("Index", "Logged");
            else
            {
                var model = new LoginViewModel { ReturnUrl = returnUrl };
                return View(model);
            }

        }

        [HttpPost]
        public IActionResult Index(string username, string passcode, string returnUrl = null)
        { 
            var issuccess = _loginUser.AuthenticateUser(username, passcode);

            if (issuccess.Result != null)
            {
                ViewBag.username = string.Format("Successfully logged-in", username);
                TempData["username"] = username;
                HttpContext.Session.SetString("logged_username_session", username);
                HttpContext.Response.Cookies.Append("LOGGED_USERNAME_COOKIE", username);

                var getLoggedUserRole = (from u in _dbContext.Employees
                                         join e in _dbContext.Roles on u.RoleId equals e.Id
                                         where u.IsActive == true && u.Username == username
                                         select e.RoleName).FirstOrDefault();


                



                if (getLoggedUserRole != null)
                {
                    HttpContext.Session.SetString("logged_user_role_session", getLoggedUserRole);
                    HttpContext.Response.Cookies.Append("LOGGED_USER_ROLE_COOKIE", getLoggedUserRole);
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    if (getLoggedUserRole == "Admin")
                    {
                        return RedirectToAction("Index", "Logged");

                    }
                    else
                    {
                        return RedirectToAction("Index", "IndividualReports");

                    }
                }

            }
            else
            {
                ViewBag.username = string.Format("Login Failed ", username);
                return View();
            }
        }



        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("LOGGED_USERNAME_COOKIE");
            Response.Cookies.Delete("LOGGED_USER_ROLE_COOKIE");
            return RedirectToAction("Index", "Home");
        }


        //public ActionResult Index()
        //{
        //    // Assembly info is not needed in production apps and the following 6 lines can be removed

        //    var result = new IndexConfig();
        //    var assembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.Equals("Microsoft.PowerBI.Api")).FirstOrDefault();
        //    if (assembly != null)
        //    {
        //        result.DotNetSDK = assembly.Version.ToString(3);
        //    }
        //    return View(result);
        //}

        //public async Task<ActionResult> EmbedReport()
        //{
        //    if (m_errorMessage=="")
        //    {
        //        return View("Error", BuildErrorModel(m_errorMessage));
        //    }

        //    try
        //    {
        //        var embedResult = await EmbedService.GetEmbedParams(ConfigValidatorService.WorkspaceId, ConfigValidatorService.ReportId);
        //        return View(embedResult);
        //    }
        //    catch (HttpOperationException exc)
        //    {
        //        m_errorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
        //        return View("Error", m_errorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", ex.Message);
        //    }
        //}

        //public async Task<ActionResult> EmbedDashboard()
        //{
        //    if (m_errorMessage == "")
        //    {
        //        return View("Error", BuildErrorModel(m_errorMessage));
        //    }

        //    try
        //    {
        //        var embedResult = await EmbedService.EmbedDashboard(new Guid(_configuration.GetValue<String>("PowerBI:workspaceId")));
        //        return View(embedResult);
        //    }
        //    catch (HttpOperationException exc)
        //    {
        //        m_errorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
        //        return View("Error", m_errorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", ex.Message);
        //    }
        //}

        //public async Task<ActionResult> EmbedTile()
        //{
        //    if (m_errorMessage == "")
        //    {
        //        return View("Error", BuildErrorModel(m_errorMessage));
        //    }

        //    try
        //    {
        //        var embedResult = await EmbedService.EmbedTile(new Guid(_configuration.GetValue<String>("PowerBI:workspaceId")));
        //        return View(embedResult);
        //    }
        //    catch (HttpOperationException exc)
        //    {
        //        m_errorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
        //        return View("Error", m_errorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", ex.Message);
        //    }
        //}


        //private ErrorModel BuildErrorModel(string errorMessage)
        //{
        //    return new ErrorModel
        //    {
        //        ErrorMessage = errorMessage
        //    };
        //}


        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}