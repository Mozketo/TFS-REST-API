namespace TfsRestApi.Modules
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Nancy;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TfsRestApi.Services;
    using TfsRestApi.Services.Extensions;

    public class MergeModule : NancyModule
    {
        public MergeModule(IChangesetService changesetService)
        {
            Get["/merge/candidates", runAsync: true] = async (parameter, ct) =>
            {
                var source = Request.Query.source;
                var destination = Request.Query.destination;
                var ignoreStartsWith = Request.Query.ignore.Value ?? "Merg";

                var candidates = await Task.Run(() =>
                    changesetService.MergeCandidates(source, destination, ignoreStartsWith));

                return Response.AsJson((IEnumerable<TfsChangesetMerged>)candidates);
            };
        }
    }
}