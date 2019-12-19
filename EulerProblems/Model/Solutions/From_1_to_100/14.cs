using System.Linq;
using System.Collections.Generic;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class Fourteen
    {
        const string content =
            "Следующая повторяющаяся последовательность определена для множества натуральных чисел: " +
            "n → n/2 (n - четное) " +
            "n → 3n + 1 (n - нечетное) " +
            "Используя описанное выше правило и начиная с 13, сгенерируется следующая последовательность: " +
            "13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1 " +
            "Получившаяся последовательность (начиная с 13 и заканчивая 1) содержит 10 элементов. " +
            "Хотя это до сих пор и не доказано (проблема Коллатца), предполагается, что все сгенерированные таким " +
            "образом последовательности оканчиваются на 1. Какой начальный элемент меньше миллиона генерирует самую " +
            "длинную последовательность? " +
            "Примечание: Следующие за первым элементы последовательности могут быть больше миллиона.";

        private (int number, int length) CalculateWithoutCache(int upperBound)
        {
            (int number, int length) max = (0, 0);
            for (int number = upperBound - 1; number > 0; number--)
            {
                long numberToCheck = number;

                // включаем само число в последовательность
                int length = 1;

                while(numberToCheck != 1)
                {
                    if(numberToCheck % 2 == 0)
                        { numberToCheck /= 2; }
                    else
                        { numberToCheck = 3 * numberToCheck + 1; }
                    length++;
                }

                if(length > max.length)
                {
                    max.number = number;
                    max.length = length;
                }
            }
            return max;
        }

        private (int number, int length) CalculateWithCache(int upperBound)
        {
            // кэш в котором хранятся значения просчитанных последовательностей
            Dictionary<long, int> cache = new Dictionary<long, int>();

            (int number, int length) max = (0, 0);
            for (int number = upperBound - 1; number > 0; number--)
            {
                // дополнительный кэш, используемый для добавления новых значений в основной кэш
                Dictionary<long, int> precache = new Dictionary<long, int>();
                long numberToCheck = number;

                // включаем само число в последовательность
                int length = 1;

                while (numberToCheck != 1)
                {
                    if(cache.ContainsKey(numberToCheck))
                    {
                        length += cache[numberToCheck];
                        break;
                    }
                    else
                    {
                        precache[numberToCheck] = length;
                    }
                    if (numberToCheck % 2 == 0)
                    { numberToCheck /= 2; }
                    else
                    { numberToCheck = 3 * numberToCheck + 1; }
                    length++;
                }

                // добавляем значения в основной кэш
                foreach (var item in precache)
                {
                    cache[item.Key] = length - item.Value;
                }

                if (length > max.length)
                {
                    max.number = number;
                    max.length = length;
                }
            }
            return max;
        }

        private void CalculateWithCacheBench()
        {
            CalculateWithCache(1000000);
        }

        private void CalculateWithoutCacheBench()
        {
            CalculateWithoutCache(1000000);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 14,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            var calcA = CalculateWithCache(1000000);
            pc.Cases.Add(new CaseContainer
            {
                Task = "Какой начальный элемент меньше 1000000 генерирует самую длинную последовательность по заданному правилу? Метод с использование кэша.",
                Result = $"Элемент: {calcA.number}, длина: {calcA.length}",
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateWithCacheBench, 2, 3)
            });

            var calcB = CalculateWithoutCache(1000000);
            pc.Cases.Add(new CaseContainer
            {
                Task = "Какой начальный элемент меньше 1000000 генерирует самую длинную последовательность по заданному правилу? Метод без использования кэша",
                Result = $"Элемент: {calcB.number}, длина: {calcB.length}",
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateWithoutCacheBench, 2, 3)
            });

            return pc;
        }
    }
}
