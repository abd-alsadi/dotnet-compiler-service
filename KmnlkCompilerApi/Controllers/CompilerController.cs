using KmnlkCompilerApi.Constants;
using KmnlkCompilerApi.Exceptions;
using KmnlkCompilerApi.Management;
using KmnlkCompilerApi.Models;
using KmnlkCommon.Shareds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static KmnlkCommon.Shareds.LoggerManagement;
using KmnlkCompilerDll.Models;
using KmnlkCompilerDll.Helpers;

namespace KmnlkCompilerApi.Controllers
{
    public class CompilerController : ApiController
    {
        private PackageManagement package = null;

        public CompilerController(PackageManagement repo)
        {
            package = repo;
        }

        [HttpPost]
        [ActionName("CheckFileCode")]
        public HttpResponseMessage CheckFileCode([FromBody]clsFileCode file)
        {
            package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstants.MSG_SUCCESS);
            string startTime = DateTime.Now.ToString("hh:mm:ss");
            string endTime = "";
            if (!isValid(file))
            {
                var response = new ResponseModel(modConstants.MSG_NOT_VALID_MODEL, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }
            try
            {
                string result = package.checkFileCode(file);
                endTime = DateTime.Now.ToString("hh:mm:ss");
                package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstants.MSG_SUCCESS);
                var response = new ResponseModel(result, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                new ApiException(package.logger, modConstants.MSG_SUCCESS, EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                var response = new ResponseModel(null, e.Message, HttpStatusCode.BadRequest, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }

        }

        [HttpPost]
        [ActionName("GetResultFileCode")]
        public HttpResponseMessage GetResultFileCode([FromBody]clsFileCode file)
        {
            package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstants.MSG_SUCCESS);
            string startTime = DateTime.Now.ToString("hh:mm:ss");
            string endTime = "";
            if (!isValid(file))
            {
                var response = new ResponseModel(modConstants.MSG_NOT_VALID_MODEL, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }
            try
            {
                string result = package.getResultFileCode(file);
                endTime = DateTime.Now.ToString("hh:mm:ss");
                package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstants.MSG_SUCCESS);
                var response = new ResponseModel(result, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                new ApiException(package.logger, modConstants.MSG_SUCCESS, EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                var response = new ResponseModel(null, e.Message, HttpStatusCode.BadRequest, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }

        }

        [HttpPost]
        [ActionName("GenerateDll")]
        public HttpResponseMessage GenerateDll([FromBody]clsFileCode file, string fileName = "download")
        {
            package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstants.MSG_SUCCESS);
            string startTime = DateTime.Now.ToString("hh:mm:ss");
            string endTime = "";
            int typeDll = 1;
            HttpResponseMessage res;
            if (!isValid(file))
            {
                var response = new ResponseModel(modConstants.MSG_NOT_VALID_MODEL, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }
            try
            {

                byte[] bytesFile = package.generateDll(file);
                endTime = DateTime.Now.ToString("hh:mm:ss");
                res = DownloadManagement.Download(bytesFile, fileName + "." + MainHelper.getStringTypeExt(typeDll), MainHelper.getStringTypeExt(typeDll), MainHelper.getStringTypeExt(typeDll));
                package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstants.MSG_SUCCESS);
                return res;
            }
            catch (Exception e)
            {
                new ApiException(package.logger, modConstants.MSG_SUCCESS, EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                var response = new ResponseModel(null, e.Message, HttpStatusCode.BadRequest, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }

        }


        [HttpPost]
        [ActionName("GenerateExe")]
        public HttpResponseMessage GenerateExe([FromBody]clsFileCode file, string fileName = "download")
        {
            package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstants.MSG_SUCCESS);
            string startTime = DateTime.Now.ToString("hh:mm:ss");
            string endTime = "";
            int typeExe = 0;
            HttpResponseMessage res;
            if (!isValid(file))
            {
                var response = new ResponseModel(modConstants.MSG_NOT_VALID_MODEL, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }
            try
            {

                byte[] bytesFile = package.generateExe(file);
                endTime = DateTime.Now.ToString("hh:mm:ss");
                res = DownloadManagement.Download(bytesFile, fileName + "." + MainHelper.getStringTypeExt(typeExe), MainHelper.getStringTypeExt(typeExe), MainHelper.getStringTypeExt(typeExe));
                package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstants.MSG_SUCCESS);
                return res;
            }
            catch (Exception e)
            {
                new ApiException(package.logger, modConstants.MSG_SUCCESS, EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                var response = new ResponseModel(null, e.Message, HttpStatusCode.BadRequest, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }

        }

        [NonAction]
        public bool isValid(clsFileCode file)
        {
            if (file == null || file.codes == null || file.codes.Count <= 0)
                return false;
            return true;
        }
    }
}
