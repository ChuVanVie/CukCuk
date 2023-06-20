using Microsoft.AspNetCore.Mvc;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Helpers.Enum;
using Misa.CukCuk.Materials.Services.BaseService;
using System.Net;
using System.Resources;

namespace Misa.CukCuk.Materials.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field

        private IBaseService<T> _baseService;

        #endregion

        #region Constructor
        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region METHOD

        [HttpGet()]
        public IActionResult GetAllRecords()
        {
            try
            {
                //Xử lý kết quả trả vê
                var result = _baseService.GetAllRecords();

                //Thành công (
                if (result != null)
                {
                    return StatusCode((int)HTTPStatusCode.Success, result);
                }

                //Thất bại (
                else
                {
                    return StatusCode((int)HTTPStatusCode.BadRequest);
                }
            }
            //Try Catch để bắt 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HTTPStatusCode.InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }

        }

        [HttpGet("Pagination")]
        public IActionResult GetPaging([FromQuery] PageRequest pageRequest)
        {
            try
            {
                //Xử lý kết quả trả vê
                var result = _baseService.GetPaging(pageRequest);

                //Thành công (
                if (result != null)
                {
                    return StatusCode((int)HTTPStatusCode.Success, result);
                }

                //Thất bại (
                else
                {
                    return StatusCode((int)HTTPStatusCode.BadRequest);
                }
            }
            //Try Catch để bắt 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HTTPStatusCode.InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }

        }



        [HttpPost]
        public virtual IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _baseService.InsertRecord(record);

                //Thành công
                if (serviceResult.isSuccess)
                {
                    return StatusCode((int)HTTPStatusCode.CreateSuccess, serviceResult);
                }

                //Thất bại
                else
                {
                    return StatusCode((int)HTTPStatusCode.BadRequest, serviceResult);
                }
            }
            //Try Catch để bắt Exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HTTPStatusCode.InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }


        [HttpPut("{recordId}")]
        public virtual IActionResult UpdateRecord(Guid recordId, [FromBody] T record)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _baseService.UpdateRecord(recordId, record);

                //Thành công
                if (serviceResult.isSuccess)
                {
                    return StatusCode((int)HTTPStatusCode.Success, serviceResult);
                }

                //Thất bại
                else
                {
                    return StatusCode((int)HTTPStatusCode.BadRequest, serviceResult);
                }
            }
            //Try Catch để bắt Exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HTTPStatusCode.InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }


        [HttpDelete("{recordId}")]
        public IActionResult DeleteRecord(Guid recordId)
        {
            try
            {
                //Xử lý kết quả trả vê
                var rowsAffected = _baseService.DeleteRecord(recordId);

                //Thành công
                if (rowsAffected > 0)
                {
                    return StatusCode((int)HTTPStatusCode.Success);
                }

                //Thất bại
                else
                {
                    return NotFound();
                }
            }
            //Try Catch để bắt Exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HTTPStatusCode.InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }

        #endregion
    }
}
