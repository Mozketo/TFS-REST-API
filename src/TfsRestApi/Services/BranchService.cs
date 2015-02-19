namespace TfsRestApi.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using TfsRestApi.Services.Extensions;
    using MZMemoize.Extensions;    

    public interface IBranchService
    {
        IEnumerable<string> Get(string source);
    }

    public class BranchService : IBranchService
    {
        ITfsService _tfsService;

        public BranchService(ITfsService tfsService)
        {
            _tfsService = tfsService;
        }

        public IEnumerable<string> Get(string source)
        {
            var branchItems = _tfsService.VCS.GetBranchHistory(new[] { new ItemSpec(source, RecursionType.None, 0) }, VersionSpec.Latest);
            var branches = branchItems[0][0].GetRequestedItem()
                .Children.OfType<BranchHistoryTreeItem>();
            var brr = branches.Where(b => b.Relative.BranchToItem != null && b.Relative.BranchToItem.DeletionId == 0)
                .Select(b => b.Relative.BranchToItem.ServerItem)
                .OrderByDescending(b => b);
            return brr;
        }
    }
}