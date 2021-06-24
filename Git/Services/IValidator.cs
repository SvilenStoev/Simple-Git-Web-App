using System.Collections.Generic;
using Git.Models.Commits;
using Git.Models.Repositories;
using Git.Models.Users;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateRepository(CreateRepositoryFormModel model);

        ICollection<string> ValidateCommit(CreateCommitFormModel model);
    }
}
