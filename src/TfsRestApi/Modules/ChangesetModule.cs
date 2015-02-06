﻿namespace TfsRestApi.Modules
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Nancy;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using TfsRestApi.Services;
    using System.Collections;
    using TfsRestApi.Services;
    using TfsRestApi.Services.Extensions;

    public class ChangesetModule : NancyModule
    {
        public ChangesetModule(ITfsService tfsService, IChangesetService changesetService)
        {
            var tfs = tfsService.Connection();
            var vcs = tfs.GetService<VersionControlServer>();

            Get["/changesets"] = parameters =>
            {
                string projectPath = Request.Query.projectPath;
                var from = Request.Query.from;
                bool includeChanges = false;

                var history = changesetService.History(vcs, projectPath, from, includeChanges);

                return Response.AsJson((IEnumerable<TfsChangeset>)history);
            };

            Get["/changeset/{id}"] = _ =>
            {
                var id = _.id;

                var changeset = vcs.GetChangeset(id);

                return Response.AsJson((TfsChangeset)changeset.ToModel());
            };
        }
    }
}