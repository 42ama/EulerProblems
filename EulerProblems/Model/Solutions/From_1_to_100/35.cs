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
    public class ThirtyFive
    {
        const string content =
            "Число 197 называется круговым простым числом, потому что все перестановки его цифр с конца в начало являются простыми числами: 197, 719 и 971. Существует тринадцать таких простых чисел меньше 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79 и 97. Сколько существует круговых простых чисел меньше миллиона?";

        private int NextCicular(int number)
        {
            var tempNumberCharArr = number.ToString().ToCharArray();

            var cicularCharArr = new char[tempNumberCharArr.Length];
            cicularCharArr[0] = tempNumberCharArr[^1];

            for (int i = 1; i < tempNumberCharArr.Length; i++)
            {
                cicularCharArr[i] = tempNumberCharArr[i - 1];
            }

            return Int32.Parse(new string(cicularCharArr));
        }

        private int Calculate(int upperBound)
        {
            var primes = new HashSet<int>();
            var circularPrimes = new HashSet<int>();
            for (int number = 2; number < upperBound; number++)
            {
                if(Helper.IsPrime(number))
                {
                    primes.Add(number);
                    bool isCircularPrime = true;

                    int tempNumber = 0;
                    int lastNumber = number;

                    while(tempNumber != number)
                    {
                        tempNumber = NextCicular(lastNumber);

                        if(!primes.Contains(tempNumber))
                        {
                            if(Helper.IsPrime(tempNumber))
                            {
                                primes.Add(tempNumber);
                            }
                            else
                            {
                                isCircularPrime = false;
                                break;
                            }
                        }

                        lastNumber = tempNumber;
                    }

                    if(isCircularPrime)
                    {
                        circularPrimes.Add(number);
                    }
                }
            }
            return circularPrimes.Count;
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
                Number = 35,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует круговых простых чисел меньше ста?",
                Result = Calculate(100).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate100)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует круговых простых чисел меньше миллиона?",
                Result = Calculate(1000000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1000000)
            });

            return pc;
        }
    }
}

