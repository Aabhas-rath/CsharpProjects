using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public class ExcelCreator : IComponent
    {
        private readonly string filename;
        private readonly string customWorkSheetName;
        private Dictionary<string, List<string>> identifiers;

        public ExcelCreator(string filename, string customWorkSheetName, Dictionary<string, List<string>> identifiers)
        {
            this.filename = filename;
            this.customWorkSheetName = customWorkSheetName;
            this.identifiers = identifiers;
        }

        private void ReadIdentifiersAndWriteToExcel(string filename, string customWorkSheetName, Dictionary<string, List<string>> identifiers)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var ExcelFile = new ExcelPackage(new FileInfo(filename)))
            {
                var worksheet = ExcelFile.Workbook.Worksheets.Add(customWorkSheetName);
                int i = 1;
                foreach (var pair in identifiers)
                {
                    int j = 1;
                    foreach (var identifier in pair.Value)
                    {
                        if (j > 10)
                        {
                            i++;
                            j = 1;
                        }
                        worksheet.Column(j).Width = 25;
                        worksheet.Column(j + 1).Width = 5;
                        worksheet.Cells[i, j].Style.WrapText = true;
                        worksheet.Cells[i, j].Style.ShrinkToFit = true;
                        worksheet.Cells[i, j].Value = identifier;
                        j += 2;
                    }
                    i += 4;
                }

                ExcelFile.Workbook.Worksheets.Add("XML");
                ExcelFile.Save();
            }
        }


        public void Perform()
        {
            ReadIdentifiersAndWriteToExcel(filename, customWorkSheetName, identifiers);
        }
    }
}
