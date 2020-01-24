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
    public class Fifty
    {
        const string content =
            "Простое число 41 можно записать в виде суммы шести последовательных простых чисел:" +
            "\n41 = 2 + 3 + 5 + 7 + 11 + 13" +
            "\nЭто - самая длинная сумма последовательных простых чисел, в результате которой получается простое число меньше одной сотни. Самая длинная сумма последовательных простых чисел, в результате которой получается простое число меньше одной тысячи, содержит 21 слагаемое и равна 953. Какое из простых чисел меньше одного миллиона можно записать в виде суммы наибольшего количества последовательных простых чисел?";


        private int Calculate(int upperBound)
        {
            // собирем все простые числа которые образуют сумму меньше
            // миллиона, при этом не обязательно чтобы сумма была простая
            int notPrimeSum = 0;
            int number = 2;
            var primes = new List<int>();
            while(notPrimeSum + number < upperBound)
            {
                if(Helper.IsPrime(number))
                {
                    primes.Add(number);
                    notPrimeSum += number;
                }
                number++;
            }

            // если эта сумма простая, возвращаем её
            if (Helper.IsPrime(notPrimeSum))
            {
                return notPrimeSum;
            }


            // иначе будем исключать из списка простых чисел, числа начиная
            // с наименьшего. Первая найденная простая сумма - будет максимальной
            // подходящей под условия

            // перевернем список, так как GetRange с изменением конца списка - менее 
            // затраная операция, чем GetRange с изменением начала списка
            primes.Reverse();
            
            for (int i = primes.Count - 1; i >= 0; i--)
            {
                int primeSum = primes.GetRange(0, i).Sum();
                if(Helper.IsPrime(primeSum))
                {
                    return primeSum;
                }
            }

            throw new Exception("Ошибка, решение не найдено");
        }

        private void CalculateBench()
        {
            Calculate(1000000);
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 50,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какое 12-значное число образуется, если объединить три члена этой прогрессии?",
                Result = Calculate(1000000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

