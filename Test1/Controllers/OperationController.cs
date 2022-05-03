using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Test1.CommonClass;
using Test1.Model;
using Test1.Providers;
using Test1.Services;

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : Controller
    {
        #region "Variable Declarations"

        private readonly IHostingEnvironment _hostingEnvironment;
        private IConfiguration _configSetting;
        private OperationsCaller objCaller = new OperationsCaller();
        #endregion

        #region "Constructors"
        public OperationController(IConfiguration currentconfig, IHostingEnvironment hostingEnvironment)
        {
            _configSetting = currentconfig;
            _hostingEnvironment = hostingEnvironment;

        }
        #endregion


        [HttpPost]
        [Route("DeleteFolderFile")]
        public async Task<ResponseModel> DeleteFolderFile(DeleteDirectoryRequest objReq)
        {
            ResponseModel objResponseModel = new ResponseModel();

            try
            {
                //Methode for getting vendor list by type
                var responseModel = objCaller.DeleteDirectoryOperation(new OperationsService(_configSetting, _hostingEnvironment), objReq);

                string statusMessage = responseModel.Message;
                objResponseModel.status = true;
                objResponseModel.statusCode = (int)HttpStatusCode.OK;
                objResponseModel.message = statusMessage;
                objResponseModel.responseData = responseModel;
            }
            catch (Exception ex)
            {
                //Can also log the errors
                objResponseModel.message = "Error : " + ex.Message;
                objResponseModel.statusCode = (int)HttpStatusCode.InternalServerError;
                objResponseModel.status = false;
            }

            return await Task.FromResult(objResponseModel);
        }
    }
}
