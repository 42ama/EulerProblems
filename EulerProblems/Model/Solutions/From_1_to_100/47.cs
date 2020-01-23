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
    public class FortySeven
    {
        const string content =
            "Первые два последовательные числа, каждое из которых имеет два отличных друг от друга простых множителя:" +
            "\n14 = 2 × 7" +
            "\n15 = 3 × 5" +
            "\nПервые три последовательные числа, каждое из которых имеет три отличных друг от друга простых множителя:" +
            "\n644 = 2^2 × 7 × 23" +
            "\n645 = 3 × 5 × 43" +
            "\n646 = 2 × 17 × 19." +
            "\nНайдите первые четыре последовательных числа, каждое из которых имеет четыре отличных друг от друга простых множителя. Каким будет первое число?";


        private int Calculate(int count, bool useCache = true)
        {
            var cache = new Dictionary<int, ISet<int>>();
            int number = 3;
            while(true)
            {
                // собираем множества для каждого интересующего нас числа
                var setsWithNumbers =  new (ISet<int> set, int number)[count];
                for (int i = 0; i < count; i++)
                {
                    ISet<int> primeDivisors;

                    if (useCache && cache.ContainsKey(number + i))
                    {
                        primeDivisors = cache[number + i];
                    }
                    else
                    {
                        primeDivisors = Helper.DivisorsPrimeWithoutTypical(number + i);

                        if (useCache)
                        {
                            cache.Add(number + i, primeDivisors);
                        }
                    }

                    setsWithNumbers[i].set = primeDivisors;
                    setsWithNumbers[i].number = number + i;
                }

                // сравниваем множества между собой и удаляем общие элементы
                foreach (var divisors1 in setsWithNumbers)
                {
                    foreach (var divisors2 in setsWithNumbers)
                    {
                        if(divisors1.Equals(divisors2))
                        {
                            continue;
                        }

                        divisors2.set.ExceptBut47Rules(divisors1.set, divisors1.number);
                    }
                }

                // проверяем все множества на необходимое количество уникальных простых множителей
                bool numberFits = true;
                foreach (var divisors in setsWithNumbers)
                {
                    if(divisors.set.Count < count)
                    {
                        numberFits = false;
                        break;
                    }
                }
                
                // если все множества удоволятворяют условия количества множителей, то возвращаем число иначе берем следующее
                if(numberFits)
                {
                    return number;
                }
                else
                {
                    number++;
                }
            }
        }

        private void Calculate3()
        {
            Calculate(3);
        }

        private void Calculate4NoCache()
        {
            Calculate(4, useCache: false);
        }

        private void Calculate4Cache()
        {
            Calculate(4);
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 47,
                Task = content,
                Cases = new List<CaseContainer>()
            };
            
            pc.Cases.Add(new CaseContainer
            {
                Task = "Три последовательных числа. Каким будет первое число?",
                Result = Calculate(3).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate3)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Четыре последовательных числа. Каким будет первое число? Без использования кэша.",
                Result = Calculate(4).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate4NoCache, 1, 1)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Четыре последовательных числа. Каким будет первое число? С использованием кэша.",
                Result = Calculate(4).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate4Cache, 3, 3)
            });

            return pc;
        }
    }
}

