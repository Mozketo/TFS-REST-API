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

    public class ChangesetModule : NancyModule
    {
        public ChangesetModule(ITfsService tfsService, IChangesetService changesetService)
        {
            Get["/changesets"] = parameters =>
            {
                string source = Request.Query.source;
                var from = Request.Query.from;
                bool includeChanges = false;

                var history = changesetService.History(source, from, includeChanges);

                return Response.AsJson((IEnumerable<TfsChangeset>)history);
            };

            Get["/changeset/{id}"] = _ =>
            {
                var id = _.id;

                var changeset = tfsService.VCS.GetChangeset(id);

                return Response.AsJson((TfsChangeset)changeset.ToModel());
            };
        }
    }
}