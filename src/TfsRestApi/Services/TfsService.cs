namespace TfsRestApi.Services
{
    using TfsRestApi.Services;
    using Microsoft.TeamFoundation.Client;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public interface ITfsService
    {
        TfsTeamProjectCollection Connection();
        //Task<TfsTeamProjectCollection> ConnectionAsync();
        VersionControlServer VCS { get; }
    }

    public class TfsService : ITfsService
    {
        protected ISettings _settings;

        public TfsService(ISettings settings)
        {
            _settings = settings;
        }

        public TfsTeamProjectCollection Connection()
        {
            string tfsUrl = _settings.TfsUrl;
            ICredentials credentials = (_settings.TfsUseDomainCredentials)
                ? System.Net.CredentialCache.DefaultCredentials
                : new NetworkCredential(_settings.TfsUsername, _settings.TfsPassword, _settings.TfsDomain);
            var server = new TfsTeamProjectCollection(new System.Uri(tfsUrl), credentials);
            server.Authenticate();
            return server;
        }

        public VersionControlServer VCS
        {
            get
            {
                var tfs = Connection();
                var vcs = tfs.GetService<VersionControlServer>();
                return vcs;
            }
        }
    }
}