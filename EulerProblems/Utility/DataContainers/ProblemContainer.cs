using System.Collections.Generic;

namespace EulerProblems.Utility.DataContainers
{
    public class ProblemContainer
    {
        public int Number { get; set; }
        public string Task { get; set; }
        public List<CaseContainer> Cases { get; set; }
    }
}
