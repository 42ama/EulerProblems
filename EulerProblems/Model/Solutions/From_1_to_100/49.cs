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
    public class FortyNine
    {
        const string content =
            "Арифметическая прогрессия: 1487, 4817, 8147, в которой каждый член возрастает на 3330, необычна " +
            "в двух отношениях: " +
            "(1) каждый из трех членов является простым числом, " +
            "(2) все три четырехзначные числа являются перестановками друг друга. Не существует арифметических прогрессий " +
            "из трех однозначных, двухзначных и трехзначных простых чисел, демонстрирующих это свойство. " +
            "Однако, существует еще одна четырехзначная возрастающая арифметическая прогрессия. " +
            "Какое 12-значное число образуется, если объединить три члена этой прогрессии?";

        

        private string Calculate()
        {
            var set = new HashSet<int>();

            // максимальное 4х-значное число при котором можно получить
            // арифметическую прогрессию из 3х 4х-значных чисел
            // с шагом 3330 - это 3339
            for (int i = 1000; i <= 3339; i++)
            {
                if(Helper.IsPrime(i))
                {
                    set.Add(i);
                }
            }

            // эта последавтельность дана в условии, её пропускаем
            set.Remove(1487);

            // слагаемое, согласно условию равно 3330
            int term = 3330;
            // по условию необходимо найти последовательность из 3х чисел,
            // первое число хранится в set
            int timesTermAdded = 2;

            foreach (var primeNumber in set)
            {
                // сколько простых чисел нашли
                int timesPrimeFound = 0;
                int[] numbers = new int[timesTermAdded + 1];
                numbers[0] = primeNumber;
                for (int i = 1; i <= timesTermAdded; i++)
                {
                    numbers[i] = numbers[i - 1] + term;

                    if(Helper.IsPrime(numbers[i]))
                    {
                        timesPrimeFound++;
                    }
                }

                // если мы нашли необходимое количество чисел
                // проверяем являются ли они перестановками друг друга
                if(timesPrimeFound == timesTermAdded)
                {
                    // подготовим данные для использования в IsPermutations
                    char[][] lines = new char[numbers.Length][];
                    int counter = 0;
                    foreach (var number in numbers)
                    {
                        lines[counter] = number.ToString().ToCharArray();
                        counter++;
                    }

                    if (Helper.IsPermutations(lines))
                    {
                        var sb = new StringBuilder();
                        foreach (var number in numbers)
                        {
                            sb.Append(number);
                        }
                        return sb.ToString();
                    }
                }

            }
            return "ошибка, ответ не найден";
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 49,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какое 12-значное число образуется, если объединить три члена этой прогрессии?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

