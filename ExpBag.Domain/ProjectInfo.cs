using ExpBag.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Domain
{
    public class ProjectInfo
    {
        public List<ProjectComponent> Components { get; set; }
        public string ProjectName { get; set; }
        public string RootPath { get; set; }

        public ProjectInfo()
        {
            Components = new List<ProjectComponent>();
        }
    }
}
