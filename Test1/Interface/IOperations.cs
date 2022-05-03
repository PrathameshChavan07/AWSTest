using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1.Model;

namespace Test1.Interface
{

    public interface IOperations
    {
        DeleteDirectoryResponse DeleteDirectoryOperation(DeleteDirectoryRequest objReq);
    }
}
