using System;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Six : IProblem
    {
        const string content =
            "Сумма квадратов первых десяти натуральных чисел равна " +
            "1^2 + 2^2 + ... + 10^2 = 385 " +
            "Квадрат суммы первых десяти натуральных чисел равен " +
            "(1 + 2 + ... + 10)^2 = 55^2 = 3025 " +
            "Следовательно, разность между суммой квадратов и квадратом суммы " +
            "первых десяти натуральных чисел составляет 3025 - 385 = 2640. " +
            "Найдите разность между суммой квадратов и квадратом суммы первых ста натуральных чисел.";

        private long Calculate(int upperBound)
        {
            return SqrOfSum(upperBound) - SumOfSqrs(upperBound);
        }

        private long SumOfSqrs(int upperBound)
        {
            long result = 0;
            for (int i = 1; i <= upperBound; i++)
            {
                result += (long)Math.Pow(i, 2);
            }
            return result;
        }

        private long SqrOfSum(int upperBound)
        {
            long summ = 0;
            for (int i = 1; i <= upperBound; i++)
            {
                summ += i;
            }
            return (long)Math.Pow(summ, 2);
        }

        private void Calculate10()
        {
            Calculate(10);
        }
        private void Calculate100()
        {
            Calculate(100);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 6,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите разность между суммой квадратов и квадратом суммы первых десяти натуральных чисел.",
                Result = Calculate(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate10)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите разность между суммой квадратов и квадратом суммы первых ста натуральных чисел.",
                Result = Calculate(100).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate100)
            });


            return pc;
        }
    }
}
