using Microsoft.AspNetCore.Mvc;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Helpers.Entities;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Helpers.Enum;
using Misa.CukCuk.Services.Services.UnitConvertionService;

namespace Misa.CukCuk.Materials.Controllers
{
    public class UnitConvertionController : BaseController<UnitConvertion>
    {
        #region Field

        private readonly IUnitConvertionService _unitConvertionService;

        #endregion

        #region Constructor
        public UnitConvertionController(IUnitConvertionService unitConvertionService) : base(unitConvertionService)
        {
            _unitConvertionService = unitConvertionService;
        }
        #endregion

        #region Method

        [HttpGet("{materialId}")]
        public IActionResult GetNewMaterialCode([FromRoute] Guid materialId)
        {
            try
            {
                //Xử lý kết quả trả vê
                var result = _unitConvertionService.GetConvertionByMaterial(materialId);

                //Thành công
                if (result != null)
                {
                    return StatusCode((int)HTTPStatusCode.Success, result);
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

        #region Override

        [HttpPost]
        public override IActionResult InsertRecord([FromBody] UnitConvertion unitConvertion)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _unitConvertionService.InsertRecord(unitConvertion);

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
                return StatusCode((int)HTTPStatusCode.DuplicateUniqueData, new ErrorResult
                {
                    ErrorCode = ErrorCode.DuplicateCode,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = "Có lỗi xảy ra",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }

        [HttpPut("{unitConvertionId}")]
        public override IActionResult UpdateRecord(Guid unitConvertionId, [FromBody] UnitConvertion unitConvertion)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _unitConvertionService.UpdateRecord(unitConvertionId, unitConvertion);

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
                return StatusCode((int)HTTPStatusCode.DuplicateUniqueData, new ErrorResult
                {
                    ErrorCode = ErrorCode.DuplicateCode,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = "Có lỗi xảy ra",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }

            #endregion
        }
    }
}
