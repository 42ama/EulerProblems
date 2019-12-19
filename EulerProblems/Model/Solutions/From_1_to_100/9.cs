using System;
using System.Collections.Generic;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class Nine : IProblem
    {
        const string content =
            "Тройка Пифагора - три натуральных числа a < b < c, для которых выполняется равенство" +
            " a^2 + b^2 = c^2 " +
            "Например, 3^2 + 4^2 = 9 + 16 = 25 = 5^2. " +
            "Существует только одна тройка Пифагора, для которой a + b + c = 1000. " +
            "Найдите произведение abc.";

        private int Calculate(out int _a, out int _b, out int _c)
        {
            for (int a = 1; a < 1000; a++)
            {
                for (int b = 1; b < 1000; b++)
                {
                    for (int c = 1; c < 1000; c++)
                    {
                        if(a<b && b<c && a+b+c == 1000)
                        {
                            if(IsPifagor(a,b,c))
                            {
                                _a = a;
                                _b = b;
                                _c = c;
                                return a * b * c;
                            }
                        }
                    }
                }
            }
            throw new Exception("Тройка не найдена");
        }

        private void Calculate()
        {
            Calculate(out _, out _, out _);
        }

        private bool IsPifagor(int a, int b, int c)
        {
            if(Math.Pow(a,2)+Math.Pow(b,2)==Math.Pow(c,2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 9,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            int a, b, c;
            var calc = Calculate(out a, out b, out c);
            pc.Cases.Add(new CaseContainer
            {
                Task = "Произведение abc, где a,b,c - тройка Пифагора и a + b + c = 1000",
                Result = $"a = {a}, b = {b}, c = {c}, Произведение = {calc}",
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate,2,3)
            });

            return pc;
        }
    }
}
