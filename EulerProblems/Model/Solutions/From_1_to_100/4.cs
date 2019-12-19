using System;
using System.Collections.Generic;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class Four : IProblem
    {
        const string content =
            "Число-палиндром с обеих сторон (справа налево и слева направо) читается одинаково. " +
            "Самое большое число-палиндром, полученное умножением двух двузначных чисел – 9009 = 91 × 99." +
            "Найдите самый большой палиндром, полученный умножением двух трехзначных чисел.";

        private int Calculate(out int numberA, out int numberB)
        {
            int maxi = 0;
            numberA = -1;
            numberB = -1;
            for (int i = 100; i <= 999; i++)
            {
                for (int j = 100; j <= 999; j++)
                {
                    int multi = i * j;
                    if (Helper.IsPalindome(multi.ToString().ToCharArray()) && (multi) > maxi)
                    {
                        numberA = i;
                        numberB = j;
                        maxi = multi;
                    }
                }
            }
            return maxi;
        }

        private void Calculate()
        {
            Calculate(out _, out _);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 4,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            var calc = Calculate(out int numberOne, out int numberTwo);

            pc.Cases.Add(new CaseContainer
            {
                Task = "Наибольшее число-палиндром, полученное умножением двух трехзначных чисел.",
                Result = $"{calc} = {numberOne} * {numberTwo}",
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate,2,3)
            });

            return pc;
        }

        
    }
}
