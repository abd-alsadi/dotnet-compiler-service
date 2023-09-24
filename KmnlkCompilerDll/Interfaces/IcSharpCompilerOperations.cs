using KmnlkCompilerDll.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkCompilerDll.Interfaces
{
   public interface IcSharpCompilerOperations
    {
        string checkFileCode(string dataFolderPath, clsFileCode file);
        string getResultFileCode(string dataFolderPath, clsFileCode file);

        string generateFileCodeDll(string dataFolderPath, clsFileCode file,string path);

        string generateFileCodeEXE(string dataFolderPath, clsFileCode file,string path);

    }
}
