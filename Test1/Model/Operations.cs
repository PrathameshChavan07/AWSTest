using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Model
{
    public class Operations
    {
    }

    public class DeleteDirectoryRequest
    {
        [Required(ErrorMessage = "Path is Required")]
        public string path { get; set; }
    }

    public class DeleteDirectoryResponse
    {
        public string Message { get; set; }
    }
}
