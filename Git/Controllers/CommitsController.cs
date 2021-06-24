using Git.Data;
using Git.Models.Commits;
using Git.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly GitDbContext data;
        private readonly IValidator validator;

        public CommitsController(GitDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repositoriy = this.data
                .Repositories
                .Where(r => r.Id == id)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new CommitToRepositoryViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .FirstOrDefault();

            if (repositoriy == null)
            {
                return BadRequest();
            }

            return View(repositoriy);
        }


        [Authorize]
        public HttpResponse All()
        {
            var commits = this.data
                .Commites
                .Where(c => c.Creator.Id == this.User.Id)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new CommitListingViewModel
                {
                     Id = c.Id,
                     Description = c.Description,
                     CreatedOn = c.CreatedOn.ToLocalTime().ToString("F"),
                     Repository = c.Repository.Name,
                })
                .ToList();

            return View(commits);
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateCommitFormModel model)
        {
            if (!this.data.Repositories.Any(r => r.Id == model.Id))
            {
                return BadRequest();
            }

            var modelErrors = this.validator.ValidateCommit(model);
             
            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var commit = new Commit
            {
                Description = model.Description,
                RepositoryId = model.Id,
                CreatorId = this.User.Id
            };

            this.data.Commites.Add(commit);

            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Delete(string id)
        {
            var commit = this.data
                .Commites
                .Where(r => r.Id == id)
                .FirstOrDefault();

            if (commit == null || commit.CreatorId != this.User.Id)
            {
                return BadRequest();
            }

            this.data.Commites.Remove(commit);

            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
