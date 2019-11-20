using System;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class One : IProblem
    {
        const string content =
            "Если выписать все натуральные числа меньше 10, кратные 3 или 5, то получим 3, 5, 6 и 9. " +
            "Сумма этих чисел равна 23. " +
            "Найдите сумму всех чисел меньше 1000, кратных 3 или 5.";

        private int Calculate(int length)
        {
            int summ = 0;
            for (int i = 1; i < length; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    summ += i;
                }
            }
            return summ;
        }

        private void Calculate10()
        {
            Calculate(10);
        }

        private void Calculate1000()
        {
            Calculate(1000);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 1,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма всех натуральных чисел меньше 10, кратных 3 или 5.",
                Result = Calculate(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate10)
            });


            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма всех натуральных чисел меньше 1000, кратных 3 или 5.",
                Result = Calculate(1000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1000)
            });


            return pc;
        }
    }
}
