using System;
using System.Collections.Generic;
using System.Text;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class Seventeen
    {
        const string content =
            "Если записать числа от 1 до 5 английскими словами (one, two, three, four, five), " +
            "то используется всего 3 + 3 + 5 + 4 + 4 = 19 букв. Сколько букв понадобится для " +
            "записи всех чисел от 1 до 1000 (one thousand) включительно? " +
            "Примечание: Не считайте пробелы и дефисы.Например, число 342 (three hundred and forty-two) " +
            "состоит из 23 букв, число 115 (one hundred and fifteen) - из 20 букв.Использование and " +
            "при записи чисел соответствует правилам британского английского.";
        Dictionary<int, string> oneToNine = new Dictionary<int, string>
        {
            {1, "one" },
            {2, "two" },
            {3, "three" },
            {4, "four" },
            {5, "five" },
            {6, "six" },
            {7, "seven" },
            {8, "eight" },
            {9, "nine" },
        };

        Dictionary<int, string> deviations = new Dictionary<int, string>
        {
            {10, "ten" },
            {11, "eleven" },
            {12, "twelve" },
            {14, "fourteen" }
        };

        Dictionary<int, string> numberPart = new Dictionary<int, string>
        {
            {2, "twent" },
            {3, "thirt" },
            {4, "fort" },
            {5, "fift" },
            {6, "sixt" },
            {7, "sevent" },
            {8, "eight" },
            {9, "ninet" },
        };

        KeyValuePair<int, string> numberPartDeviation = new KeyValuePair<int, string>(1, "een");



        private long Calculate(int upperBound)
        {
            long totalLength = 0;
            for (int i = 1; i <= upperBound; i++)
            {
                StringBuilder numberBuilder = new StringBuilder();

                int thousands = i / 1000;
                int hundreds = i / 100;
                int dozensPlusUnits = i % 100;
                int dozens = dozensPlusUnits / 10;
                int units = dozensPlusUnits % 10;

                // в нашей выборке максимальное число 1000
                if (thousands == 1)
                {
                    totalLength += "onethousand".Length;
                    continue;
                }

                // добавляем количество сотен
                if (hundreds > 0)
                {
                    numberBuilder.Append(oneToNine[hundreds]);
                    numberBuilder.Append("hundred");
                    if(dozens > 0 || units > 0)
                    {
                       numberBuilder.Append("and");
                    }
                }

                // добавляем количество десятков
                if (dozens > 0)
                {
                    if (dozens == numberPartDeviation.Key)
                    {
                        if (deviations.ContainsKey(dozensPlusUnits))
                        {
                            numberBuilder.Append(deviations[dozensPlusUnits]);
                        }
                        else
                        {
                            numberBuilder.Append(numberPart[units]);
                            numberBuilder.Append(numberPartDeviation.Value);
                        }
                    }
                    else
                    {
                        numberBuilder.Append(numberPart[dozens]);
                        numberBuilder.Append("y");
                    }
                }

                // добавляем количество единиц
                if (units > 0 && dozens != numberPartDeviation.Key)
                {
                    numberBuilder.Append(oneToNine[units]);
                }

                // считаем символы в текущем числе
                totalLength += numberBuilder.Length;
            }
            return totalLength;
        }

        private void Calculate5()
        {
            Calculate(5);
        }

        private void Calculate1000()
        {
            Calculate(1000);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 17,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько букв понадобится для записи всех чисел от 1 до 5 включительно?",
                Result = Calculate(5).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate5)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько букв понадобится для записи всех чисел от 1 до 1000 включительно?",
                Result = Calculate(1000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1000)
            });

            return pc;
        }
    }
}

