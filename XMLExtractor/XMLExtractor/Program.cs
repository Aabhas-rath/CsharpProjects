using OfficeOpenXml;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using Logics.Configuration;
using Logics;

namespace XMLExtractor
{
    class Program
    {
        private static DirectoryInfo directoryInfo = null;

        private static bool PathError(string ArgumentName, out string argument)
        {
            Console.WriteLine($"Incorrect input Directory value : {ArgumentName}");
            Console.WriteLine("Plese Input a value:");
            argument = Console.ReadLine();
            if (!(Directory.Exists(argument) || File.Exists(argument)))
            {
                return PathError(ArgumentName, out argument);
            }
            else
                return true;
        }


        static void Main(string[] args)
        {
            string from = ConfigurationManager.AppSettings["From"];
            string to = ConfigurationManager.AppSettings["To"];
            string customWorkSheetName = ConfigurationManager.AppSettings["CustomWorkSheetName"];
            Configurations config = new Configurations();
            if (Directory.Exists(from))
            {
                directoryInfo = new DirectoryInfo(from);
            }
            else
            {
                PathError("Xml Files Folder",out from);
            }

            if (Directory.Exists(to))
            {
                var ExcelFolder = new DirectoryInfo(to);

                Console.WriteLine($"Excel files are present in {to}");
                Console.WriteLine("Press Y to Delete all. Press N to cancel and return");
                var input = Console.ReadKey(false);
                if (input.Key == ConsoleKey.Y)
                {
                    foreach (FileInfo file in ExcelFolder.EnumerateFiles())
                    {
                        file.Delete();
                    }
                }
                else if(input.Key == ConsoleKey.N)
                {
                    return;
                }
            }
            else
                PathError("Excel File creation folder", out to);


            foreach (var file in directoryInfo.EnumerateFiles())
            {
                XmlExtractor xmlExtractor = new XmlExtractor(file, new Configurations());
                var identifiers = xmlExtractor.Extract();
                
                Console.WriteLine($"{identifiers.Count} set of Identifiers Extracted : ");
                identifiers.Values.ToList().ForEach(l => { Console.WriteLine(l.Count); });

                var ExcelName = Path.Combine(to, Path.GetFileNameWithoutExtension(file.Name) + ".xlsx");

                Console.WriteLine($"writing to Excel {ExcelName} at {to} Folder");
                
                ExcelCreator excelCreator = new ExcelCreator(ExcelName, customWorkSheetName, identifiers);
                excelCreator.Perform();
                
                Console.WriteLine("Excel Created");
            }

            Console.WriteLine("");
            Console.WriteLine("press any key to terminate");
            Console.ReadKey();
        }
    }
}
