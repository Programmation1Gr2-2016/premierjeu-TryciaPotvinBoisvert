using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Double nombre = 0;

            Console.ReadLine();

            if ((nombre / 2) * 2 != nombre)
            {
                Console.WriteLine("impair");
            }
            else
            {
                Console.WriteLine("pair");
            }
            Console.ReadLine();
        }
    }
}
