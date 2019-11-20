using System;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Seven : IProblem
    {
        const string content =
            "Выписав первые шесть простых чисел, получим 2, 3, 5, 7, 11 и 13. " +
            "Очевидно, что 6-ое простое число - 13." +
            "Какое число является 10001-ым простым числом?";

        private long Calculate(int count)
        {
            int primeNumber = 1;
            long anyNumber = 0;
            while(primeNumber <= count)
            {
                anyNumber++;
                if (Helper.IsPrime(anyNumber))
                {
                    primeNumber++;
                }
            }
            return anyNumber;
        }

        private void Calculate6()
        {
            Calculate(6);
        }

        private void Calculate10001()
        {
            Calculate(10001);
        }
        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 7,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "6-ое простое число.",
                Result = Calculate(6).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate6)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "10001-ое простое число.",
                Result = Calculate(10001).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate10001)
            });


            return pc;
        }
    }
}
