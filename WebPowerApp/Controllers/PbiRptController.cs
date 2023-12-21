using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;
using System;
using System.Diagnostics;
using System.Reflection;
using WebPowerApp.Context;
using WebPowerApp.Interfaces;
using WebPowerApp.PowerBIModels;
using WebPowerApp.Services;

namespace WebPowerApp.Controllers
{
    public class PbiRptController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private string m_errorMessage;
        IConfiguration _configuration;

        public PbiRptController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDBContext dbContext)
        {
            m_errorMessage = ConfigValidatorService.GetWebConfigErrors();
            _configuration = configuration;
            _dbContext = dbContext;
        }
        
        public ActionResult Index()
        {
            // Assembly info is not needed in production apps and the following 6 lines can be removed

            var result = new IndexConfig();
            var assembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(n => n.Name.Equals("Microsoft.PowerBI.Api")).FirstOrDefault();
            if (assembly != null)
            {
                result.DotNetSDK = assembly.Version.ToString(3);
            }
            return View(result);
        }

        public async Task<ActionResult> EmbedReport()
        {
            if (m_errorMessage == "")
            {
                return View("Error", BuildErrorModel(m_errorMessage));
            }

            try
            {
                var embedResult = await EmbedService.GetEmbedParams(ConfigValidatorService.WorkspaceId, ConfigValidatorService.ReportId, ConfigValidatorService.ApplicationId, ConfigValidatorService.ApplicationSecret);
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

        public async Task<ActionResult> EmbedDashboard()
        {
            if (m_errorMessage == "")
            {
                return View("Error", BuildErrorModel(m_errorMessage));
            }

            try
            {
                var embedResult = await EmbedService.EmbedDashboard(new Guid(_configuration.GetValue<String>("PowerBI:workspaceId")));
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
                var embedResult = await EmbedService.EmbedTile(new Guid(_configuration.GetValue<String>("PowerBI:workspaceId")));
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
