
using KmnlkCompilerDll.Constants;
using KmnlkCompilerDll.Exceptions;
using KmnlkCompilerDll.Helpers;
using KmnlkCompilerDll.Interfaces;
using KmnlkCommon.Shareds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCommon.Shareds.LoggerManagement;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using KmnlkCompilerDll.Models;

namespace KmnlkCompilerDll.Management
{
    public class cVisualBasicCompilerManagement : IcSharpCompilerOperations, IValidationOperations
    {
        private ILog logger;
        private CodeDomProvider provider;
        private CompilerParameters parameters;
        Dictionary<string, string> provOptions;
        private string tempFiles;
        public cVisualBasicCompilerManagement(ILog logger, string tempFiles,string version= "v3.5")
        {
            this.tempFiles = tempFiles;
            this.logger = logger;
            this.provOptions = new Dictionary<string, string>();
            provOptions.Add("CompilerVersion", version);
            //provider = CodeDomProvider.CreateProvider(modConstant.LANGUAGE_VB_NAME,provOptions);
            provider = CodeDomProvider.CreateProvider(modConstant.LANGUAGE_VB_NAME);
            parameters = new CompilerParameters();
            init();
        }

        private void init()
        {
            // Set the level at which the compiler 
            // should start displaying warnings.
            parameters.WarningLevel = 3;

            // Set whether to treat all warnings as errors.
            parameters.TreatWarningsAsErrors = false;


            // Generate debug information.
            parameters.IncludeDebugInformation = true;


            if (tempFiles != "")
                parameters.TempFiles = new TempFileCollection(tempFiles);

            // Set compiler argument to optimize output.
            //parameters.CompilerOptions = "/optimize";

            //if (Directory.Exists("Resources"))
            //{
            //    if (provider.Supports(GeneratorSupport.Resources))
            //    {
            //        // Set the embedded resource file of the assembly.
            //        // This is useful for culture-neutral resources,
            //        // or default (fallback) resources.
            //        parameters.EmbeddedResources.Add("Resources\\Default.resources");

            //        // Set the linked resource reference files of the assembly.
            //        // These resources are included in separate assembly files,
            //        // typically localized for a specific language and culture.
            //        parameters.LinkedResources.Add("Resources\\nb-no.resources");
            //    }
            //}


            //if (provider.Supports(GeneratorSupport.EntryPointMethod))
            //{
            //    // Specify the class that contains 
            //    // the main method of the executable.
            //    parameters.MainClass = "Samples.Class1";
            //}
        }
        public string checkFileCode(string dataFolderPath, clsFileCode file)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(file))
                {
                    return null;
                }

                string result = "";

                // Add an assembly reference.
                MainHelper.fillAssemblies(ref parameters);
                // Save the assembly as a physical file.
                parameters.GenerateInMemory = true;
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, file.codes.ToArray());
                // Check errors
                StringBuilder sb = new StringBuilder();
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}): {1} [col:{2},line:{3}]", error.ErrorNumber, error.ErrorText, error.Column, error.Line));
                    }
                }
                else
                {
                    //sb.Append(String.Format("Source files {0} built into {1} successfully.", file.codes.Count.ToString(), results.PathToAssembly));
                    //sb.Append(String.Format("{0} temporary files created during the compilation.",parameters.TempFiles.Count.ToString()));
                    sb.Append(String.Format("the files code are valid"));
                }
                result = sb.ToString();
                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                return result;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }
        public string generateFileCodeDll(string dataFolderPath, clsFileCode file,string path)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), path, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(file))
                {
                    return null;
                }

                string result = "";
                Guid guid = Guid.NewGuid();
                string nameFile = guid.ToString();
                string tempPath = path;
                string newPath = tempPath+".dll";

                // Set the assembly file name to generate.
                parameters.OutputAssembly = newPath;
                // Add an assembly reference.
                MainHelper.fillAssemblies(ref parameters);
                // Generate an executable instead of 
                // a class library.
                parameters.GenerateExecutable = false; //DLL
                // Save the assembly as a physical file.
                parameters.GenerateInMemory = false;

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, file.codes.ToArray());


                // Check errors
                StringBuilder sb = new StringBuilder();
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}): {1} [col:{2},line:{3}]", error.ErrorNumber, error.ErrorText, error.Column, error.Line));
                    }
                }
                else
                {
                    return newPath;

                }

                // Get assembly, type and the Main method:
                //Assembly assembly = results.CompiledAssembly;
                //Type program = assembly.GetType("First.Program");
                //MethodInfo main = program.GetMethod("Main");

                //// run it
                //var obj = new object[] { 2, 3 };
                //var ooo = main.Invoke(null, null);
                result = sb.ToString();
                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                return result;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }
        public string getResultFileCode(string dataFolderPath, clsFileCode file)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(file))
                {
                    return null;
                }

                string result = "";
          
                // Add an assembly reference.
                MainHelper.fillAssemblies(ref parameters);
                // Generate an executable instead of 
                // a class library.
                parameters.GenerateExecutable = false; //DLL
                // Save the assembly as a physical file.
                parameters.GenerateInMemory = true;

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, file.codes.ToArray());


                // Check errors
                StringBuilder sb = new StringBuilder();
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}): {1} [col:{2},line:{3}]", error.ErrorNumber, error.ErrorText, error.Column, error.Line));
                    }
                }

                // Get assembly, type and the Main method:
                Assembly assembly = results.CompiledAssembly;
                Type program = assembly.GetType("Main.Program");
                MethodInfo main = program.GetMethod(modConstant.MAIN_FUNCTION_NAME);

                //// run it
                //var obj = new object[] { 2, 3 };

                String outputResults = (String)main.Invoke(null, null);
                if(outputResults!=null)
                    sb.Append(outputResults);
                result = sb.ToString();
                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                return result;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }
        public string generateFileCodeEXE(string dataFolderPath, clsFileCode file, string path)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), path, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(file))
                {
                    return null;
                }

                string result = "";
                Guid guid = Guid.NewGuid();
                string nameFile = guid.ToString();
                string tempPath = path;
                string newPath = tempPath + ".exe";

                // Set the assembly file name to generate.
                parameters.OutputAssembly = newPath;
                // Add an assembly reference.
                MainHelper.fillAssemblies(ref parameters);
                // Generate an executable instead of 
                // a class library.
                parameters.GenerateExecutable = true; //DLL
                // Save the assembly as a physical file.
                parameters.GenerateInMemory = false;

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, file.codes.ToArray());


                // Check errors
                StringBuilder sb = new StringBuilder();
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}): {1} [col:{2},line:{3}]", error.ErrorNumber, error.ErrorText, error.Column, error.Line));
                    }
                }
                else
                {
                    return newPath;

                }

                // Get assembly, type and the Main method:
                //Assembly assembly = results.CompiledAssembly;
                //Type program = assembly.GetType("First.Program");
                //MethodInfo main = program.GetMethod("Main");

                //// run it
                //var obj = new object[] { 2, 3 };
                //var ooo = main.Invoke(null, null);
                result = sb.ToString();
                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                return result;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }
        public bool isValid(object model)
        {
            bool result = true;
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            if (model == null)
                result = false;
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
            return result;
        }


    }
}
