using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class FortyTwo
    {
        const string content =
            "n-ый член последовательности треугольных чисел задается как tn = 1/2 * n * (n+1). Таким образом, первые десять треугольных чисел:" +
            "\n1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ..." +
            "\nПреобразовывая каждую букву в число, соответствующее ее порядковому номеру в алфавите, и складывая эти значения, мы получим числовое значение слова. Для примера, числовое значение слова SKY равно 19 + 11 + 25 = 55 = t_10.Если числовое значение слова является треугольным числом, то мы назовем это слово треугольным словом. Используя p042_words.txt определите, сколько в нем треугольных слов.";

        static IDictionary<char, int> alphabets = new Dictionary<char, int>
        {
            {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8}, {'I', 9},
            {'J', 10}, {'K', 11}, {'L', 12}, {'M', 13}, {'N', 14}, {'O', 15}, {'P', 16}, {'Q', 17}, {'R', 18},
            {'S', 19}, {'T', 20}, {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24}, {'Y', 25}, {'Z', 26}
        };

        static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Model\Resources\p042_words.txt");

        private string[] Parse(string pathToFile)
        {
            var notParsedText = File.ReadAllText(pathToFile);
            var s = notParsedText.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = s[i].Substring(1, (s[i].Length - 2));
            }
            return s;
        }

        private int Calculate()
        {
            // формируем список треугольных чисел
            var triangles = new HashSet<int>();
            for (int i = 1; i <= 100; i++)
            {
                triangles.Add((int)(0.5*i*(i+1)));
            }

            var lines = Parse(path);

            int counter = 0;
            // проверяем каждое слово
            foreach (var line in lines)
            {
                int number = 0;

                // считаем числовое значение
                foreach (var chara in line)
                {
                    number += alphabets[chara];
                }

                // проверяем является ли значение - треугольным
                if(triangles.Contains(number))
                {
                    counter++;
                }
            }

            return counter;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 42,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Определите, сколько в документе треугольных слов.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

