using Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IProjectData
    {
        Task<Project> AddProject(Project Project);
        //Task<bool> DeleteProject(string id);
        Task<IEnumerable<Project>> GetProjects(string SessionID);
        Task<Project> UpdateProject(Project Project);
    }

    public class ProjectData : IProjectData
    {
        //ProjectData()
        //{
        //    Projects.Add(new Project { Id = 50, Name = "Pineapple", Description = "SpongeBob", Quantity = 20 });
        //}
        //JsonConvert.DeserializeObject<WFProject>(tokenString);
        private List<Project> Projects = new List<Project>(); //= new List<Project>
        private readonly List<Project> Projects2 = new List<Project>
        {
            new Project
            {
                ID = "asdfasdf",
                name = "Strawberry Project",
                description = "16oz package of fresh organic strawberries",
                percentComplete = "10"
            },
            new Project
            {
                ID = "20",
                name = "Sliced bread Project",
                description = "Load of fresh sliced wheat bread",
                percentComplete = "20"
            },
            new Project
            {
                ID = "30",
                name = "Apple Project",
                description = "Bag of 7 fresh McIntosh apples",
                percentComplete = "33"
            },
              new Project
            {
                ID = "40",
                name = "Grape Project",
                description = "1 Lb of fresh grapes",
                percentComplete = "50"
            }
        };

        private int GetRandomInt()
        {
            var random = new Random();
            return random.Next(100, 1000);
        }

        public Task<Project> AddProject(Project Project)
        {
            Project.ID = ""; GetRandomInt();
            Projects.Add(Project);
            return Task.FromResult(Project);
        }

        public Task<Project> UpdateProject(Project Project)
        {
            var index = Projects.FindIndex(p => p.ID == Project.ID);
            Projects[index] = Project;
            return Task.FromResult(Project);
        }

        public Task<bool> DeleteProject(string id)
        {
            var index = Projects.FindIndex(p => p.ID == id);
            Projects.RemoveAt(index);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Project>> GetProjects(string SessionID)
        {
            Projects.Clear();
            if((SessionID != null) && !SessionID.ToUpper().Equals("NOT SET"))
            {
                Api.RestClient client = new RestClient();
                string url = "https://denverwater.sb01.workfront.com/attask/api/v9.0/project/search?description_Mod=notnull&fields=description,percentComplete&sessionID=" + SessionID;
                JToken mytoken = client.DoRequest(url);
                Projects = JsonConvert.DeserializeObject<List<Project>>(mytoken["data"].ToString());

                //Projects.Add(new Project { ID = "50", name = "Pineapple", description = "SpongeBob", Quantity = 20 });
                return Task.FromResult(Projects.AsEnumerable());
            }
            else
            {
                Project p = new Project();
                p.name = "No Session Id defined yet.";
                   Projects2.Insert(0, p);
                return Task.FromResult(Projects2.AsEnumerable());
            }
        }
    }
}
