using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace Api
{
    public class ProjectsGet
    {
        private readonly IProjectData ProjectData;

        public ProjectsGet(IProjectData ProjectData)
        {
            this.ProjectData = ProjectData;
        }

        [FunctionName("ProjectsGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Projects")] HttpRequest req)
        {
            IQueryCollection x = req.Query;
            string sessionID = x["SessionID"];
            var Projects = await ProjectData.GetProjects(sessionID);
            return new OkObjectResult(Projects);
        }
    }
}
