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
    public class Forty
    {
        const string content =
            "Дана иррациональная десятичная дробь, образованная объединением положительных целых чисел: 0.123456789101112131415161718192021... Видно, что 12-ая цифра дробной части - 1. Также дано, что dn представляет собой n-ую цифру дробной части.Найдите значение следующего выражения:" +
            "\n"+"d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000";

        static HashSet<int> positionsToCollect = new HashSet<int> { 1, 10, 100, 1000, 10000, 100000, 1000000 };

        private int Calculate()
        {
            // сюда сохраняем результат произведения цифр на позициях
            int production = 1;

            // счетчик десяток, используется для увеличения значения digitsInNumber при достижении
            // своей верхней границы
            int counterForDozens = 2;
            int maxCForDozens = 10;

            // позиция цифры в дроби
            int position = 1;
            // текущее число
            int number = 1;
            // позиция цифры в числе
            int currentDigit = 1;
            // всего цифр в числе
            int digitsInNumber = 1;
            // проверяем все числа с 1ой позиции по 1000000ую
            while (position <= 1000000)
            {
                // забираем число, если мы находимся в правильной позиции
                if (positionsToCollect.Contains(position))
                {
                    int digit = (int)Char.GetNumericValue(number.ToString().ToCharArray()[currentDigit - 1]);
                    production *= digit;
                }

                // если мы прошли все цифры в числе
                if (currentDigit >= digitsInNumber)
                {
                    // берем следующее число
                    currentDigit = 1;
                    number++;
                    // если мы перешли за следующий разряд (например с 1го разряда на 2 - с 9и на 10)
                    if (counterForDozens == maxCForDozens)
                    {
                        // увеличиваем количество цифр в числе
                        digitsInNumber++;
                        // возвращаем переменным стандартное значение
                        currentDigit = 1;
                        counterForDozens = 1;
                        // обновляем верхнюю границу
                        maxCForDozens = (int)(Math.Pow(10, digitsInNumber) - Math.Pow(10, digitsInNumber - 1));
                    }
                    else
                    {
                        counterForDozens++;
                    }
                }
                else
                {
                    currentDigit++;
                }
                

                position++;
            }
            return production;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 40,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите значение следующего выражения: d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

