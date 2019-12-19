using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;
using EulerProblems.Utility.Extensions;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class TwentyNine
    {
        const string content =
            "Рассмотрим все целочисленные комбинации a^b для 2 ≤ a ≤ 5 и 2 ≤ b ≤ 5:" +
            "2^2=4, 2^3=8, 2^4=16, 2^5=32 " +
            "3^2=9, 3^3=27, 3^4=81, 3^5=243 " +
            "4^2=16, 4^3=64, 4^4=256, 4^5=1024" +
            "5^2=25, 5^3=125, 5^4=625, 5^5=3125 " +
            "Если их расположить в порядке возрастания, исключив повторения, мы получим " +
            "следующую последовательность из 15 различных членов: " +
            "4, 8, 9, 16, 25, 27, 32, 64, 81, 125, 243, 256, 625, 1024, 3125" +
            "Сколько различных членов имеет последовательность a^b для 2 ≤ a ≤ 100 и 2 ≤ b ≤ 100?";

        private int Calculate(Range rangeA, Range rangeB)
        {
            var set = new HashSet<double>();
            for (int a = rangeA.Start.Value; a <= rangeA.End.Value; a++)
            {
                for (int b = rangeB.Start.Value; b <= rangeB.End.Value; b++)
                {
                    double power = Math.Pow(a, b);
                    set.Add(power);
                }
            }
            return set.Count;
        }

        private void Calculate2To5()
        {
            Calculate(new Range(2, 5), new Range(2, 5));
        }

        private void Calculate2To100()
        {
            Calculate(new Range(2, 100), new Range(2, 100));
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 29,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько различных членов имеет последовательность a^b для 2 ≤ a ≤ 5 и 2 ≤ b ≤ 5?",
                Result = Calculate(new Range(2, 5), new Range(2, 5)).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate2To5)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько различных членов имеет последовательность a^b для 2 ≤ a ≤ 100 и 2 ≤ b ≤ 100?",
                Result = Calculate(new Range(2, 100), new Range(2, 100)).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate2To100)
            });

            return pc;
        }
    }
}

