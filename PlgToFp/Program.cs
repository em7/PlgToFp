using FlpToFp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FlpToFp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || ArgIsHelp(args[0]))
            {
                PrintHelp();
                return;
            }

            foreach (var f in args)
            {
                try
                {
                    var converter = new PlgToFpConverter();
                    var plgDoc = XDocument.Load(f);
                    var fpDoc = converter.Convert(plgDoc);
                    fpDoc.Save(GetNewName(f));
                }
                catch (Exception ex)
                {
                    if (ex is IOException || ex is UriFormatException)
                    {
                        var msg = Resources.strings.CantReadFileIoException;
                        Console.WriteLine(string.Format(msg, f));
                    }
                    else if (ex is FlightPlanConvertException)
                    {
                        var msg = Resources.strings.CantReadFileFormatException;
                        Console.WriteLine(string.Format(msg, f));
                    }
                    else
                    {
                        var msg = Resources.strings.CantReadFileException;
                        Console.WriteLine(string.Format(msg, f));
                    }

                    Console.ReadKey();
                }
            }


            

        }

        private static string GetNewName(string plgName)
        {
            return Path.ChangeExtension(plgName, "fp");
        }

        /// <summary>
        /// Return true if argument is "help"
        /// </summary>
        /// <returns></returns>
        private static bool ArgIsHelp(string arg)
        {
            return arg == "-h" || arg == "/h" || arg == "--help"
                || arg == "/?" || arg == "-?"
                || arg == "-v" || arg == "--version";
        }

        /// <summary>
        /// Prints the help
        /// </summary>
        private static void PrintHelp()
        {
            var help = Resources.strings.Help;
            var pressKey = Resources.strings.PressAnyKey;

            var version = Assembly.GetAssembly(typeof(Program)).GetName().Version;
            var versionStr = version.Major + "." + version.Minor;

            var helpStr = string.Format(help, versionStr);

            Console.WriteLine(helpStr);
            Console.WriteLine();
            Console.WriteLine(pressKey);

            Console.ReadKey();
        }
    }
}
