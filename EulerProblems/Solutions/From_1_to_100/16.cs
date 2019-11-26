using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Sixteen
    {
        const string content =
            "2^15 = 32768, сумма цифр этого числа равна 3 + 2 + 7 + 6 + 8 = 26." +
            "Какова сумма цифр числа 2^1000?";

        private int Calculate(int power)
        {
            string number = BigInteger.Pow(2, power).ToString();
            int summ = 0;
            foreach (var digit in number)
            {
                summ += (int)Char.GetNumericValue(digit);
            }
            return summ;
        }

        private void Calculate15()
        {
            Calculate(15);
        }

        private void Calculate1000()
        {
            Calculate(1000);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 16,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Cумма цифр 2^15.",
                Result = Calculate(15).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate15)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Cумма цифр 2^1000.",
                Result = Calculate(1000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1000)
            });

            return pc;
        }
    }
}

