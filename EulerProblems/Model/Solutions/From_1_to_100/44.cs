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
    public class FortyFour
    {
        const string content =
            "Пятиугольные числа вычисляются по формуле: Pn=n(3n−1)/2. Первые десять пятиугольных чисел:" +
            "\n1, 5, 12, 22, 35, 51, 70, 92, 117, 145, ..." +
            "\nМожно убедиться в том, что P4 + P7 = 22 + 70 = 92 = P8. Однако, их разность, 70 − 22 = 48, не является пятиугольным числом. Найдите пару пятиугольных чисел Pj и Pk, для которых сумма и разность являются пятиугольными числами и значение D = |Pk − Pj| минимально, и дайте значение D в качестве ответа.";

        

        // если double без остатка делится на единицу - то число является целым
        private bool IsInteger(double number) => number % 1 == 0;

        // решив уравнение Pn=n(3n−1)/2 относительно n получим 3n^2-n-2*Pn=0
        // Pn является пятиугольным числом только в том случае, если уравнение имеет
        // хотя бы один положительный корень
        private bool IsNumberPentagon(long number)
        {
            // считаем дискрименант для квадратного уравнения 3x^2 - x - 2*number = 0
            var disc = 1 + 4 * 3 * (2 * number);

            // нам нужны только реальные числа
            if(disc > 0)
            {
                // если квадрат дискрименанта не является целым - мы не получим целое число как корень
                var sqrOfDisc = Math.Sqrt(disc);
                if (IsInteger(sqrOfDisc))
                {
                    var x1 = (1 - sqrOfDisc) / 6;
                    var x2 = (1 + sqrOfDisc) / 6;
                    // ищем целый положительный корень
                    if((x1 > 0 && IsInteger(x1)) || (x2 > 0 && IsInteger(x2)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private int Calculate()
        {
            // обходим последовательность в два цикла
            // number, counter A - добавляет сверху в последовательность новые пятиугольные числа
            // number, counter B - используется для составление суммы и разности со всеми пятиугольными
            // числами меньше numberA
            int counterA = 1;
            while (true)
            {
                int numberA = counterA * (3 * counterA - 1) / 2;
                for (int counterB = 1; counterB < counterA; counterB++)
                {
                    int numberB = counterB * (3 * counterB - 1) / 2;

                    int difference = numberA - numberB;
                    int summ = numberA + numberB;

                    if (IsNumberPentagon(difference) && IsNumberPentagon(summ))
                    {
                        return difference;
                    }
                    
                }
                counterA++;
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
                Number = 44,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите пару пятиугольных чисел Pj и Pk, для которых сумма и разность являются пятиугольными числами и значение D = |Pk − Pj| минимально, и дайте значение D в качестве ответа.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

