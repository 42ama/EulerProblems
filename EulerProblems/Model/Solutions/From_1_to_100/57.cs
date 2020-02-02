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
    public class FiftySeven
    {
        const string content =
            "Можно убедиться в том, что квадратный корень из двух можно выразить в виде бесконечно длинной дроби." +
            "\n√ 2 = 1 + 1/(2 + 1/(2 + 1/(2 + ... ))) = 1.414213..." +
            "\nПриблизив это выражение для первых четырех итераций, получим:" +
            "\n1 + 1/2 = 3/2 = 1.5" +
            "\n1 + 1/(2 + 1/2) = 7/5 = 1.4" +
            "\n1 + 1/(2 + 1/(2 + 1/2)) = 17/12 = 1.41666..." +
            "\n1 + 1/(2 + 1/(2 + 1/(2 + 1/2))) = 41/29 = 1.41379..." +
            "\nСледующие три приближения: 99/70, 239/169 и 577/408, а восьмое приближение, 1393/985, является первым случаем, в котором количество цифр в числителе превышает количество цифр в знаменателе. " +
            "У скольких дробей длина числителя больше длины знаменателя в первой тысяче приближений?";

        private int Calculate()
        {
            var count = 0;

            // числитель и знаменатель последнего второго слагаемого
            var numLast = new BigInteger(1);
            var denumLast = new BigInteger(2);
            for (int i = 2; i <= 1000; i++)
            {
                // новые знаменатель и числитель очень просто рассчитать, если знать
                // значение последнего рассчитанного 
                // В данной прогрессии каждый раз меняется только второе слагаемое, назавем его term2 
                // И второе слагаемое в знаменателе term2 равняется term2 из предыдущей итерации
                // (первое слагаемое - 2, числитель - 1)

                // числитель и знаменатель текущего второго слагаемого
                var num = denumLast;
                var denum = 2*denumLast + numLast;

                numLast = num;
                denumLast = denum;

                // числитель и знаменатель всего числа
                var numFull = num + denum;
                var denumFull = denum;

                if(numFull.Length() > denumFull.Length())
                {
                    count++;
                }
            }
            return count;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 57,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "У скольких дробей длина числителя больше длины знаменателя в первой тысяче приближений?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

