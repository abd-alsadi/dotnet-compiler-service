using KmnlkCompilerApi.Management;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using static KmnlkCompilerApi.Constants.Enums;
using static KmnlkCommon.Shareds.LoggerManagement;
using KmnlkCompilerDll.Management;
using KmnlkCompilerDll.Models;

namespace KmnlkCompilerApi.Management
{
    public class PackageManagement
    {
        private BussinessCompilerManagement manager;
        public ILog logger;
        public PackageManagement()
        {
            string pathLog = SettingsManagement.getSetting(SettingsManagement.KEY_PathLog).ToString();
            string typeLog = SettingsManagement.getSetting(SettingsManagement.KEY_TypeLog).ToString();
            string versionVB = SettingsManagement.getSetting(SettingsManagement.KEY_VersionVB).ToString();
            string versionCSharp = SettingsManagement.getSetting(SettingsManagement.KEY_VersionCsharp).ToString();
            string tempFilesPath = SettingsManagement.getSetting(SettingsManagement.KEY_TempFilesPath).ToString();
            switch (typeLog.ToLower())
            {
                case "file":
                    logger = new FileLogger(pathLog);
                    break;
                case "db":
                    logger = new DBLogger(pathLog);
                    break;
                default:
                    logger = new FileLogger(pathLog);
                    break;
            }
            manager = new BussinessCompilerManagement(logger, versionVB, versionCSharp, tempFilesPath);
        }
        public string checkFileCode(clsFileCode file)
        {
            string dataFolderPath = SettingsManagement.getSetting(SettingsManagement.KEY_DataFolder).ToString();
            string result = manager.checkCode(dataFolderPath, file);
            return result;
        }
        public string getResultFileCode(clsFileCode file)
        {
            string dataFolderPath = SettingsManagement.getSetting(SettingsManagement.KEY_DataFolder).ToString();
            string result = manager.getResult(dataFolderPath, file);
            return result;
        }
        public byte[] generateDll(clsFileCode file)
        {
            Guid guid = Guid.NewGuid();
            string dataFolderPath = SettingsManagement.getSetting(SettingsManagement.KEY_DataFolder).ToString();
            dataFolderPath = Path.Combine(dataFolderPath, "DownloadDll");

            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            dataFolderPath = Path.Combine(dataFolderPath, guid.ToString());
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            byte[] result = null;
         
                var filePath = Path.Combine(dataFolderPath, guid.ToString());
                string returnPath = manager.generateDLL(dataFolderPath,file, filePath);
                if (returnPath != null && !returnPath.Contains("Error"))
                    result = File.ReadAllBytes(returnPath);
       
            return result;
        }

        public byte[] generateExe(clsFileCode file)
        {
            Guid guid = Guid.NewGuid();
            string dataFolderPath = SettingsManagement.getSetting(SettingsManagement.KEY_DataFolder).ToString();
            dataFolderPath = Path.Combine(dataFolderPath, "DownloadExe");

            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            dataFolderPath = Path.Combine(dataFolderPath, guid.ToString());
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }
            byte[] result = null;

            var filePath = Path.Combine(dataFolderPath, guid.ToString());
            string returnPath = manager.generateEXE(dataFolderPath, file, filePath);
            if (returnPath != null && !returnPath.Contains("Error"))
                result = File.ReadAllBytes(returnPath);

            return result;
        }
    }
}