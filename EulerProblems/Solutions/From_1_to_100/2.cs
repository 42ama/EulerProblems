using System;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Two : IProblem
    {
        const string content =
            "Каждый следующий элемент ряда Фибоначчи получается при сложении двух предыдущих. " +
            "Начиная с 1 и 2, первые 10 элементов будут:" +
            "1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ..." +
            "Найдите сумму всех четных элементов ряда Фибоначчи, которые не превышают четыре миллиона.";
        
        private int Calculate(int length)
        {
            int prev = 1;
            int curr = 1;
            int summ = 0;
            while (curr < length)
            {
                int temp = 0;
                if (curr % 2 == 0)
                {
                    summ += curr;

                }
                temp = curr;
                curr += prev;
                prev = temp;
            }
            return summ;
        }
        private void Calculate10()
        {
            Calculate(10);
        }
        private void Calculate4000000()
        {
            Calculate(4000000);
        }
        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 2,
                Task = content,
                Cases = new List<CaseContainer>()
            };


            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма всех четных чисел ряда Фибоначи не превыщающих 10.",
                Result = Calculate(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate10)
            });


            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма всех четных чисел ряда Фибоначи не превыщающих 4000000.",
                Result = Calculate(4000000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate4000000)
            });


            return pc;
        }
    }
}
