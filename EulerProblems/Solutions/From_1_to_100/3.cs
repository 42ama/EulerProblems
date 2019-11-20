using System;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Three : IProblem
    {
        const string content =
            "Простые делители числа 13195 - это 5, 7, 13 и 29." +
            "Каков самый большой делитель числа 600851475143, являющийся простым числом?";

        private long Calculate(long length)
        {
            long newnumm = length;
            long largestFact = 0;

            int counter = 2;
            while (counter * counter <= newnumm)
            {
                if (newnumm % counter == 0)
                {
                    newnumm /= counter;
                    largestFact = counter;
                }
                else
                {
                    counter++;
                }
            }
            if (newnumm > largestFact)
            {
                largestFact = newnumm;
            }
            return largestFact;
        }
        
        private void Calculate13195()
        {
            Calculate(13195);
        }
        private void Calculate600851475143()
        {
            Calculate(600851475143);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 3,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Самый большой простой делитель числа 13195.",
                Result = Calculate(13195).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate13195)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Самый большой простой делитель числа 600851475143.",
                Result = Calculate(600851475143).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate600851475143)
            });


            return pc;
        }
    }

    static class ThreeHeadOn
    {
        const string content =
            "Простые делители числа 13195 - это 5, 7, 13 и 29." +
            "Каков самый большой делитель числа 600851475143, являющийся простым числом?";

        public static long Calculate(long length = 600851475143)
        {
            long maxi = 1;
            for (long i = 2; i < length; i++)
            {
                if (length % i == 0 && IsPrime(i))
                {
                    maxi = i;
                }
            }
            return maxi;
        }

        private static bool IsPrime(long number)
        {
            for (long i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
