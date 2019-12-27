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
    public class ThirtyNine
    {
        const string content =
            "Если p - периметр прямоугольного треугольника с целочисленными длинами сторон {a,b,c}, то существует ровно три решения для p = 120:" +
            "\n{20,48,52}, {24,45,51}, {30,40,50}" +
            "\nКакое значение p ≤ 1000 дает максимальное число решений?";

        private bool IsPifagor(int a, int b, int c)
        {
            // в треугольнике сумма двух каждых сторон должна быть больше третьей стороны
            if(a*a + b*b == c*c)
            {
                return true;
            }
            return false;
        }

        private int Calculate()
        {
            var dict = new Dictionary<int, int>();
            //брутфорс
            for (int a = 1; a <= 1000; a++)
            {
                for (int b = a+1; b <= 1000; b++)
                {
                    for (int c = b+1; c <= 1000; c++)
                    {
                        int p = a + b + c;
                        if (p <= 1000)
                        {
                            if (IsPifagor(a, b, c))
                            {
                                if(dict.ContainsKey(p))
                                {
                                    dict[p]++;
                                }
                                else
                                {
                                    dict[p] = 1;
                                }
                            }
                        }
                    }
                }
            }
            int max = dict.Max(kvp => kvp.Value);
            var result = dict.Single(kvp => kvp.Value == max);
            return result.Key;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 39,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какое значение p ≤ 1000 дает максимальное число решений?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

