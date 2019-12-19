using System;
using System.Linq;
using System.Diagnostics;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.DataContainers;

namespace EulerProblems.Model.Utility.Services
{
    public static class WatchstopBenchmark
    {
        const int heatUpTimesConst = 4;
        const int testTimesConst = 10;

        public static bool IgnoreConsts { get; private set; } = false;
        public static void SetIgnoreConsts(bool value)
        {
            IgnoreConsts = value;
        }

        public static WatchstopBenchmarkResult Benchmark(Action action)
        {
            return BenchmarkLogic(action, heatUpTimesConst, testTimesConst);
        }
        public static WatchstopBenchmarkResult Benchmark(Action action, int heatUpTimes, int testTimes)
        {
            return BenchmarkLogic(action, heatUpTimes, testTimes);
        }

        private static WatchstopBenchmarkResult BenchmarkLogic(Action action, int heatUpTimes, int testTimes)
        {
            if(IgnoreConsts)
            {
                heatUpTimes = 10;
                testTimes = 100;
            }
            if (heatUpTimes == 0 || testTimes == 0)
            {
                return null;
            }
            long[] timerMs = new long[testTimes];
            long[] timerTicks = new long[testTimes];
            for (int i = 0; i < heatUpTimes; i++)
            {
                var watch = Stopwatch.StartNew();
                action.Invoke();
                watch.Stop();
            }
            for (int i = 0; i < testTimes; i++)
            {
                var watch = Stopwatch.StartNew();
                action.Invoke();
                watch.Stop();

                timerTicks[i] = watch.ElapsedTicks;
                timerMs[i] = watch.ElapsedMilliseconds;
            }

            var result = new WatchstopBenchmarkResult
            {
                AvarageMs = timerMs.Average(),
                AvarageTicks = timerTicks.Average(),
                MedianMs = Helper.Median(timerMs),
                MedianTicks = Helper.Median(timerTicks)
            };
            return result;
        }
    }
}
