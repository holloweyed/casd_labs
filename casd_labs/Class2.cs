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
        private Stopwatch time;
        private int priority;
        private int number;
        private int step;

        public Stopwatch Stopwatch => time;
        public int Priority => priority;
        public int Number => number;
        public int Step => step;

        public Request(int priority, int number, int step)
        {
            time = Stopwatch.StartNew();
            this.priority = priority;
            this.number = number;
            this.step = step;
        }

        ~Request()
        {
            time.Stop();
            Console.WriteLine(time.ElapsedMilliseconds);
        }

        public int CompareTo(Request other)
        {
            if (other == null) return 1;
            return this.priority.CompareTo(other.priority);
        }
    }
}