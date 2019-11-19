using System.Collections.Generic;

namespace EulerProject.Utility.DataContainers
{
    public class ProblemContainer
    {
        public int Number { get; set; }
        public string Task { get; set; }
        public List<CaseContainer> Cases { get; set; }
    }
}
