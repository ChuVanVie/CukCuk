using Microsoft.AspNetCore.Mvc;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Helpers.Enum;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.MaterialGroupService;

namespace Misa.CukCuk.Materials.Controllers
{
    public class MaterialGroupController : BaseController<MaterialGroup>
    {
        #region Field

        private readonly IMaterialGroupService _materialGroupService;

        #endregion

        #region Constructor
        public MaterialGroupController(IMaterialGroupService materialGroupService) : base(materialGroupService)
        {
            _materialGroupService = materialGroupService;
        }
        #endregion

        #region Override

        public override IActionResult InsertRecord([FromBody] MaterialGroup materialGroup)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _materialGroupService.InsertRecord(materialGroup);

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
                    UserMsg = "Mã nhóm " + materialGroup.MaterialGroupCode + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }

        [HttpPut("{materialGroupId}")]
        public override IActionResult UpdateRecord(Guid materialGroupId, [FromBody] MaterialGroup materialGroup)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _materialGroupService.UpdateRecord(materialGroupId, materialGroup);

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
                    UserMsg = "Mã nhóm " + materialGroup.MaterialGroupCode + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }

            #endregion
        }
    }
}
