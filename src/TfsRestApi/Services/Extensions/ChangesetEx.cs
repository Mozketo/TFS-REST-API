namespace TfsRestApi.Services.Extensions
{
    using Microsoft.TeamFoundation.VersionControl.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class ChangesetEx
    {
        public static TfsChangeset ToModel(this Changeset cs)
        {
            return new TfsChangeset
            {
                Changesetid = cs.ChangesetId,
                Comment = cs.Comment,
                Committer = cs.Committer,
                CommitterDisplayName = cs.CommitterDisplayName,
                CreationDate = cs.CreationDate,
                CreationDateHuman = cs.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Changes = cs.Changes,
            };
        }
    }
}