// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// ----------------------------------------------------------------------------

namespace WebPowerApp.Services
{
    using System;
    using System.Configuration;

    public class ConfigValidatorService
    {



        public static readonly string ApplicationId = "1990ac0f-8176-4a19-814b-a54c56874323";
        //public static readonly Guid WorkspaceId = new Guid("ea002786-7b48-4d83-99b4-a88653dfab4b");

        //public static readonly Guid WorkspaceId;
        //public static readonly Guid ReportId;


        public static readonly Guid WorkspaceId = new Guid("0daef677-4925-4410-9b62-c829fb695843");
        public static readonly Guid ReportId = new Guid("ec6f7d8b-e6c0-41cf-8b45-5593ed921d08");//new Guid("564ea6a0-93f8-4ac9-9234-5d6b25688c5d");
        // public static readonly Guid ReportId = new Guid("7bf08a6e-c062-417a-9826-e7f6d4f8a707");/*d6b1efe7-04a0-4634-9028-3a8534185910     7bf08a6e-c062-417a-9826-e7f6d4f8a707*/
        public static readonly string AuthenticationType = "ServicePrincipal";
        public static readonly string ApplicationSecret = "A0Y8Q~UG50ooZUBgISz72x6xEi-AXf7alYCRTdaA";
        public static readonly string Tenant = "5008541a-b3bf-411f-8de0-3fcd20d276b9";
        public static readonly string Username = "";
        public static readonly string Password = "";


        //public static readonly string ApplicationId = "1bccd527-ea8a-4423-97ed-6ffb53e3f830";
        //public static readonly Guid WorkspaceId = new Guid("627d8449-425b-4849-8b8e-3dc2e2a953b6");
        //public static readonly Guid ReportId = new Guid("56e5eda8-03df-46ad-ad85-f45245f155b1");
        //public static readonly string AuthenticationType = "ServicePrincipal";
        //public static readonly string ApplicationSecret = "_ZX8Q~voe7mIw9yG2PtTRI~2hmyGNwCeKzDqjcx1";
        //public static readonly string Tenant = "d97c2e05-e56a-49e1-9f0a-5c35a4b68db8";
        //public static readonly string Username = "";
        //public static readonly string Password = "";


        //public static readonly Guid WorkspaceId = GetParamGuid(ConfigurationManager.AppSettings["workspaceId"]);
        //public static readonly Guid ReportId = GetParamGuid(ConfigurationManager.AppSettings["reportId"]);
        //public static readonly string AuthenticationType = ConfigurationManager.AppSettings["authenticationType"];
        //public static readonly string ApplicationSecret = ConfigurationManager.AppSettings["applicationSecret"];
        //public static readonly string Tenant = ConfigurationManager.AppSettings["tenant"];
        //public static readonly string Username = ConfigurationManager.AppSettings["pbiUsername"];
        //public static readonly string Password = ConfigurationManager.AppSettings["pbiPassword"];

        /// <summary>
        /// Check if web.config embed parameters have valid values.
        /// </summary>
        /// <returns>Null if web.config parameters are valid, otherwise returns specific error string.</returns>
        public static string GetWebConfigErrors()
        {
            string message = null;
            Guid result;

            // Application Id must have a value.
            if (string.IsNullOrWhiteSpace(ApplicationId))
            {
                message = "ApplicationId is empty. please register your application as Native app in https://dev.powerbi.com/apps and fill client Id in web.config.";
            }
            // Application Id must be a Guid object.
            else if (!Guid.TryParse(ApplicationId, out result))
            {
                message = "ApplicationId must be a Guid object. please register your application as Native app in https://dev.powerbi.com/apps and fill application Id in web.config.";
            }
            // Workspace Id must have a value.
            else if (WorkspaceId == Guid.Empty)
            {
                message = "WorkspaceId is empty or not a valid Guid. Please fill its Id correctly in web.config";
            }
            // Report Id must have a value.
            else if (ReportId == Guid.Empty)
            {
                message = "ReportId is empty or not a valid Guid. Please fill its Id correctly in web.config";
            }
            else if (AuthenticationType.Equals("masteruser", StringComparison.InvariantCultureIgnoreCase))
            {
                // Username must have a value.
                if (string.IsNullOrWhiteSpace(Username))
                {
                    message = "Username is empty. Please fill Power BI username in web.config";
                }

                // Password must have a value.
                if (string.IsNullOrWhiteSpace(Password))
                {
                    message = "Password is empty. Please fill password of Power BI username in web.config";
                }
            }
            else if (AuthenticationType.Equals("serviceprincipal", StringComparison.InvariantCultureIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(ApplicationSecret))
                {
                    message = "ApplicationSecret is empty. please register your application as Web app and fill appSecret in web.config.";
                }
                // Must fill tenant Id
                else if (string.IsNullOrWhiteSpace(Tenant))
                {
                    message = "Invalid Tenant. Please fill Tenant ID in Tenant under web.config";
                }
            }
            else
            {
                message = "Invalid authentication type";
            }

            return message;
        }

        private static Guid GetParamGuid(string param)
        {
            Guid paramGuid = Guid.Empty;
            Guid.TryParse(param, out paramGuid);
            return paramGuid;
        }
    }
}
