using System.Linq;
using System.Collections.Generic;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class Twelve
    {
        const string content =
            "Последовательность треугольных чисел образуется путем сложения натуральных чисел. " +
            "К примеру, 7-ое треугольное число равно 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. " +
            "Первые десять треугольных чисел: 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ... " +
            "28 - первое треугольное число, у которого более пяти делителей. " +
            "Каково первое треугольное число, у которого более пятисот делителей?";

        private long Calculate(int divisorCount)
        {
            List<long> terms = new List<long> { 1 };
            int numberDivisors = 1;
            long number = 1;
            while(numberDivisors <= divisorCount)
            {
                terms.Add(terms.Last() + 1);
                number = terms.Sum();
                numberDivisors = Helper.DivisorsCount(number);
            }
            return number;
        }

        private void Calculate5()
        {
            Calculate(5);
        }

        private void Calculate500()
        {
            Calculate(500);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 12,
                Task = content,
                Cases = new List<CaseContainer>()
            };
            
            pc.Cases.Add(new CaseContainer
            {
                Task = "Каково первое треугольное число, у которого более пяти делителей.",
                Result = Calculate(5).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate5)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Каково первое треугольное число, у которого более пятисот делителей.",
                Result = Calculate(500).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate500, 2, 3)
            });

            return pc;
        }
    }
}
