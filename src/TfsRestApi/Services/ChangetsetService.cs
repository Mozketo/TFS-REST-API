namespace TfsRestApi.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using TfsRestApi.Services.Extensions;

    public interface IChangesetService
    {
        IEnumerable<TfsChangeset> History(VersionControlServer vcs, string projectPath, DateTime from, bool includeChanges);
    }

    public class ChangesetService : IChangesetService
    {
        public IEnumerable<TfsChangeset> History(VersionControlServer vcs, string projectPath, DateTime from, bool includeChanges)
        {
            var history = vcs.QueryHistory(projectPath, VersionSpec.Latest, 0, RecursionType.Full,
                    null, new DateVersionSpec(from), null, int.MaxValue,
                    includeChanges, false, false, false)
                    .OfType<Changeset>().Reverse()
                    .Select(cs => cs.ToModel());
            return history;
        }
    }

    public class TfsChangeset
    {
        public int Changesetid { get; set; }
        public string Comment { get; set; }
        public string Committer { get; set; }
        public string CommitterDisplayName { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateHuman { get; set; }
        public Change[] Changes { get; set; }
    }
}