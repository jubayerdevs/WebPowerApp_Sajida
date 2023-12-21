using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Rest;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using WebPowerApp.Context;
using WebPowerApp.Models;
using WebPowerApp.PowerBIModels;
using WebPowerApp.Services;

namespace WebPowerApp.Controllers
{
    public class IndividualReportsController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private string m_errorMessage;
        IConfiguration _configuration;

        public IndividualReportsController(ApplicationDBContext dbContext, IConfiguration configuration)
        {
            m_errorMessage = ConfigValidatorService.GetWebConfigErrors();
            _configuration = configuration;
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


            var result = new IndexConfig();
            var assembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.Equals("Microsoft.PowerBI.Api")).FirstOrDefault();
            if (assembly != null)
            {
                result.DotNetSDK = assembly.Version.ToString(3);
            }
            return View(result);
        }



        public async Task<ActionResult> EmbedReport(int id)
        {

            //var getReport = _dbContext.Reports.Where(p => p.Id == id)
            //                .Select(p => p.ReportId).FirstOrDefaultAsync();

            var table = _dbContext.Reports.Where(mt => mt.Id == id)
                               .Select(mt => new ReportModel
                               {
                                   AuthenticationType = mt.AuthenticationType,
                                   ApplicationId = mt.ApplicationId,
                                   WorkspaceId = mt.WorkspaceId,
                                   ReportId = mt.ReportId,
                                   Scope = mt.Scope,
                                   UrlPowerBiServiceApiRoot = mt.UrlPowerBiServiceApiRoot,
                                   ApplicationSecret = mt.ApplicationSecret,
                                   Tenant = mt.Tenant,
                                   IsActive = mt.IsActive
                               })
                               .FirstOrDefault();

            Guid WorkspaceId = (Guid)table.WorkspaceId;
            Guid ReportId = (Guid)table.ReportId;
            string ApplicationSecret = table.ApplicationSecret;
            string ApplicationId = table.ApplicationId;






            if (m_errorMessage == "")
            {
                return View("Error", BuildErrorModel(m_errorMessage));
            }

            try
            {
                //Guid workspaceId, Guid reportId, string applicationId, string applicationSecret
                //var embedResult = await EmbedService.GetEmbedParams(new Guid("0daef677-4925-4410-9b62-c829fb695843"), new Guid("ec6f7d8b-e6c0-41cf-8b45-5593ed921d08"), "1990ac0f-8176-4a19-814b-a54c56874323", "A0Y8Q~UG50ooZUBgISz72x6xEi-AXf7alYCRTdaA");

                var embedResult = await EmbedService.GetEmbedParams(WorkspaceId, ReportId, ApplicationId, ApplicationSecret);
                // var embedResult = await EmbedService.GetEmbedParams(new Guid("ea002786-7b48-4d83-99b4-a88653dfab4b"), new Guid("d6b1efe7-04a0-4634-9028-3a8534185910"));
                return View(embedResult);
            }
            catch (HttpOperationException exc)
            {
                m_errorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
                return View("Error", m_errorMessage);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<ActionResult> EmbedDashboard(Guid workspaceId)
        {

            //Guid workspaceId = new Guid(workspaceId);
            if (m_errorMessage == "")
            {
                return View("Error", BuildErrorModel(m_errorMessage));
            }

            try
            {
                var embedResult = await EmbedService.EmbedDashboard(workspaceId); // _configuration.GetValue<String>("PowerBI:workspaceId")
                return View(embedResult);
            }
            catch (HttpOperationException exc)
            {
                m_errorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
                return View("Error", m_errorMessage);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<ActionResult> EmbedTile()
        {
            if (m_errorMessage == "")
            {
                return View("Error", BuildErrorModel(m_errorMessage));
            }

            try
            {
                var embedResult = await EmbedService.EmbedTile(new Guid("0daef677-4925-4410-9b62-c829fb695843")); //new Guid(_configuration.GetValue<String>("PowerBI:workspaceId")
                return View(embedResult);
            }
            catch (HttpOperationException exc)
            {
                m_errorMessage = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
                return View("Error", m_errorMessage);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }


        private ErrorModel BuildErrorModel(string errorMessage)
        {
            return new ErrorModel
            {
                ErrorMessage = errorMessage
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
