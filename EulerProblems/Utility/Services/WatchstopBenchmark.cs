using System;
using System.Linq;
using System.Diagnostics;
using EulerProject.Utility.Helpers;
using EulerProject.Utility.DataContainers;

namespace EulerProject.Utility.Services
{
    public static class WatchstopBenchmark
    {
        const int heatUpTimesConst = 10;
        const int testTimesConst = 100;

        public static WatchstopBenchmarkResult Benchmark(Action action, int heatUpTimes = heatUpTimesConst, int testTimes = testTimesConst)
        {
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
