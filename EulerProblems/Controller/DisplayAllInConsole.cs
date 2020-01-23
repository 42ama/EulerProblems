using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using EulerProblems.Model.Solutions.From_1_to_100;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;

namespace EulerProblems.Controller
{
    public static class DisplayAllInConsole
    {
        static HashSet<char> ignoreInWordWrap = new HashSet<char> { ' ', '.', ',', '-', '+', '=', ':', '?', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        public static void Display()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            WatchstopBenchmark.SetIgnoreConsts(false);

            // интерфейс IProblem удостоверяется в том что данные типы обладают методом CompleteProblem()
            List<ProblemContainer> problems = new List<ProblemContainer>();
            var problemsTypes = ProblemHandling.GetProblemTypes();

            // тестируем последную добавленную проблему (ту над которой ведется работа)
            var lastProblem = ProblemHandling.RunSingle(problemsTypes.Last());
            DisplayProblem(lastProblem);

            Console.WriteLine("\nНажмите клавишу, чтобы произвести решение остальных проблем начиная с первой...");
            Console.ReadKey();

            foreach (var problemType in problemsTypes)
            {
                var problem = ProblemHandling.RunSingle(problemType);
                DisplayProblem(problem);
            }

            Console.WriteLine("Готово.");

            Console.ReadKey();
        }

        private static void DisplayProblem(ProblemContainer problem)
        {
            if (problem == null)
            {
                return;
            }
            Console.WriteLine($"\t\t\t\tЭйлер №{problem.Number}:");
            int startSybm = 0;
            int lineWidthPref = 90;
            int lineWidthActual = problem.Task.Length > startSybm + lineWidthPref ? lineWidthPref : problem.Task.Length - startSybm;
            while (true)
            {
                Console.Write(problem.Task.Substring(startSybm, lineWidthActual));
                if (ignoreInWordWrap.Contains(problem.Task[startSybm + lineWidthActual - 1]))
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("-");
                }
                if (lineWidthActual != lineWidthPref)
                {
                    break;
                }
                startSybm += lineWidthActual;
                lineWidthActual = problem.Task.Length > startSybm + lineWidthPref ? lineWidthPref : problem.Task.Length - startSybm;
            }
            int counter = 1;
            foreach (var problemCase in problem.Cases)
            {
                Console.WriteLine($"Случай №{counter}");
                Console.WriteLine($"\t{problemCase.Task}");
                Console.WriteLine($"\tРезультат: {problemCase.Result}");

                if (problemCase.BenchmarkResult != null)
                {
                    double avarageMsRounded = Math.Round(problemCase.BenchmarkResult.AvarageMs, 2);
                    if (avarageMsRounded > 0 || problemCase.BenchmarkResult.MedianMs > 0)
                    {
                        double avarageSecRounded = Math.Round(problemCase.BenchmarkResult.AvarageMs / 100.0, 2);
                        double medianSecRounded = Math.Round(problemCase.BenchmarkResult.MedianMs / 100.0, 2);

                        if (avarageSecRounded > 0 || medianSecRounded > 0)
                        {
                            Console.WriteLine($"\tСреднее сек: {avarageSecRounded}; Медианное сек: {Math.Round(medianSecRounded, 2)}");
                        }

                        Console.WriteLine($"\tСреднее мс: {avarageMsRounded}; Медианное мс: {problemCase.BenchmarkResult.MedianMs}");
                    }

                    Console.WriteLine($"\tСреднее тики: {Math.Round(problemCase.BenchmarkResult.AvarageTicks, 2)}; Медианное тики: {problemCase.BenchmarkResult.MedianTicks}");
                }

                counter++;
            }
            Console.Write("\n\n");
        }

    }
}
