using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaBot.Services.Github;
using Refit;

namespace ClaBot.Controllers
{
    public abstract class GitHubControllerBase
    {
        protected IGitHubApi GitHubApi => RestService.For<IGitHubApi>("https://api.github.com");
    }
}
