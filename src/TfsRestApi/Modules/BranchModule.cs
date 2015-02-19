namespace TfsRestApi.Modules
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Nancy;
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TfsRestApi.Services;
    using TfsRestApi.Services.Extensions;

    public class BranchModule : NancyModule
    {
        public BranchModule(IBranchService branchService)
        {
            Get["/branches", runAsync: true] = async (parameter, ct) =>
            {
                dynamic source = Request.Query.source;

                var branches = await Task.Run(() => branchService.Get(source));

                return Response.AsJson((IEnumerable<string>)branches);
            };
        }
    }
}