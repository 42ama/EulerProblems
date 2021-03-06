﻿using System;
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
    public class TwentyFive
    {
        const string content =
            "Последовательность Фибоначчи определяется рекурсивным правилом: " +
            "Fn = Fn−1 + Fn−2, где F1 = 1 и F2 = 1. " +
            "Таким образом, первые 12 членов последовательности равны: " +
            "F1 = 1 F2 = 1 F3 = 2 F4 = 3 F5 = 5 F6 = 8 F7 = 13 F8 = 21 " +
            "F9 = 34 F10 = 55 F11 = 89 F12 = 144 Двенадцатый член F12 - первый член последовательности, который содержит три цифры. Каков порядковый номер первого члена последовательности Фибоначчи, содержащего 1000 цифр?";

        private long Calculate(int numberLength)
        {
            BigInteger firstB = 1;
            BigInteger secondB = 1;
            long count = 2;
            while(secondB.ToString().Length < numberLength)
            {
                BigInteger tempB = secondB;
                secondB += firstB;
                firstB = tempB;
                count++;
            }
            return count;
        }

        private void Calculate3()
        {
            Calculate(3);
        }

        private void Calculate1000()
        {
            Calculate(1000);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 25,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова порядковый номер первого члена последовательности Фибоначчи, содержащего 3 цифры.",
                Result = Calculate(3).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate3)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова порядковый номер первого члена последовательности Фибоначчи, содержащего 1000 цифр.",
                Result = Calculate(1000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1000)
            });

            return pc;
        }
    }
}

