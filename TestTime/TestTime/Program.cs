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
            //TimeSpan t1 = System.DateTime.Now.TimeOfDay;
            //TimeSpan t2 = System.DateTime.Now.TimeOfDay.Subtract(new TimeSpan(0, 10, 0));
            //TimeSpan t3 = t1.Subtract(t2);
            //if (t3.Minutes > 5)
            //{
            //    System.Console.WriteLine(t1 + " - " + t2 + " = " + t3);
            //}
            int min = 7;
            string message = "false";
            bool b = (7 < 5) || message.Contains("false");
            if(b)
            {
                System.Console.WriteLine("nei zaatrzyma sie ");
            }
            else
            {
                System.Console.WriteLine("zaatrzyma sie ");
            }
            
            System.Console.ReadKey();
        }
    }
}
