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
    public class TwentyThree
    {
        const string content =
            "Идеальным числом называется число, у которого сумма его делителей равна самому числу. Например, сумма делителей числа 28 равна 1 + 2 + 4 + 7 + 14 = 28, что означает, что число 28 является идеальным числом. Число n называется недостаточным, если сумма его делителей меньше n, и называется избыточным, если сумма его делителей больше n. Так как число 12 является наименьшим избыточным числом (1 + 2 + 3 + 4 + 6 = 16), наименьшее число, которое может быть записано как сумма двух избыточных чисел, равно 24. Используя математический анализ, можно показать, что все целые числа больше 28123 могут быть записаны как сумма двух избыточных чисел.Эта граница не может быть уменьшена дальнейшим анализом, даже несмотря на то, что наибольшее число, которое не может быть записано как сумма двух избыточных чисел, меньше этой границы. Найдите сумму всех положительных чисел, которые не могут быть записаны как сумма двух избыточных чисел.";

        private bool IsAbundant(int number)
        {
            // из условия известно, что 12 минимальное избыточное число
            if(number < 12)
            {
                return false;
            }
            int excess = Helper.DivisorsWithoutTypical(number).Sum() + 1;
            return (excess > number) ? true : false;
        }

        private long Calculate(int upperBound = 28123)
        {
            var abundants = new HashSet<int>();
            var sumOfAbundants = new HashSet<int>();

            // находим избыточные числа, одно временно считаем их суммы
            for (int number = 1; number <= upperBound; number++)
            {
                if(IsAbundant(number))
                {
                    foreach (var abundant in abundants)
                    {
                        if(abundant + number <= upperBound)
                        {
                            sumOfAbundants.Add(abundant + number);
                        }
                    }
                    sumOfAbundants.Add(number + number);
                    abundants.Add(number);
                }
            }

            // считаем сумму всех чисел не являющихся суммой двух избыточных
            long summ = 0;
            for (int i = 1; i <= upperBound; i++)
            {
                if(!sumOfAbundants.Contains(i))
                {
                    summ += i;
                }
            }
            
            return summ;
        }


        private void CalculateBench()
        {
            Calculate();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 23,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Cумму всех положительных чисел, которые не могут быть записаны как сумма двух избыточных чисел.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

