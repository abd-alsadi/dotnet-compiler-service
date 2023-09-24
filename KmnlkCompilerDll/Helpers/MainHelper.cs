
using KmnlkCompilerDll.Constants;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCompilerDll.Constants.Enums;

namespace KmnlkCompilerDll.Helpers
{
   public class MainHelper
    {
     
        public static string getPathWithOutExt(string path)
        {
            int ind = path.IndexOf(".");
            string res = "";
            if (ind>0)
             res = path.Substring(0, ind);
            return res;
        }
        public static void fillAssemblies(ref CompilerParameters parameters)
        {
            if (parameters != null)
            {
                parameters.ReferencedAssemblies.Add("System.dll");
            }
        }

        public static string getStringTypeExt(int type)
        {
            switch ((Enum_Output_Code)type)
            {
                case Enum_Output_Code.EXE:
                    return "exe";
                case Enum_Output_Code.DLL:
                    return "dll";

                default:
                    return "dll";
            }
        }

    }
}
