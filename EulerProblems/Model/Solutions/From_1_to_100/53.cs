using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;
using System.Text;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class FiftyThree
    {
        const string content =
            "Существует ровно десять способов выбора 3 элементов из множества пяти {1, 2, 3, 4, 5}:" +
            "\n123, 124, 125, 134, 135, 145, 234, 235, 245, и 345" +
            "\nВ комбинаторике для этого используется обозначение 5C3 = 10." +
            "В общем случае," +
            "\n nCr = n!/(r!(n−r)!), где r ≤ n, n! = n×(n−1)×...×3×2×1 и 0! = 1." +
            "Это значение превышает один миллион, начиная с n = 23: 23C10 = 1144066." +
            "Cколько значений(не обязательно различных)  nCr для 1 ≤ n ≤ 100 больше одного миллиона?";


        private int Calculate()
        {
            int times = 0;
            BigInteger oneMillion = new BigInteger(1000000);
            // проверим все значения с n = 23..100
            for (int n = 23; n <= 100; n++)
            {
                // так как сочетания после n/2 равняются сочетаниями до n/2, будем
                // расчитывать их только до n/2
                for (int r = 1; r <= n/2; r++)
                {
                    if(Combinations(n, r) > oneMillion)
                    {
                        // формула для рассчета удвоенного количества сочетаний от r до n/2
                        times += n - 2*r + 1;
                        break;
                    }
                }
            }
            
            return times;
        }

        /// <summary>
        /// Возвращает количество сочетаний для указанных <c>n</c> и <c>r</c>
        /// </summary>
        /// <param name="n">Общее количество элементов в множестве</param>
        /// <param name="r">Количество элементов, которые выбираются</param>
        /// <returns>Количество сочетаний для указанных <c>n</c> и <c>r</c></returns>
        private BigInteger Combinations(int n, int r)
        {
            return Helper.Factorial(n) / (Helper.Factorial(r) * Helper.Factorial(n - r));
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 53,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Cколько значений(не обязательно различных)  nCr для 1 ≤ n ≤ 100 больше одного миллиона?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

