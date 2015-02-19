namespace Janison.TfsApi
{
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;
    using TfsRestApi.Services;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register<ITfsService, TfsService>().AsSingleton();
        }
    }
}