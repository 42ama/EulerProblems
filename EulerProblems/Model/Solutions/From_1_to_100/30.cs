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
    public class Thirty 
    {
        const string content =
            "Удивительно, но существует только три числа, которые могут быть записаны в " +
            "виде суммы четвертых степеней их цифр: " +
            "1634 = 1^4 + 6^4 + 3^4 + 4^4 " +
            "8208 = 8^4 + 2^4 + 0^4 + 8^4 " +
            "9474 = 9^4 + 4^4 + 7^4 + 4^4 " +
            "1 = 1^4 не считается, так как это - не сумма. " +
            "Сумма этих чисел равна 1634 + 8208 + 9474 = 19316. " +
            "Найдите сумму всех чисел, которые могут быть записаны в виде суммы пятых степеней их цифр.";

        private int FindUpperBound(int power)
        {
            int a = 1;
            int b = a * (int)Math.Pow(9, power);
            // если условие верно, то использовав наше правило на неком числе с количеством цифр a, мы
            // в общем с случае сможем получить число b
            while (b.ToString().Length >= a)
            {
                a++;
                b = a * (int)Math.Pow(9, power);
            }
            // когда условие перестает выполняться, то никакое число с количеством цифр a, не даст
            // нам b в общем случае, а значит и дальше проверять числа в нашей задаче не имеет смысла
            // и последнее найденное b является верхней границей
            return b;
        }


        private int CalculateCharConvert(int power)
        {
            int number = 2;
            int summOfNumbers = 0;
            while(number <= FindUpperBound(power))
            {
                string numberAsString = number.ToString();
                int summOfPowers = 0;

                foreach (var chara in numberAsString)
                {
                    var digit = Char.GetNumericValue(chara);
                    summOfPowers += (int)Math.Pow(digit, power);
                }

                if (summOfPowers == number)
                {
                    summOfNumbers += number;
                }

                number++;
            }
            return summOfNumbers;
        }

        private int CalculateMod(int power)
        {
            int number = 2;
            int summOfNumbers = 0;
            while (number <= FindUpperBound(power))
            {
                int summOfPowers = 0;
                int tempNumber = number;
                while (tempNumber != 0)
                {
                    int digit = tempNumber % 10;
                    summOfPowers += (int)Math.Pow(digit, power);

                    tempNumber /= 10;
                }
                

                if (summOfPowers == number)
                {
                    summOfNumbers += number;
                }

                number++;
            }
            return summOfNumbers;
        }

        private void CalculateCharConvert4()
        {
            CalculateCharConvert(4);
        }

        private void CalculateCharConvert5()
        {
            CalculateCharConvert(5);
        }

        private void CalculateMod4()
        {
            CalculateMod(4);
        }

        private void CalculateMod5()
        {
            CalculateMod(5);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 30,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму всех чисел, которые могут быть записаны в виде суммы четвертых степеней их цифр. Парсинг чисел как строк.",
                Result = CalculateCharConvert(4).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateCharConvert4)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму всех чисел, которые могут быть записаны в виде суммы пятых степеней их цифр. Парсинг чисел как строк.",
                Result = CalculateCharConvert(5).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateCharConvert5, 3, 3)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму всех чисел, которые могут быть записаны в виде суммы четвертых степеней их цифр. Постепенно уменьшение числа делением на 10 с округлением вплоть до 0.",
                Result = CalculateMod(4).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateMod4)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите сумму всех чисел, которые могут быть записаны в виде суммы пятых степеней их цифр. Постепенно уменьшение числа делением на 10 с округлением вплоть до 0.",
                Result = CalculateMod(5).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateMod5, 3, 3)
            });

            return pc;
        }
    }
}

