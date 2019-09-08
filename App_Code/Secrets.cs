using System;
using System.Collections.Generic;
using System.Configuration;

namespace UnlockedInsertService
{
    internal static class Secrets
    {
        public static string RbxAuthToken { get { return ConfigurationManager.AppSettings["RbxAuthToken"]; } }
        public static string ApiKey { get { return ConfigurationManager.AppSettings["ApiKey"]; } }
    }
}