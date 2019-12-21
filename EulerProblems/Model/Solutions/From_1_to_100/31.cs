using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class ThirtyOne
    {
        const string content =
            "В Англии валютой являются фунты стерлингов £ и пенсы p, и в обращении есть восемь монет: " +
            "1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) и £2 (200p). " +
            "£2 возможно составить следующим образом: " +
            "1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p " +
            "Сколькими разными способами можно составить £2, используя любое количество монет?";
        private int Calculate()
        {
            // за основу взято решение с MathBlog
            int target = 200;
            int ways = 0;

            for (int a = target; a >= 0; a -= 200)
            {
                for (int b = a; b >= 0; b -= 100)
                {
                    for (int c = b; c >= 0; c -= 50)
                    {
                        for (int d = c; d >= 0; d -= 20)
                        {
                            for (int e = d; e >= 0; e -= 10)
                            {
                                for (int f = e; f >= 0; f -= 5)
                                {
                                    for (int g = f; g >= 0; g -= 2)
                                    {
                                        ways++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ways;
        }

        private void CalculateBench()
        {
            Calculate();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 31,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколькими разными способами можно составить £2, используя любое количество монет?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

