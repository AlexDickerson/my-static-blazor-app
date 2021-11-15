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
    public class ProjectsPut
    {
        private readonly IProjectData ProjectData;

        public ProjectsPut(IProjectData ProjectData)
        {
            this.ProjectData = ProjectData;
        }

        [FunctionName("ProjectsPut")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Projects")] HttpRequest req,
            ILogger log)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var Project = JsonSerializer.Deserialize<Project>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var updatedProject = await ProjectData.UpdateProject(Project);
            return new OkObjectResult(updatedProject);
        }
    }
}
