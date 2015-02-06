namespace TfsRestApi.Services
{
    using TfsRestApi.Services;
    using Microsoft.TeamFoundation.Client;
    using System.Net;
    using System.Threading.Tasks;

    public interface ITfsService
    {
        TfsTeamProjectCollection Connection();
        //Task<TfsTeamProjectCollection> ConnectionAsync();
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
    }
}