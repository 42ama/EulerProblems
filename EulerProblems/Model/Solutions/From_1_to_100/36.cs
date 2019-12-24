using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class ThirtySix
    {
        const string content =
            "Десятичное число 585_10 = 1001001001_2 (в двоичной системе), является палиндромом по обоим основаниям. Найдите сумму всех чисел меньше миллиона, являющихся палиндромами по основаниям 10 и 2. (Пожалуйста, обратите внимание на то, что палиндромы не могут начинаться с нуля ни в одном из оснований).";
        private int Calculate(int upperBound)
        {
            int summ = 0;
            for (int number = 1; number < upperBound; number++)
            {
                if(Helper.IsPalindrome(number.ToString().ToCharArray()))
                {
                    var numberBase2 = Helper.ConvertFromBase10(number, 2);
                    if (Helper.IsPalindrome(numberBase2))
                    {
                        summ += number;
                    }
                }
            }
            return summ;
        }

        private void Calculate100()
        {
            Calculate(100);
        }

        private void Calculate1000000()
        {
            Calculate(1000000);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 36,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму всех чисел меньше ста, являющихся палиндромами по основаниям 10 и 2.",
                Result = Calculate(100).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate100)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму всех чисел меньше миллиона, являющихся палиндромами по основаниям 10 и 2.",
                Result = Calculate(1000000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1000000)
            });

            return pc;
        }
    }
}

