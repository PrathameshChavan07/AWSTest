using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.CommonClass;
using Test1.Interface;
using Test1.Model;

namespace Test1.Providers
{
    public class OperationsCaller
    {
        #region "Variable Declarations"
        public IOperations operations;
        #endregion

        public DeleteDirectoryResponse DeleteDirectoryOperation(IOperations _operations, DeleteDirectoryRequest objReq)
        {
            operations = _operations;
            return operations.DeleteDirectoryOperation(objReq);
        }
    }
}
