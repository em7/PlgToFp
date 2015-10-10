using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlpToFp
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new List<String> { @"c:\Users\eMko\Desktop\X - Plane 10\Output\FMS plans\VFR Mosnov to Turany.plg" };
            foreach (var f in file)
                Console.WriteLine(f);

            Console.ReadKey();
        }
    }
}
