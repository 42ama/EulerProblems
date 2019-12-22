using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class ThirtyFour
    {
        const string content =
            "145 является любопытным числом, поскольку 1! + 4! + 5! = 1 + 24 + 120 = 145. " +
            "Найдите сумму всех чисел, каждое из которых равно сумме факториалов своих цифр. " +
            "Примечание: поскольку 1! = 1 и 2! = 2 не являются суммами, учитывать их не следует.";


        private int Calculate()
        {
            int summ = 0;
            var cache = new Dictionary<int, int>();
            // 10 является нижней границей, по условию пропускаем все цифры которые меньше, потому что они не образуют суммы
            // 9!7 = 2540160 является верхней границей, потому что 9!8 также возвращет 7изначние число, а чтобы оно подходило под правило должно возвращать 8и значное
            for (int num = 10; num <= 2540160; num++)
            {
                var digits = Helper.GetAllDigits(num);
                int digitsSumm = 0;
                foreach (var digit in digits)
                {
                    if(cache.ContainsKey(digit))
                    {
                        digitsSumm += cache[digit];
                    }
                    else
                    {
                        cache[digit] = (int)Helper.Factorial(digit);
                        digitsSumm += cache[digit];
                    }
                    
                    if(digitsSumm > num)
                    {
                        break;
                    }
                }
                if(digitsSumm == num)
                {
                    summ += num;
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
                Number = 34,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма всех чисел, каждое из которых равно сумме факториалов своих цифр.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

