using System;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Five : IProblem
    {
        const string content =
            "2520 - самое маленькое число, которое делится без остатка на все числа от 1 до 10." +
            "Какое самое маленькое число делится нацело на все числа от 1 до 20?";

        private int Calculate(int upperBound)
        {
            Dictionary<int, int> primeNumberAndTimesOrig = new Dictionary<int, int>();

            for (int i = 2; i <= upperBound; i++)
            {
                int counter = 2;
                int newnumm = i;
                Dictionary<int, int> primeNumberAndTimesComparable = new Dictionary<int, int>();
                while (counter <= newnumm)
                {
                    if (newnumm % counter == 0)
                    {
                        newnumm /= counter;
                        if (primeNumberAndTimesComparable.ContainsKey(counter))
                        {
                            primeNumberAndTimesComparable[counter]++;
                        }
                        else
                        {
                            primeNumberAndTimesComparable[counter] = 1;
                        }
                    }
                    else
                    {
                        counter++;
                    }
                }
                foreach (var kvp in primeNumberAndTimesComparable)
                {
                    if (!primeNumberAndTimesOrig.ContainsKey(kvp.Key) || kvp.Value > primeNumberAndTimesOrig[kvp.Key])
                    {
                        primeNumberAndTimesOrig[kvp.Key] = kvp.Value;
                    }
                }
            }
            int result = 1;
            foreach (var kvp in primeNumberAndTimesOrig)
            {
                result *= (int)Math.Pow(kvp.Key, kvp.Value);
            }
            return result;
        }

        private void Calculate10()
        {
            Calculate(10);
        }

        private void Calculate20()
        {
            Calculate(20);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 5,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Самое маленькое число делится нацело на все числа от 1 до 10.",
                Result = Calculate(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate10)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Самое маленькое число делится нацело на все числа от 1 до 20.",
                Result = Calculate(20).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate20)
            });


            return pc;
        }
    }
}
