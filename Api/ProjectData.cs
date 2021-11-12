using Data;
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
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> UpdateProject(Project Project);
    }

    public class ProjectData : IProjectData
    {
        //ProjectData()
        //{
        //    Projects.Add(new Project { Id = 50, Name = "Pineapple", Description = "SpongeBob", Quantity = 20 });
        //}
        private readonly List<Project> Projects = new List<Project>
        {
            new Project
            {
                ID = "asdfasdf",
                name = "Strawberry Project",
                description = "16oz package of fresh organic strawberries",
                Quantity = 1
            },
            new Project
            {
                ID = "20",
                name = "Sliced bread Project",
                description = "Load of fresh sliced wheat bread",
                Quantity = 1
            },
            new Project
            {
                ID = "30",
                name = "Apple Project",
                description = "Bag of 7 fresh McIntosh apples",
                Quantity = 1
            },
              new Project
            {
                ID = "40",
                name = "Grape Project",
                description = "1 Lb of fresh grapes",
                Quantity = 1
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

        public Task<IEnumerable<Project>> GetProjects()
        {
            Projects.Add(new Project { ID = "50", name = "Pineapple", description = "SpongeBob", Quantity = 20 });
            return Task.FromResult(Projects.AsEnumerable());
        }
    }
}
