using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Models.Commits
{
    public class CommitListingViewModel
    {
        public string Id { get; set; }

        public string Repository { get; init; }

        public string Description { get; init; }

        public string CreatedOn { get; init; }

    }
}
