using System;
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
    public class FortyOne
    {
        const string content =
            "Будем считать n-значное число пан-цифровым, если каждая из цифр от 1 до n используется в нем ровно один раз. К примеру, 2143 является 4-значным пан-цифровым числом, а также простым числом. Какое существует наибольшее n-значное пан-цифровое простое число?";

        private int Calculate()
        {
            int max = 0;

            int number = 1000;
            int panBase = 4;
            // любое пан-цифровое число по опредленному основнию всегда имеет одинаковую сумму цифр
            // основание 2: 3, 3: 6, 4: 10, 5: 15, 6: 21, 7: 28, 8: 36, 9: 45.
            // из этих основний только 4ое и 7ое не делятся на 3и, а значит 
            // могут быть простыми, а значит будем проверять только их
            while (number <= 7654321)
            {
                if ((panBase == 4 || panBase == 7 ) && Helper.IsPandigital(number, panBase) && Helper.IsPrime(number))
                {
                    max = number;
                }

                if(number.CountDigits() > panBase)
                {
                    panBase++;
                }

                number++;
            }

            return max;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 41,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какое существует наибольшее n-значное пан-цифровое простое число?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench, 1, 1)
            });

            return pc;
        }
    }
}

