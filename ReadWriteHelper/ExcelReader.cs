using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadWriteHelper
{
    public class ExcelReader
    {
        string _filePath;

        public ExcelReader(string filePath)
        {
            _filePath = filePath;
        }

        public List<EnumModel> GetModelList()
        {
            var enumModelList = new List<EnumModel>();

            //Create COM Objects. Create a COM object for everything that is referenced
            var xlApp = new Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Open(_filePath);

            foreach (Excel._Worksheet sheet in xlWorkbook.Sheets)
            {
                GetFromSheet(enumModelList, sheet);
            }
            CleanFile(xlApp, xlWorkbook);

            return enumModelList;
        }

        private static void GetFromSheet(List<EnumModel> enumModelList, Excel._Worksheet sheet)
        {
            var xlRange = sheet.UsedRange;
            var rowCount = xlRange.Rows.Count;
            var colCount = xlRange.Columns.Count;

            for (int i = 1; i <= colCount; i++)
            {
                if (xlRange.Cells[1, i] == null || xlRange.Cells[1, i].Value2 == null)
                    break;

                var enumModel = new EnumModel();
                enumModel.Title = xlRange.Cells[1, i].Value2.ToString();
                for (int j = 2; j <= rowCount; j++)
                {
                    if (xlRange.Cells[j, i] != null && xlRange.Cells[j, i].Value2 != null)
                        enumModel.EnumsList.Add(xlRange.Cells[j, i].Value2.ToString());
                }
                enumModelList.Add(enumModel);
            }

            CleanSheet(sheet, xlRange);
        }

        private static void CleanFile(Excel.Application xlApp, Excel.Workbook xlWorkbook)
        {
            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private static void CleanSheet(Excel._Worksheet sheet, Excel.Range xlRange)
        {
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(sheet);
        }
    }
}
