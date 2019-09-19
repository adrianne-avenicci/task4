using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Times.MaxOverlap(GetValues(Console.ReadLine())).Start);
            Console.WriteLine(Times.MaxOverlap(GetValues(Console.ReadLine())).End);



            Console.ReadLine();
           // switch (t1,t2);



        }

        static public Times[] GetValues(String path)
        {
            String[] lines = System.IO.File.ReadAllLines(path);
            Times[] times = new Times[lines.Length];
            String pattern = @"(\d+[:\d+]*)\s+(\d+[:\d+]*)";

            int counter = 0;

            foreach (string line in lines)
            {
                //Console.WriteLine(line);
                MatchCollection mc = Regex.Matches(line, pattern);
                foreach (Match m in mc)
                {
                    times[counter] = new Times
                    {
                        Start = DateTime.Parse(m.Groups[1].Value),
                        End = DateTime.Parse(m.Groups[2].Value)
                    };
                }
                counter++;
            }
            /*
            for (int i = 0; i < times.Length; i++)
            {
                Console.Write(times[i].Start + " ");
                Console.WriteLine(times[i].End);
            }
            */
            //Console.WriteLine(Times.GetOverlapping(times[0], times[1]).Start);
            //Console.WriteLine(Times.GetOverlapping(times[0], times[1]).End);

            return times;
        }
    }


    public class Times
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Contains(DateTime time)
        {
            return time >= Start && time <= End;
        }

        static public bool IsOverlapping(Times T1, Times T2)
        {
            return T1.Contains(T2.Start) || T1.Contains(T2.End) || T2.Contains(T1.Start) || T2.Contains(T1.End);
        }

        static public Times GetOverlapping(Times T1, Times T2)
        {
            Times time_intersect = new Times();

            if (T1.Contains(T2.Start) && T2.Contains(T1.End))
            {
                time_intersect.Start = T2.Start;
                time_intersect.End = T1.End;
            }
            else if (T2.Contains(T1.Start) && T1.Contains(T2.End))
            {
                time_intersect.Start = T1.Start;
                time_intersect.End = T2.End;
            }
            else if (T1.Contains(T2.Start) && T1.Contains(T2.End))
            {
                time_intersect.Start = T2.Start;
                time_intersect.End = T2.End;
            }
            else if (T2.Contains(T1.Start) && T2.Contains(T1.End))
            {
                time_intersect.Start = T1.Start;
                time_intersect.End = T1.End;
            }
            else time_intersect = T1;
            return time_intersect;
        }

        static public Times MaxOverlap(Times[] T0)
        {
            Times time_0 = T0[0];
            int i = 1;
            do
            {
                time_0 = GetOverlapping(time_0, T0[i]);
                i++;
            }
            while (i<T0.Length);

            return time_0;
        }
    }
}
