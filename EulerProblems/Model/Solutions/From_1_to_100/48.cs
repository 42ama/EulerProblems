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
    public class FortyEight
    {
        const string content =
            "Сумма 1^1 + 2^2 + 3^3 + ... + 10^10 = 10405071317. " +
            "Найдите последние десять цифр суммы 1^1 + 2^2 + 3^3 + ... + 1000^1000.";


        private long Calculate()
        {
            // все числа выше этой границы просчитывать не будем
            const long upperBound = 10000000000;
            long summ = 0;
            for (int i = 1; i <= 1000; i++)
            {
                long number;
                if (i > 10)
                {
                    number = PowerUpKeepBound(i, i, 10);
                }
                else
                {
                    // считаем степени для чисел, для которых их можнм посчитать
                    number = (long)Math.Pow(i, i) % upperBound;
                }
                
                summ = (summ + number) % upperBound;
                
            }
            return summ;
        }

        /// <summary>
        /// Считает степень числа, оставляя только <c>digits</c> последних цифр
        /// </summary>
        /// <param name="number">Число которое требуется возвести в степень</param>
        /// <param name="power">Степень в которую требуется возвести число</param>
        /// <param name="digits">Количество рассчитываемых цифр. Максимум 18.</param>
        /// <returns></returns>
        private long PowerUpKeepBound(int number, int power, int digits)
        {
            if(digits > 18)
            {
                throw new ArgumentOutOfRangeException("digits", "Количество расчитываемых цифр должно быть не больше 18и.");
            }

            BigInteger _number = new BigInteger(number);
            long upperBound = (long)Math.Pow(10, digits);

            for (int i = 2; i <= power; i++)
            {
                _number = (_number * number) % upperBound;
            }

            return (long)_number;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 48,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Последние десять цифр суммы 1^1 + 2^2 + 3^3 + ... + 1000^1000.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

