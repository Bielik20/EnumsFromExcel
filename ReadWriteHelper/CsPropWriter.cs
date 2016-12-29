using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteHelper
{
    public class CsPropWriter
    {
        List<EnumModel> _enumModelList = new List<EnumModel>();
        readonly string _propBase;
        readonly string _displayBase;
        readonly string _editorBase;

        public CsPropWriter(List<EnumModel> enumModelList)
        {
            _enumModelList = enumModelList;
            _propBase = BaseReader.Read(@"BaseFiles\PropBase.cs");
            _displayBase = BaseReader.Read(@"BaseFiles\DisplayBase.cshtml");
            _editorBase = BaseReader.Read(@"BaseFiles\EditorBase.cshtml");
        }

        public void CreateFiles()
        {
            Directory.CreateDirectory("Output_Prop");

            WriteProperties();
            WriteModelViewModelTransition();
            WriteDisplayView();
            WriteEditorView();
        }

        private void WriteProperties()
        {
            string lines = "";

            foreach (var enumModel in _enumModelList)
            {
                lines += _propBase.Replace("FILL_IN_FORMATED", enumModel.GetFormatedTitle());
                lines = lines.Replace("FILL_IN_TITLE", enumModel.Title);
            }

            StreamWriter file = new StreamWriter(@"Output_Prop\Properties.cs");
            file.WriteLine(lines);
            file.Close();
        }

        private void WriteModelViewModelTransition()
        {
            string lines = "//VM Constructor\n";

            foreach (var enumModel in _enumModelList)
                lines += $"{enumModel.GetFormatedTitle()} = model.{enumModel.GetFormatedTitle()};\n";

            lines += "\n\n//UpdateModel\n";

            foreach (var enumModel in _enumModelList)
                lines += $"model.{enumModel.GetFormatedTitle()} = this.{enumModel.GetFormatedTitle()};\n";

            StreamWriter file = new StreamWriter(@"Output_Prop\ViewModel.cs");
            file.WriteLine(lines);
            file.Close();
        }

        private void WriteDisplayView()
        {
            string lines = "";

            foreach (var enumModel in _enumModelList)
            {
                lines += _displayBase.Replace("FILL_IN", enumModel.GetFormatedTitle());
            }

            StreamWriter file = new StreamWriter(@"Output_Prop\Display.cshtml");
            file.WriteLine(lines);
            file.Close();
        }

        private void WriteEditorView()
        {
            string lines = "";

            foreach (var enumModel in _enumModelList)
            {
                lines += _editorBase.Replace("FILL_IN", enumModel.GetFormatedTitle());
            }

            StreamWriter file = new StreamWriter(@"Output_Prop\Editor.cshtml");
            file.WriteLine(lines);
            file.Close();
        }
    }
}
