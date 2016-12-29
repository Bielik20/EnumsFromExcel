using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteHelper
{
    public class EnumModel
    {
        public string Title { get; set; }
        public List<string> EnumsList { get; set; } = new List<string>();

        public string GetFormatedTitle()
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            StringBuilder sb = new StringBuilder();
            foreach (char c in textInfo.ToTitleCase(Title))
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public string GetFormatedEnumsList()
        {
            string list = "";
            for (int i = 0; i < EnumsList.Count; i++)
            {
                list += $"\t\t[Display(Name = \"{EnumsList[i]}\")]\n" + $"\t\tA{i},\n";
            }
            return list;
        }
    }
}
