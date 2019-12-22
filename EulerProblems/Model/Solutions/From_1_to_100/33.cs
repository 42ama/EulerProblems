using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class ThirtyThree
    {
        const string content =
            "Дробь 49/98 является любопытной, поскольку неопытный математик, пытаясь сократить ее, будет ошибочно полагать, что 49/98 = 4/8, являющееся истиной, получено вычеркиванием девяток. Дроби вида 30/50 = 3/5 будем считать тривиальными примерами. Существует ровно 4 нетривиальных примера дробей подобного типа, которые меньше единицы и содержат двухзначные числа как в числителе, так и в знаменателе. Пусть произведение этих четырех дробей дано в виде несократимой дроби (числитель и знаменатель дроби не имеют общих сомножителей). Найдите знаменатель этой дроби.";


        private int Calculate()
        {
            int multipliedNumerator = 1;
            int multipliedDenumerator = 1;
            for (int num = 10; num <= 99; num++)
            {
                for (int denum = num+1; denum <= 99; denum++)
                {
                    // после долгих попыток написать элегантный класс, который не добавлял бы лишних 200 строчек
                    // я решил сделать напрямую

                    // А - старший разряд числа, В - младший
                    int numA = num / 10;
                    int numB = num % 10;

                    int denumA = denum / 10;
                    int denumB = denum % 10;

                    var fractionA = (double)num / denum;
                    double fractionB = -1;

                    if (numA == denumA && numA != 0)
                    {
                        fractionB = (double)numB / denumB;
                    }
                    else if(numB == denumA && numB != 0)
                    {
                        fractionB = (double)numA / denumB;
                    }
                    else if (numA == denumB && numA != 0)
                    {
                        fractionB = (double)numB / denumA;
                    }
                    else if (numB == denumB && numB != 0)
                    {
                        fractionB = (double)numA / denumA;
                    }

                    if(fractionB != -1 && fractionA == fractionB)
                    {
                        multipliedNumerator *= num;
                        multipliedDenumerator *= denum;
                    }
                }
            }

            return multipliedDenumerator / Helper.GreatestCommonDivisor(multipliedNumerator, multipliedDenumerator);
        }

        private void CalculateBench()
        {
            Calculate();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 33,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Знаменатель такой дроби.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

