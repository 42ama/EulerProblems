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
    public class FortySix
    {
        const string content =
            "Кристиан Гольдбах показал, что любое нечетное составное число можно записать в виде суммы простого числа и удвоенного квадрата." +
            "\n9 = 7 + 2×1^2" +
            "\n15 = 7 + 2×2^2" +
            "\n21 = 3 + 2×3^2" +
            "\n25 = 7 + 2×3^2" +
            "\n27 = 19 + 2×2^2" +
            "\n33 = 31 + 2×1^2" +
            "\nОказалось, что данная гипотеза неверна. Каково наименьшее нечетное составное число, которое нельзя записать в виде суммы простого числа и удвоенного квадрата?";


        private int Calculate()
        {
            var primes = new HashSet<int>{ 1 };
            // будем проходить все числа с 2х, потому что для вычисления необходимо
            // собрать все простые числа меньше искомого составного
            int number = 2;
            while(true)
            {
                if(!Helper.IsPrime(number))
                {
                    // проверяем только нечетные
                    if(number % 2 == 1)
                    {
                        // если в процессе проверки мы находим числа удоволетворяющие
                        // условию "нечетное составное число можно записать в виде суммы 
                        // простого числа и удвоенного квадрата.", прекращаем проверку для данного числа
                        // если такие числа не найдены, считаем проблему решеной
                        bool isFound = false;
                        foreach (var prime in primes)
                        {
                            // вычисляем остаток, и сверяем его с удвоенными квадратами, пока
                            int reminder = number - prime;
                            int counter = 1;
                            int calcReminder = 0;
                            while(calcReminder <= reminder)
                            {
                                calcReminder = 2 * counter * counter;
                                if (calcReminder == reminder)
                                {
                                    isFound = true;
                                }
                                counter++;
                            }

                            // если числа подходящие условию уже найдены, нет смысла
                            // проверяеть остальные комбинации
                            if(isFound)
                            {
                                break;
                            }
                        }
                        // если при проверке всех комбинаций числа не были найдены, то
                        // считаем проблему решеной
                        if(!isFound)
                        {
                            return number;
                        }
                    }
                }
                else
                {
                    // собираем все простые меньше искомого составного
                    primes.Add(number);
                }

                number++;
            }
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 46,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Каково наименьшее нечетное составное число, которое нельзя записать в виде суммы простого числа и удвоенного квадрата?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

