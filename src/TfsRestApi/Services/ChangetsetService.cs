namespace TfsRestApi.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using TfsRestApi.Services.Extensions;
    using MZMemoize.Extensions;    

    public interface IChangesetService
    {
        IEnumerable<TfsChangeset> History(string projectPath, DateTime from, bool includeChanges);
        IEnumerable<TfsChangesetMerged> MergeCandidates(string source, string destination);
    }

    public class ChangesetService : IChangesetService
    {
        ITfsService _tfsService;

        public ChangesetService(ITfsService tfsService)
        {
            _tfsService = tfsService;
        }

        public IEnumerable<TfsChangeset> History(string projectPath, DateTime from, bool includeChanges)
        {
            var history = _tfsService.VCS.QueryHistory(projectPath, VersionSpec.Latest, 0, RecursionType.Full,
                    null, new DateVersionSpec(from), null, int.MaxValue,
                    includeChanges, false, false, false)
                    .OfType<Changeset>().Reverse()
                    .Select(cs => cs.ToModel());
            return history;
        }

        /// <summary>
        /// Provide a list of candidate changesets that have not been merged between the source and destination.
        /// </summary>
        public IEnumerable<TfsChangesetMerged> MergeCandidates(string source, string destination)
        {
            var result = _tfsService.VCS.GetMergeCandidates(source, destination, RecursionType.Full)
                .Select(mc => mc.ToModel());
            return result;
        }

        public IEnumerable<int> ChangesetsInDestination(IEnumerable<int> changesets, string source, string destination)
        {
            if (!destination.HasValue())
                throw new ArgumentNullException("Destination is a required parameter");
            if (!source.HasValue())
                throw new ArgumentNullException("Source is a required parameter");

            var merged = new List<int>();

            foreach (var cs in changesets)
            {
                var mergeBranch = TrackChangesetIn(cs, source, new[] { destination }.AsEnumerable());
                if (!mergeBranch.Any())
                {
                    merged.Add(cs);
                }
            }

            return merged;
        }

        //public async Task<ExtendedMerge> TrackChangesetInAsync(Changeset changeset, string projectPath, string branch)
        //{
        //    var result = await TrackChangesetInAsync(changeset.ChangesetId, projectPath, new[] { branch });
        //    return result.FirstOrDefault();
        //}

        //public async Task<ExtendedMerge[]> TrackChangesetInAsync(Changeset changeset, string projectPath, IEnumerable<string> branches)
        //{
        //    var result = await TrackChangesetInAsync(changeset.ChangesetId, projectPath, branches);
        //    return result;
        //}

        ///// <summary>
        ///// Track a changeset merged into a possible list of branches.
        ///// </summary>
        ///// <param name="changesetId">The changeset ID to track</param>
        ///// <param name="projectPath">The source path of the changeset eg $/project/dev</param>
        ///// <param name="branches">A list of paths to check if the changeset has been merged into</param>
        ///// <returns>An array of Extended Merge</returns>
        //public async Task<ExtendedMerge[]> TrackChangesetInAsync(int changesetId, string projectPath, IEnumerable<string> branches)
        //{
        //    var t = await Task.Run(() =>
        //    {
        //        var result = TrackChangesetIn(changesetId, projectPath, branches);
        //        return result;
        //    });
        //    return t;
        //}

        public ExtendedMerge TrackChangesetIn(Changeset changeset, string projectPath, string branch)
        {
            var result = TrackChangesetIn(changeset.ChangesetId, projectPath, new[] { branch });
            return result.FirstOrDefault();
        }

        public ExtendedMerge[] TrackChangesetIn(int changesetId, string projectPath, string branch)
        {
            var result = TrackChangesetIn(changesetId, projectPath, new[] { branch });
            return result;
        }

        /// <summary>
        /// Track a changeset merged into a possible list of branches.
        /// </summary>
        /// <param name="changesetId">The changeset ID to track</param>
        /// <param name="projectPath">The source path of the changeset eg $/project/dev</param>
        /// <param name="branches">A list of paths to check if the changeset has been merged into</param>
        /// <returns>An array of Extended Merge</returns>
        public ExtendedMerge[] TrackChangesetIn(int changesetId, string projectPath, IEnumerable<string> branches)
        {
            var merges = _tfsService.VCS.TrackMerges(new int[] { changesetId },
                new ItemIdentifier(projectPath),
                branches.Select(b => new ItemIdentifier(b)).ToArray(),
                null);

            return merges;
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

    public class TfsChangesetMerged : TfsChangeset
    {
        public bool PartialMerge { get; set; }
    }
}