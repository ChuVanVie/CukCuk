using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Helpers.Entities.DTO
{
    public class ServiceResult
    {
        public bool isSuccess { get; set; }

        public string message { get; set; }

        public object data { get; set; }
    }
}
