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
    public class ThirtySeven
    {
        const string content =
            "Число 3797 обладает интересным свойством. Будучи само по себе простым числом, из него можно последовательно выбрасывать цифры слева направо, число же при этом остается простым на каждом этапе: 3797, 797, 97, 7. Точно таким же способом можно выбрасывать цифры справа налево: 3797, 379, 37, 3. Найдите сумму единственных одиннадцати простых чисел, из которых можно выбрасывать цифры как справа налево, так и слева направо, но числа при этом остаются простыми. ПРИМЕЧАНИЕ: числа 2, 3, 5 и 7 таковыми не считаются.";

        static HashSet<int> primes = new HashSet<int> { 2, 3, 5, 7 };

        private bool IsTruncatablesPrime(int number)
        {
            int tempNumber = number;

            int dozens = 0;
            // идем справа налево
            while(tempNumber != 0)
            {
                if(!primes.Contains(tempNumber))
                {
                    if(!Helper.IsPrime(tempNumber))
                    {
                        return false;
                    }
                    else
                    {
                        primes.Add(tempNumber);
                    }
                }

                dozens++;
                tempNumber /= 10;
            }

            tempNumber = number;
            // и слева направо
            for (int i = dozens-1; i >= 0; i--)
            {
                if (!primes.Contains(tempNumber))
                {
                    if (!Helper.IsPrime(tempNumber))
                    {
                        return false;
                    }
                    else
                    {
                        primes.Add(tempNumber);
                    }
                }

                tempNumber %= (int)Math.Pow(10, i);
            }

            return true;
        }

        private int Calculate()
        {
            // по условию известно, что таких чисел всего 11
            int[] result = new int[11];

            int arrPointer = 0;
            int number = 10;

            while(result[^1] == 0)
            {
                if(IsTruncatablesPrime(number))
                {
                    result[arrPointer] = number;
                    arrPointer++;
                }
                number++;
            }

            return result.Sum();
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 37,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму единственных одиннадцати простых чисел, из которых можно выбрасывать цифры как справа налево, так и слева направо, но числа при этом остаются простыми.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });
            
            return pc;
        }
    }
}

