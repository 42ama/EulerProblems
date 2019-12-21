using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using EulerProblems.Model.Solutions.From_1_to_100;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;

namespace EulerProblems.Controller
{
    public static class ProblemHandling
    {
        public static IEnumerable<Type> GetProblemTypes()
        {
            return new List<Type>
            {
                typeof(One), typeof(Two), typeof(Three), typeof(Four), typeof(Five),
                typeof(Six), typeof(Seven), typeof(Eight), typeof(Nine), typeof(Ten),
                typeof(Eleven), typeof(Twelve), typeof(Thirteen), typeof(Fourteen), typeof(Fiveteen),
                typeof(Sixteen), typeof(Seventeen), typeof(Eighteen), typeof(Nineteen), typeof(Twenty),
                typeof(TwentyOne), typeof(TwentyTwo), typeof(TwentyThree), typeof(TwentyFour), typeof(TwentyFive),
                typeof(TwentySix), typeof(TwentySeven), typeof(TwentyEight), typeof(TwentyNine), typeof(Thirty),
                typeof(ThirtyOne), typeof(ThirtyTwo), 
            };
        }

        public static ProblemContainer RunSingle(Type type)
        {
            ProblemContainer problem = type.GetMethod("CompleteProblem")?.Invoke(Activator.CreateInstance(type), null) as ProblemContainer;
            return problem;
        }

    }
}
