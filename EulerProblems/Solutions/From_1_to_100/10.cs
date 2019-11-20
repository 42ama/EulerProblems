using System;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Ten : IProblem
    {
        const string content =
            "Сумма простых чисел меньше 10 равна 2 + 3 + 5 + 7 = 17. " +
            "Найдите сумму всех простых чисел меньше двух миллионов.";

        private long Calculate(int upperBound)
        {
            int number = 2;
            long summ = 0;
            while(number < upperBound)
            {
                if(Helper.IsPrime(number))
                {
                    summ += number;
                }
                number++;
            }
            return summ;
        }

        private void Calculate10()
        {
            Calculate(10);
        }
        private void Calculate2000000()
        {
            Calculate(10);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 10,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма простых чисел меньше 10.",
                Result = Calculate(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate10)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма простых чисел меньше двух миллионов.",
                Result = Calculate(2000000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate2000000)
            });

            return pc;
        }
    }
}
