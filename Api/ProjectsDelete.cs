using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    //public class ProjectsDelete
    //{
    //    private readonly IProjectData ProjectData;

    //    public ProjectsDelete(IProjectData ProjectData)
    //    {
    //        this.ProjectData = ProjectData;
    //    }

    //    [FunctionName("ProjectsDelete")]
    //    public async Task<IActionResult> Run(
    //        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Projects/{ProjectId:string}")] HttpRequest req,
    //        string ProjectId,
    //        ILogger log)
    //    {
    //        var result = await ProjectData.DeleteProject(ProjectId);

    //        if (result)
    //        {
    //            return new OkResult();
    //        }
    //        else
    //        {
    //            return new BadRequestResult();
    //        }
    //    }
    //}
}
