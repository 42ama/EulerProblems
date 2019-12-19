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
    public class TwentySeven
    {
        const string content =
            "Эйлер опубликовал свою замечательную квадратичную формулу: n^2+n+41 " +
            "Оказалось, что согласно данной формуле можно получить 40 простых чисел, последовательно подставляя значения " +
            "0≤n≤39. Однако, при n = 40, 40^2+40+41=40(40+1)+41 делится на 41 без остатка, " +
            "и, очевидно, при n=41, 41^2+41+41 делится на 41 без остатка. " +
            "При помощи компьютеров была найдена невероятная формула n^2−79n+1601, согласно которой " +
            "можно получить 80 простых чисел для последовательных значений n от 0 до 79. " +
            "Произведение коэффициентов −79 и 1601 равно −126479. " +
            "Рассмотрим квадратичную формулу вида:" +
            "n^2+an+b, где |a| < 1000 и |b| ≤ 1000,  где |n| является " +
            "модулем (абсолютным значением) n. К примеру, |11|=11 и |−4|=4 Найдите произведение " +
            "коэффициентов a и b квадратичного выражения, согласно которому " +
            "можно получить максимальное количество простых чисел для последовательных " +
            "значений n, начиная со значения n = 0.";

        private int Calculate()
        {
            (int a, int b) maxAB = (a: 1, b: 1);
            int maxLength = 0;
            for (int a = -999; a < 1000; a++)
            {
                for (int b = -1000; b <= 1000; b++)
                {
                    int primeLength = LengthOfPrimeRange(a, b);
                    if (primeLength > maxLength)
                    {
                        maxLength = primeLength;
                        maxAB = (a, b);
                    }
                }
            }
            
            return maxAB.a * maxAB.b;
        }

        private int LengthOfPrimeRange(int a, int b)
        {
            int n = 0;
            while(true)
            {
                int number = n * n + a * n + b;
                if(Helper.IsPrime(number))
                {
                    n++;
                }
                else
                {
                    break;
                }
            }
            return n;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 27,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Произведение коэффициентов a и b.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

