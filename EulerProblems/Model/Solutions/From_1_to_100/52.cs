using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;
using System.Text;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class FiftyTwo
    {
        const string content =
            "Может быть замечено, что числа - 125874 и оно же увеличенное в два раза - 251748, содержит те же самые цифры, но в другом порядке. Найдите такое наименьшее положительное целое число x, чтобы 2x, 3x, 4x, 5x и 6x состояли из одних и тех же цифр.";


        private int Calculate()
        {
            for (int number = 1; number < Int32.MaxValue; number++)
            {
                char[][] permutations = new char[6][];
                for (int i = 1; i <= 6; i++)
                {
                    permutations[i - 1] = (number * i).ToString().ToCharArray();
                }
                if(Helper.IsPermutations(permutations))
                {
                    return number;
                }
            }
            throw new Exception("Ошибка, решение не найдено");
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 52,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите такое наименьшее положительное целое число x, чтобы 2x, 3x, 4x, 5x и 6x состояли из одних и тех же цифр.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

