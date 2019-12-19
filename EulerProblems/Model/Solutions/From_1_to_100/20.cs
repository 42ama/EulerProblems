using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;
namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class Twenty
    {
        const string content =
            "n! означает n × (n − 1) × ... × 3 × 2 × 1 " +
            "Например, 10! = 10 × 9 × ... × 3 × 2 × 1 = 3628800, " +
            "и сумма цифр в числе 10! равна 3 + 6 + 2 + 8 + 8 + 0 + 0 = 27. " +
            "Найдите сумму цифр в числе 100!.";

        private int Calculate(int number)
        {
            var factorial = Helper.Factorial(number).ToString().ToCharArray();
            int summ = 0;
            for (int i = 0; i < factorial.Length; i++)
            {
                summ += (int)Char.GetNumericValue(factorial[i]);
            }
            return summ;
        }

        private void Calculate10()
        {
            Calculate(10);
        }

        private void Calculate100()
        {
            Calculate(100);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 20,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Cумма цифр 10!",
                Result = Calculate(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate10)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Cумма цифр 100!",
                Result = Calculate(100).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate100)
            });

            return pc;
        }
    }
}

