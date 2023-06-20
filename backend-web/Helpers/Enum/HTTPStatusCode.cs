using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Helpers.Enum
{
    public enum HTTPStatusCode
    {
        /// <summary>
        /// Thực thi thành công
        /// </summary>
        Success = 200,

        /// <summary>
        /// Thêm dữ liệu thành công
        /// </summary>
        CreateSuccess = 201,

        /// <summary>
        /// Thông tin không hợp lệ
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Trùng thông tin
        /// </summary>
        DuplicateUniqueData = 409,

        /// <summary>
        /// Lỗi server
        /// </summary>
        InternalServerError = 500,
    }
}
