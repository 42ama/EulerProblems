using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;
using EulerProblems.Utility.Extensions;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class TwentyEight
    {
        const string content =
            "Начиная с числа 1 и двигаясь дальше вправо по часовой стрелке, образуется следующая спираль 5 на 5:\n" +
            "21 22 23 24 25\n" +
            "20  7  8  9 10\n" +
            "19  6  1  2 11\n" +
            "18  5  4  3 12\n" +
            "17 16 15 14 13\n" +
            "Можно убедиться, что сумма чисел в диагоналях равна 101." +
            "Какова сумма чисел в диагоналях спирали 1001 на 1001, образованной таким же способом?";

        private int Calculate(int dimensionSide)
        {

            if(dimensionSide % 2 != 1)
            {
                throw new ArgumentOutOfRangeException("dimesionSide", "Данный способ образует только спирали со стороной имеющий величину нечетного числа (3 на 3, 5 на 5 и т.д.).");
            }
            if (dimensionSide == 1)
            {
                return 1;
            }
            if(dimensionSide < 3)
            {
                throw new ArgumentOutOfRangeException("dimesionSide", "Миниальная сторона спирали 1 на 1");
            }


            var diagonal = new HashSet<int> { 1 };

            // сторона предыдущего витка
            int lastSide = 1;

            // на каждом витке спирали мы пропускаем skipStep чисел между числами которые находятся на диагонали
            int skipStep = 1;
            // если значение данной переменной меньше чем skipStep, то 
            // пропускаем число, иначе считаем что оно находится на диагонали
            int currentSkipStep = 0;
            // поправка на пропуск растущая с каждым витком
            int startingStep = 0;


            // количество чисел которые необходимо пройти перед сменой витка
            // считаем по формуле 4*a+4 упрощенной от (a+2)^2-a^2
            int numbersToNextCoil = 4 * lastSide + 4;
            // считаем количество чисел в витке для смены витков
            int currentNumberTNC = 0;
            

            for (int i = 2; i <= dimensionSide * dimensionSide; i++)
            {

                if (currentNumberTNC == numbersToNextCoil)
                {
                    // при смене витка меняем параметры системы
                    skipStep += 2;
                    lastSide += 2;

                    numbersToNextCoil = 4 * lastSide + 4;

                    // устанавливаем счетчики в исходное положение
                    currentSkipStep = startingStep;
                    currentNumberTNC = 0;

                    // увеличиваем поправку
                    startingStep++;
                }
                else
                {
                    currentNumberTNC++;
                }

                // проверяем находится ли число на диагонали
                if (currentSkipStep != skipStep)
                {
                    currentSkipStep++;
                    continue;
                }
                else
                {
                    currentSkipStep = 0;
                    diagonal.Add(i);
                }
            }

            return diagonal.Sum();
        }

        private void Calculate5()
        {
            Calculate(5);
        }

        private void Calculate1001()
        {
            Calculate(1001);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 28,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма чисел в диагоналях спирали 5 на 5, образованной таким же способом.",
                Result = Calculate(5).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate5)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма чисел в диагоналях спирали 1001 на 1001, образованной таким же способом.",
                Result = Calculate(1001).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(Calculate1001)
            });

            return pc;
        }
    }
}

