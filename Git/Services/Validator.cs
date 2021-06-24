using Git.Data;
using Git.Models.Commits;
using Git.Models.Repositories;
using Git.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Git.Services
{
    using static DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UserMinUsername || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinUsername} and {DefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < UserMinPassword || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {DefaultMaxLength} characters long.");
            }

            if (model.Password.Count(p => char.IsLetter(p)) < 4 || model.Password.Count(p => char.IsDigit(p)) < 2)
            {
                errors.Add($"The password should containt at least 4 letters and 2 digits.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }

        public ICollection<string> ValidateRepository(CreateRepositoryFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < RepositoryMinName || model.Name.Length > RepositoryMaxName)
            {
                errors.Add($"Repository '{model.Name}' is not valid. It must be between {RepositoryMinName} and {RepositoryMaxName} characters long.");
            }

            if (model.RepositoryType != RepositoryPublicType && model.RepositoryType != RepositoryPrivateType)
            {
                errors.Add($"Repository type can be either '{RepositoryPublicType}' or '{RepositoryPrivateType}'.");
            }

            return errors;
        }

        public ICollection<string> ValidateCommit(CreateCommitFormModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < CommitMinDescription)
            {
                errors.Add($"Commit '{model.Description}' is not valid. It must be more than {CommitMinDescription} characters long.");
            }

            return errors;
        }
    }
}
