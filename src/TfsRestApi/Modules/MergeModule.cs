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

    public class MergeModule : NancyModule
    {
        public MergeModule(IChangesetService changesetService)
        {
            Get["/merge/candidates"] = parameters =>
            {
                var source = Request.Query.source;
                var destination = Request.Query.destination;

                var candidates = changesetService.MergeCandidates(source, destination);
                return Response.AsJson((IEnumerable<MergeCandidate>)candidates);
            };
        }
    }
}