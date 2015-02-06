using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TfsRestApi.Services
{
    public interface ISettings
    {
        string TfsDomain { get; }
        string TfsPassword { get; }
        string TfsProjectPath { get; }
        string TfsUrl { get; }
        bool TfsUseDomainCredentials { get; }
        string TfsUsername { get; }
    }

    public class Settings : TfsRestApi.Services.ISettings
    {
        public string TfsUrl { get; protected set; }
        public string TfsUsername { get; protected set; }
        public string TfsPassword { get; protected set; }
        public string TfsDomain { get; protected set; }
        public bool TfsUseDomainCredentials { get; protected set; }
        public string TfsProjectPath { get; protected set; }

        public Settings()
        {
            TfsUsername = System.Configuration.ConfigurationManager.AppSettings["TfsUsername"];
            TfsPassword = System.Configuration.ConfigurationManager.AppSettings["TfsPassword"];
            TfsDomain = System.Configuration.ConfigurationManager.AppSettings["TfsDomain"];
            TfsUseDomainCredentials = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["TfsUseDomainCredentials"]);
            TfsUrl = System.Configuration.ConfigurationManager.AppSettings["TfsUrl"];
            TfsProjectPath = System.Configuration.ConfigurationManager.AppSettings["TfsProjectPath"];
        }
    }
}