using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTime
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan t1 = System.DateTime.Now.TimeOfDay;
            TimeSpan t2 = new TimeSpan(0,70,0);
            System.Console.WriteLine(t1 +"  + 1h = "+t1.Add(t2) );
            System.Console.ReadKey();
        }
    }
}
