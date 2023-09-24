using KmnlkCompilerDll.Management;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using static KmnlkCompilerDll.Constants.Enums;
using static KmnlkCommon.Shareds.LoggerManagement;
using KmnlkCompilerDll.Models;

namespace KmnlkCompilerDll.Management
{
    public class BussinessCompilerManagement
    {
        private cSharpCompilerManagement CSharpM;
        private cVisualBasicCompilerManagement VBM;
        public ILog logger;
        public string versionVB;
        public string versionCSharp;
        private string tempPath;
        public BussinessCompilerManagement(ILog logger,string versionVB,string versionCSharp,string tempPath)
        {
            this.tempPath = tempPath;
            this.logger = logger;
            CSharpM = new cSharpCompilerManagement(logger,tempPath,versionCSharp);
            VBM = new cVisualBasicCompilerManagement(logger, tempPath, versionVB);
            this.versionVB = versionVB;
            this.versionCSharp = versionCSharp;
        }

        public string checkCode(string dataFolderPath, clsFileCode file)
        {
            switch (file.type)
            {
                case (int)Enum_Type_Code.CSHARP:
                    return CSharpM.checkFileCode(dataFolderPath, file);
                case (int)Enum_Type_Code.VB:
                    return VBM.checkFileCode(dataFolderPath, file);
               
                default:
                    return CSharpM.checkFileCode(dataFolderPath, file);
            }
        }

        public string generateDLL(string dataFolderPath, clsFileCode file,string path)
        {
            switch (file.type)
            {
                case (int)Enum_Type_Code.CSHARP:
                    return CSharpM.generateFileCodeDll(dataFolderPath, file, path);
                case (int)Enum_Type_Code.VB:
                    return VBM.generateFileCodeDll(dataFolderPath, file, path);

                default:
                    return CSharpM.generateFileCodeDll(dataFolderPath, file, path);
            }
        }

        public string generateEXE(string dataFolderPath, clsFileCode file, string path)
        {
            switch (file.type)
            {
                case (int)Enum_Type_Code.CSHARP:
                    return CSharpM.generateFileCodeEXE(dataFolderPath, file, path);
                case (int)Enum_Type_Code.VB:
                    return VBM.generateFileCodeEXE(dataFolderPath, file, path);

                default:
                    return CSharpM.generateFileCodeEXE(dataFolderPath, file, path);
            }
        }

        public string getResult(string dataFolderPath, clsFileCode file)
        {
            switch (file.type)
            {
                case (int)Enum_Type_Code.CSHARP:
                    return CSharpM.getResultFileCode(dataFolderPath, file);
                case (int)Enum_Type_Code.VB:
                    return VBM.getResultFileCode(dataFolderPath, file);

                default:
                    return CSharpM.getResultFileCode(dataFolderPath, file);
            }
        }

    }
}