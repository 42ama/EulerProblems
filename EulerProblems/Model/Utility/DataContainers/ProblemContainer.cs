using System.Collections.Generic;

namespace EulerProblems.Model.Utility.DataContainers
{
    public class ProblemContainer
    {
        public int Number { get; set; }
        public string Task { get; set; }
        public List<CaseContainer> Cases { get; set; }
    }
}
