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
    public class FortyThree
    {
        const string content =
            "Число 1406357289, является пан-цифровым, поскольку оно состоит из цифр от 0 до 9 в определенном порядке. Помимо этого, оно также обладает интересным свойством делимости подстрок. " +
            "Пусть d1 будет 1-ой цифрой, d2 будет 2-ой цифрой, и т.д.В таком случае, можно заметить следующее:" +
            "\nd2d3d4=406 делится на 2 без остатка" +
            "\nd3d4d5=063 делится на 3 без остатка" +
            "\nd4d5d6=635 делится на 5 без остатка" +
            "\nd5d6d7=357 делится на 7 без остатка" +
            "\nd6d7d8=572 делится на 11 без остатка" +
            "\nd7d8d9=728 делится на 13 без остатка" +
            "\nd8d9d10=289 делится на 17 без остатка" +
            "\nНайдите сумму всех пан-цифровых чисел из цифр от 0 до 9, обладающих данным свойством.";

        static int[] firstPrimes = { 2, 3, 5, 7, 11, 13, 17 };
        
        private bool IsRuleAcceptable(string number)
        {
            // провряем подстроки
            for (int i = 0; i < 7; i++)
            {
                var subString = number.Substring(i + 1, 3);
                subString = subString.TrimStart('0');

                int subNumber = Int32.Parse(subString);

                if (subNumber % firstPrimes[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private long Calculate()
        {
            long summ = 0;

            int[] numberArr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // используем метод расширения для генераци лексографических перестановок, написанный при решении 24ой проблемы
            bool nextLex = numberArr.NextLexographic();
            while(nextLex)
            {
                // если число начинается с 0, то оно не 10и-значное
                if (numberArr[0] == 0)
                {
                    nextLex = numberArr.NextLexographic();
                    continue;
                }

                // проверяем обладает ли число заданным свойством
                var number = numberArr.CombineToString();
                if (IsRuleAcceptable(number))
                {
                    summ += Int64.Parse(number);
                }

                nextLex = numberArr.NextLexographic();
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
                Number = 43,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму всех пан-цифровых чисел из цифр от 0 до 9, обладающих данным свойством.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench, 3, 3)
            });

            return pc;
        }
    }
}

