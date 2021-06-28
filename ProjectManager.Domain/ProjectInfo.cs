using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public class ProjectInfo
    {
        public List<ProjectComponent> Components { get; set; }


        public ProjectInfo()
        {
            Components = new List<ProjectComponent>();
        }
    }
}
