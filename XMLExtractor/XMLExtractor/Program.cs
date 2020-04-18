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
                Console.WriteLine("Incorrect input Directory value");
                return;
            }

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
