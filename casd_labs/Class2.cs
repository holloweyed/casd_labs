using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Request : IComparable<Request>
    {
        private int priority;
        private int number;
        private int step;
        private Stopwatch stopwatch;

        public int Priority => priority;
        public int Number => number;
        public int Step => step;
        public Stopwatch Stopwatch => stopwatch;

        public Request(int priority, int number, int step)
        {
            this.priority = priority;
            this.number = number;
            this.step = step;
            stopwatch = Stopwatch.StartNew();
        }

        ~Request()
        {
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        public int CompareTo(Request other)
        {
            if (other == null)
                return 1;
            return this.priority.CompareTo(other.priority);
        }
    }
}