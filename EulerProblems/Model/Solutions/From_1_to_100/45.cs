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
    public class FortyFive
    {
        const string content =
            "Треугольные, пятиугольные и шестиугольные числа вычисляются по нижеследующим формулам:" +
            "\nТреугольные Tn = n(n + 1) / 2; 1, 3, 6, 10, 15, ..." +
            "\nПятиугольные Pn = n(3n−1) / 2; 1, 5, 12, 22, 35, ..." +
            "\nШестиугольные Hn = n(2n−1); 1, 6, 15, 28, 45, ..." +
            "\nМожно убедиться в том, что T285 = P165 = H143 = 40755. " +
            "Найдите следующее треугольное число, являющееся также пятиугольным и шестиугольным.";

        
        

        // тре-, пяти- и шести- угольные числа проверяется по общему приниципу:
        // квадртаное уравнение решеное относительно номера числа должно давать
        // хотя бы один положительный целый корень
        // используя это свойство модифицируем метод из прошлой проблемы
        private bool IsNumberNgon(int a, int b, long c)
        {
            // если double без остатка делится на единицу - то число является целым
            bool IsInteger(double number) => number % 1 == 0;

            // считаем дискрименант для квадратного уравнения 3x^2 - x - 2*number = 0
            var disc = b*b - 4 * a * c;

            // нам нужны только реальные числа
            if (disc > 0)
            {
                // если квадрат дискрименанта не является целым - мы не получим целое число как корень
                var sqrOfDisc = Math.Sqrt(disc);
                if (IsInteger(sqrOfDisc))
                {
                    var x1 = (-b - sqrOfDisc) / (2 * a);
                    var x2 = (-b + sqrOfDisc) / (2 * a);
                    // ищем целый положительный корень
                    if ((x1 > 0 && IsInteger(x1)) || (x2 > 0 && IsInteger(x2)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private long Calculate()
        {
            // будем проверять все 6и угольные числа, так как числа
            // по этой формуле имеет наибольшую скорость роста

            // решив уравнения относительно n получим следующие коэфиценты а и b
            // для каждого из них
            var triCoef = (a: 1, b: 1);
            var pentaCoef = (a: 3, b: -1);
            var hexaCoef = (a: 2, b: -1);
            // по условию начинаем с 144ой позиции
            int counter = 144; 
            while(true)
            {
                long number = counter * (2 * counter - 1);
                if(IsNumberNgon(triCoef.a, triCoef.b, -2*number) &&
                    IsNumberNgon(pentaCoef.a, pentaCoef.b, -2*number) &&
                    IsNumberNgon(hexaCoef.a, hexaCoef.b,-number))
                {
                    return number;
                }
                counter++;
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
                Number = 45,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Найдите следующее треугольное число, являющееся также пятиугольным и шестиугольным.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

