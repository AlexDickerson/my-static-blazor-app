using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using Data;

namespace Api
{
    public class ProjectsPost
    {
        private readonly IProjectData ProjectData;

        public ProjectsPost(IProjectData ProjectData)
        {
            this.ProjectData = ProjectData;
        }

        [FunctionName("ProjectsPost")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Projects")] HttpRequest req,
            ILogger log)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var Project = JsonSerializer.Deserialize<Project>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var newProject = await ProjectData.AddProject(Project);
            return new OkObjectResult(newProject);
        }
    }
}
