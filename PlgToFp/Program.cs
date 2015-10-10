using FlpToFp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FlpToFp
{
    class Program
    {
        static void Main(string[] args)
        {

            var files = new List<String> { @"c:\Users\eMko\Desktop\X-Plane 10\Output\FMS plans\VFR Mosnov to Turany.plg" };
            foreach (var f in files)
            {
                var converter = new PlgToFpConverter();
                var plgDoc = XDocument.Load(f);
                var fpDoc = converter.Convert(plgDoc);
                fpDoc.Save(GetNewName(f));
            }

            Console.ReadKey();
        }

        private static string GetNewName(string plgName)
        {
            return Path.ChangeExtension(plgName, "fp");
        }
    }
}
