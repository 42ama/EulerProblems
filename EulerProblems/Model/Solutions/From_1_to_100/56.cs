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
    public class FiftySix
    {
        const string content =
            "Гугол (10^100) - гигантское число: один со ста нулями; 100^100 почти невообразимо велико: один с двумястами нулями. Несмотря на их размер, сумма цифр каждого числа равна всего лишь 1. Рассматривая натуральные числа вида a^b, где a, b < 100, какая встретится максимальная сумма цифр числа?";


        private int Calculate()
        {
            int max = -1;
            // рассчитыаем все степени и соберем сумму цифр каждого числа
            for (int a = 1; a < 100; a++)
            {
                for (int b = 1; b < 100; b++)
                {
                    var number = BigInteger.Pow(a, b);

                    int summ = 0;
                    // быстрее чем версия с mod в ~3 раза
                    var numberString = number.ToString();
                    for (int i = numberString.Length - 1; i >= 0; i--)
                    {
                        summ += (int)Char.GetNumericValue(numberString[i]);
                    }

                    if (summ > max)
                    {
                        max = summ;
                    }
                }
            }
            return max;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 56,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Рассматривая натуральные числа вида a^b, где a, b < 100, какая встретится максимальная сумма цифр числа?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

