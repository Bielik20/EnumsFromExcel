using ReadWriteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumsFromExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            var excelReader = new ExcelReader(AppDomain.CurrentDomain.BaseDirectory + @"\input.xlsx");
            var enumModelList = excelReader.GetModelList();
            var csEnumWriter = new CsEnumWriter(enumModelList);
            csEnumWriter.CreateFiles();
            Console.WriteLine("Ready");
            Console.ReadLine();
        }
    }
}
