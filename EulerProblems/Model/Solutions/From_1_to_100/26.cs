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
    public class TwentySix
    {
        const string content =
            "Единичная дробь имеет 1 в числителе. Десятичные представления единичных дробей со знаменателями от 2 до 10 даны ниже: 1/2 = 0.5 1/3 = 0.(3) 1/4 = 0.25 1/5 = 0.2 1/6 = 0.1(6) 1/7 = 0.(142857) 1/8 = 0.125 1/9 = 0.(1) 1/10 = 0.1 Где 0.1(6) значит 0.166666..., и имеет повторяющуюся последовательность из одной цифры. Заметим, что 1/7 имеет повторяющуюся последовательность из 6 цифр. Найдите значение d < 1000, для которого 1/d в десятичном виде содержит самую длинную повторяющуюся последовательность цифр.";

        private int Calculate(int upperBound)
        {
            int maxPosition = 0;
            int maxLength = 0;

            for (int i = 2; i < upperBound; i++)
            {
                // первое число всегда один, так как остаток от деления 1%i = 1
                HashSet<int> foundRemainders = new HashSet<int> { 1 };
                int value = 10 % i;

                // поочередно находим цифры в дробной части, собирая их в foundRemainders
                while (!foundRemainders.Contains(value))
                {
                    foundRemainders.Add(value);
                    value *= 10;
                    value %= i;
                }
                

                if (foundRemainders.Count > maxLength)
                {
                    maxPosition = i;
                    maxLength = foundRemainders.Count;
                }
            }

            return maxPosition;
        }

        private void Calculate11()
        {
            Calculate(11);
        }

        private void Calculate1000()
        {
            Calculate(1000);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 26,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Значение d < 11, для которого 1/d в десятичном виде содержит самую длинную повторяющуюся последовательность цифр.",
                Result = Calculate(11).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate11)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Значение d < 1000, для которого 1/d в десятичном виде содержит самую длинную повторяющуюся последовательность цифр.",
                Result = Calculate(1000).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1000)
            });

            return pc;
        }
    }
}

