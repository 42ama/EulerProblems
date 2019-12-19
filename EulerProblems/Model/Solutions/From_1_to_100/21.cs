using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class TwentyOne
    {
        const string content =
            "Пусть d(n) определяется как сумма делителей n (числа меньше n, делящие n нацело). " +
            "Если d(a)=b и d(b)=a, где a ≠ b, то a и b называются дружественной парой, а каждое " +
            "из чисел a и b - дружественным числом. Например, делителями числа 220 являются " +
            "1, 2, 4, 5, 10, 11, 20, 22, 44, 55 и 110, поэтому d(220) = 284. " +
            "Делители 284 - 1, 2, 4, 71, 142, поэтому d(284) = 220. " +
            "Подсчитайте сумму всех дружественных чисел меньше 10000.";

        HashSet<long> checkedNumbers = new HashSet<long>();
        private long Calculate(int upperBound = 10000)
        {
            for (int i = 1; i < upperBound; i++)
            {
                if(!checkedNumbers.Contains(i))
                {
                    var divisorsA = Helper.DivisorsWithoutTypical(i);
                    // необходимо добавить единицу, так как метод возвращает только уникальные делители
                    // а в условии при подсчете используется единица (хоть и не используется 
                    // само число, как делитель само себя)
                    long numberB = divisorsA.Sum() + 1;
                    if (numberB != i)
                    {
                        var divisorsB = Helper.DivisorsWithoutTypical(numberB);
                        if (divisorsB.Sum() + 1 == i )
                        {
                            checkedNumbers.Add(i);
                            checkedNumbers.Add(numberB);
                        }
                    }
                }
            }
            return checkedNumbers.Sum();
        }

        private void CalculateBench()
        {
            Calculate();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 21,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Cумму всех дружественных чисел меньше 10000",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

