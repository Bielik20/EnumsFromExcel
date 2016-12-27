using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumsFromExcel
{
    class CsWriter
    {
        List<EnumModel> _enumModelList = new List<EnumModel>();
        readonly string _enumBase;
        readonly string _databaseBase;

        public CsWriter(List<EnumModel> enumModelList)
        {
            _enumModelList = enumModelList;
            _enumBase = ReadBase(@"BaseFiles\EnumBase.cs");
            _databaseBase = ReadBase(@"BaseFiles\DatabaseBase.cs");
        }

        public void CreateFiles()
        {
            Directory.CreateDirectory("Output");
            Directory.CreateDirectory(@"Output\Enums");
            Directory.CreateDirectory(@"Output\ADD");

            Parallel.ForEach(_enumModelList, enumModel =>
            {
                WriteEnum(enumModel);
                WriteDatabaseModel(enumModel);
            });
            WriteEnumProperties();
            WriteDatabaseModelProperties();
            WriteDbSets();
        }

        private void WriteDatabaseModel(EnumModel enumModel)
        {
            // Compose a string that consists of three lines.
            string lines = _databaseBase;
            lines = lines.Replace("FILL_IN_TITLE", "ADD_" + enumModel.GetFormatedTitle());
            lines = lines.Replace("FILL_IN_ENUM_TITLE", enumModel.GetFormatedTitle() + "Enum");

            // Write the string to a file.
            StreamWriter file = new StreamWriter(@"Output\ADD\ADD_" + enumModel.GetFormatedTitle() + ".cs");
            file.WriteLine(lines);

            file.Close();
        }

        private void WriteDatabaseModelProperties()
        {
            string lines = "";
            foreach (var enumModel in _enumModelList)
            {
                lines += $"[Display(Name = \"{enumModel.Title}\")]\n" +
                    $"public virtual ICollection<ADD_{enumModel.GetFormatedTitle()}> {enumModel.GetFormatedTitle()}" + " { get; set; }\n";
            }

            StreamWriter file = new StreamWriter(@"Output\DatabaseModelProperties.cs");
            file.WriteLine(lines);
            file.Close();
        }

        private void WriteDbSets()
        {
            string lines = "";
            foreach (var enumModel in _enumModelList)
            {
                lines += $"public DbSet<ADD_{enumModel.GetFormatedTitle()}> ADD_{enumModel.GetFormatedTitle()}" + " { get; set; }\n";
            }

            StreamWriter file = new StreamWriter(@"Output\DbSets.cs");
            file.WriteLine(lines);
            file.Close();
        }

        private void WriteEnum(EnumModel enumModel)
        {
            // Compose a string that consists of three lines.
            string lines = _enumBase;
            lines = lines.Replace("FILL_IN_TITLE", enumModel.GetFormatedTitle() + "Enum");
            lines = lines.Replace("FILL_IN_LIST", enumModel.GetFormatedEnumsList());

            // Write the string to a file.
            StreamWriter file = new StreamWriter(@"Output\Enums\" + enumModel.GetFormatedTitle() + "Enum.cs");
            file.WriteLine(lines);

            file.Close();
        }

        private void WriteEnumProperties()
        {
            string lines = "";
            foreach (var enumModel in _enumModelList)
            {
                lines += $"[Display(Name = \"{enumModel.Title}\")]\n" + 
                    $"public {enumModel.GetFormatedTitle()}Enum {enumModel.GetFormatedTitle()}" + " { get; set; }\n";
            }

            StreamWriter file = new StreamWriter(@"Output\EnumProperties.cs");
            file.WriteLine(lines);
            file.Close();
        }

        private string ReadBase(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
