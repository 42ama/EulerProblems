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
    public class TwentyFour
    {
        const string content =
            "Перестановка - это упорядоченная выборка объектов. К примеру, 3124 является одной из возможных перестановок из цифр 1, 2, 3 и 4. Если все перестановки приведены в порядке возрастания или алфавитном порядке, то такой порядок будем называть словарным. Словарные перестановки из цифр 0, 1, 2, 3 представлены ниже: 0123, 0132, 0213, 0231, 0312, 0321, 1023 ... Какова миллионная словарная перестановка из цифр 0, 1, 2, 3, 4, 5, 6, 7, 8 и 9?";
        
        private string CalculateBruteForce(int permutationIndex = 1000000)
        {
            // https://habr.com/ru/post/428552/ неплохо описан алгоритм
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 2; i <= permutationIndex; i++)
            {
                numbers.NextLexographic();
            }

            return string.Join("", numbers);
        }

        private string CalculateFactorialMagic(int permutationIndex = 1000000)
        {
            var finalNumber = new StringBuilder();
            List<int> numbers = new List<int>{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
            // первая перестановка уже найдена, вычитаем её из общего числа
            int permutIndex = permutationIndex - 1;

            // начиная с самого старшего разряда вычисляем число находящееся 
            // на конкретной позиции финальной перестановки.
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                int factorial = (int)Helper.Factorial(i);
                // смотрим сколько раз остаток от числа перестановок делится на данный факториал
                // полученное число соответстуется позиции числа в нашем оригинальном наборе
                int numberPosition = permutIndex/factorial;

                // записываем число в выходной набор и удаляем его из оригинального
                finalNumber.Append(numbers[numberPosition]);
                numbers.RemoveAt(numberPosition);

                permutIndex %= factorial * numberPosition;

                if (permutIndex == 0)
                {
                    break;
                }
            }

            // приписываем к концу оставшиеся числа
            foreach(var number in numbers)
            {
                finalNumber.Append(number);
            }


            return finalNumber.ToString();
        }


        private void CalculateBruteForceBench()
        {
            CalculateBruteForce();
        }

        private void CalculateFactorialMagicBench()
        {
            CalculateFactorialMagic();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 24,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова миллионная словарная перестановка из цифр 0, 1, 2, 3, 4, 5, 6, 7, 8 и 9? Подсчет вплоть до миллионой.",
                Result = CalculateBruteForce().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBruteForceBench)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова миллионная словарная перестановка из цифр 0, 1, 2, 3, 4, 5, 6, 7, 8 и 9? Вычисление миллионой с помощью факториалов.",
                Result = CalculateFactorialMagic().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateFactorialMagicBench)
            });

            return pc;
        }
    }
}

