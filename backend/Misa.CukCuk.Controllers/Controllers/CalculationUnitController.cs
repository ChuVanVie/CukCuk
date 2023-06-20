using Microsoft.AspNetCore.Mvc;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Helpers.Enum;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.CalculationUnitService;

namespace Misa.CukCuk.Materials.Controllers
{
    public class CalculationUnitController : BaseController<CalculationUnit>
    {
        #region Field

        private readonly ICalculationUnitService _calculationUnitService;

        #endregion

        #region Constructor
        public CalculationUnitController(ICalculationUnitService calculationUnitService) : base(calculationUnitService)
        {
            _calculationUnitService = calculationUnitService;
        }
        #endregion

        #region Override

        [HttpPost]
        public override IActionResult InsertRecord([FromBody] CalculationUnit calculationUnit)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _calculationUnitService.InsertRecord(calculationUnit);

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
                    UserMsg = "Đơn vị tính " + calculationUnit.CalculationUnitName + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }

        [HttpPut("{calculationUnitId}")]
        public override IActionResult UpdateRecord(Guid calculationUnitId, [FromBody] CalculationUnit calculationUnit)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _calculationUnitService.UpdateRecord(calculationUnitId, calculationUnit);

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
                    UserMsg = "Đơn vị tính " + calculationUnit.CalculationUnitName + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }

            #endregion
        }
    }
}
