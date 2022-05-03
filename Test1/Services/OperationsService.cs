using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test1.CommonClass;
using Test1.Interface;
using Test1.Model;

namespace Test1.Services
{
    public class OperationsService : IOperations
    {
        #region "Variable Declarations"

        IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        #endregion

        public OperationsService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

        }

        public DeleteDirectoryResponse DeleteDirectoryOperation(DeleteDirectoryRequest objReq)
        {
            DeleteDirectoryResponse objResp = new DeleteDirectoryResponse();
            try
            {
                if (!Directory.Exists(objReq.path))
                {
                    objResp.Message = "Path Doesnot Exist";
                    return objResp;
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(objReq.path);
                foreach (FileInfo file in di.GetFiles())
                {
                    //Returning string if you want to log any errors
                    DeteleFiles(file);
                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        //Returning string if you want to log any errors
                        DeteleFiles(file);
                    }
                    try
                    {
                        dir.Delete(true);
                    }
                    catch (Exception)
                    {

                    }

                }

                if (Directory.Exists(objReq.path))
                    Directory.Delete(objReq.path, true);

                objResp.Message = "Operation Successfull";
                return objResp;
            }
            catch (Exception ex)
            {
                objResp.Message = ex.ToString();
                throw;
            }
            finally
            { }
        }

        private string DeteleFiles(FileInfo fileInfo)
        {
            try
            {
                if (KillProcess(fileInfo))
                {
                    File.SetAttributes(fileInfo.FullName, FileAttributes.Normal);
                    fileInfo.Delete();
                    return string.Empty;
                }
                else
                    return fileInfo.Name + " Not Deleted";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private bool KillProcess(FileInfo fileInfo)
        {
            bool IsOpen = false;
            try
            {

                try
                {
                    using (FileStream stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        stream.Close();
                    }
                }
                catch (IOException)
                {
                    IsOpen = true;
                }

                if (IsOpen)
                {
                    var contentType = MimeTypes.GetContentType(fileInfo.Name);
                    foreach (var process in Process.GetProcessesByName(contentType.Split("/")[1]))
                    {
                        process.Kill();
                    }
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
