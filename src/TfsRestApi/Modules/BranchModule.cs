namespace TfsRestApi.Modules
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Nancy;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using TfsRestApi.Services;
    using System.Collections;
    using TfsRestApi.Services.Extensions;

    public class BranchModule : NancyModule
    {
        public BranchModule(IBranchService branchService)
        {
            Get["/branches"] = parameters =>
            {
                var source = Request.Query.source;
                var branches = branchService.Get(source);

                return Response.AsJson((IEnumerable<string>)branches);
            };
        }
    }
}