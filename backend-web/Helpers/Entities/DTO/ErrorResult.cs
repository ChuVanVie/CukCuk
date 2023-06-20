using Misa.CukCuk.Helpers.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Helpers.Entities.DTO
{
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi trả về
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        public string DevMsg { get; set; }

        public string UserMsg { get; set; }

        public object MoreInfo { get; set; }

        public string TraceId { get; set; }
    }
}
