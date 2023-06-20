using Microsoft.AspNetCore.Mvc;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Helpers.Enum;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.WarehouseService;

namespace Misa.CukCuk.Materials.Controllers
{
    public class WarehouseController : BaseController<Warehouse>
    {
        #region Field

        private readonly IWarehouseService _warehouseService;

        #endregion

        #region Constructor
        public WarehouseController(IWarehouseService warehouseService) : base(warehouseService)
        {
            _warehouseService = warehouseService;
        }
        #endregion

        #region Override

        [HttpPost]
        public override IActionResult InsertRecord([FromBody] Warehouse warehouse)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _warehouseService.InsertRecord(warehouse);

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
                    UserMsg = "Mã kho " + warehouse.WarehouseCode + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }

        [HttpPut("{warehouseId}")]
        public override IActionResult UpdateRecord(Guid warehouseId, [FromBody] Warehouse warehouse)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _warehouseService.UpdateRecord(warehouseId, warehouse);

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
                    UserMsg = "Mã kho " + warehouse.WarehouseCode + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }

            #endregion
        }
    }
}
