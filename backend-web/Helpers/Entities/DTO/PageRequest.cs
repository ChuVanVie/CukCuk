using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Helpers.Entities.DTO
{
    public class PageRequest
    {
        public int pageSize { get; set; }

        public int pageNumber { get; set; }

        public string? stringCode { get; set; }

        public string? stringName { get; set; }

        public string? stringAddress { get; set; }

        public string? stringProperty { get; set; }

        public string? stringUnit { get; set; }

        public string? stringMaterialgroup { get; set; }

        public string? stringNote { get; set; }

        public int? stringStopFollow { get; set; }


    }
}
