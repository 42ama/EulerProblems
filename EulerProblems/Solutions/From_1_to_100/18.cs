using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class Eighteen
    {
        const string content =
            @"Начиная в вершине треугольника (см. пример ниже) и перемещаясь вниз на смежные числа найдите максимальную сумму пути от вершины до основания следующего треугольника: /\ " +
            "Примечание: Так как в данном треугольнике всего 16384 возможных маршрута от вершины до " +
            "основания, эту задачу можно решить проверяя каждый из маршрутов. Однако похожая Задача 67 " +
            "с треугольником, состоящим из сотни строк, не решается перебором(brute force) и требует " +
            "более умного подхода! ;o)";

        const string triangle =
            "75 95 64 17 47 82 18 35 87 10 20 04 82 47 65 19 01 23 75 03 34 88 02 77 " +
            "73 07 63 67 99 65 04 28 06 16 70 92 41 41 26 56 83 40 80 70 33 41 48 72 " +
            "33 47 32 37 16 94 29 53 71 44 65 25 43 91 52 97 51 14 70 11 33 28 77 73 " +
            "17 78 39 68 17 57 91 71 52 38 17 14 91 43 58 50 27 29 48 63 66 04 68 89 " +
            "53 67 30 73 16 69 87 40 31 04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";

        public class NodeInTriangle
        {
            public int Id { get; set; }
            public int Value { get; set; }
            public int ChildId1 { get; set; }
            public int ChildId2 { get; set; }
            public int Row { get; set; }
        }
        public HashSet<NodeInTriangle> Parse(string triangle)
        {
            string[] arr = triangle.Split(' ');
            int row = 0;
            int reachCountToUpRow = 1;
            int countToUpRow = 0;
            var nodes = new HashSet<NodeInTriangle>();
            for (int i = 0; i < arr.Length; i++)
            {
                nodes.Add( new NodeInTriangle 
                { 
                    Id = i,
                    Value = Int32.Parse(arr[i]),
                    ChildId1 = i + row + 1,
                    ChildId2 = i + row + 2,
                    Row = row
                });
                //Console.Write(arr[i] + " ");
                countToUpRow++;
                if(countToUpRow == reachCountToUpRow)
                {
                    //Console.Write("\n");
                    row++;
                    reachCountToUpRow++;
                    countToUpRow = 0;
                }
            }
            return nodes;
        }


        private int Calculate()
        {
            var nodes = Parse(triangle);
            int maxRow = nodes.Max(i => i.Row);

            // начиная с предпоследнего ряда обходим каждый узел, для каждого узла: 
            // 1) из узлов детей выбираем один с максимальным значением
            // 2) добавляем его значение к значению узла
            // 3) повторяем пока не пройдем весь треугольник
            // в итоге в начальном узле окажется максимальная сумма пути

            for (int i = maxRow - 1; i >= 0; i--)
            {
                foreach(var node in nodes.Where(item => item.Row == i))
                {
                    int child1Value = nodes.Single(item => item.Id == node.ChildId1).Value;
                    int child2Value = nodes.Single(item => item.Id == node.ChildId2).Value;
                    node.Value += Math.Max(child1Value, child2Value);
                }
            }
            return nodes.Single(i => i.Id == 0).Value;
        }

        private void CalculateBench()
        {
            Calculate();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 18,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Максимальная сумма пути по данному треугольнику",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

