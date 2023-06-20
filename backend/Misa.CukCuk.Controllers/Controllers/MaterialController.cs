using Microsoft.AspNetCore.Mvc;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Helpers.Enum;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;
using Misa.CukCuk.Materials.Services.MaterialService;
using System.Resources;

namespace Misa.CukCuk.Materials.Controllers
{
    public class MaterialController : BaseController<Material>
    {
        #region Field

        private readonly IMaterialService _materialService;

        #endregion

        #region Constructor
        public MaterialController(IMaterialService materialService) : base(materialService)
        {
            _materialService = materialService;
        }
        #endregion

        #region Method

        [HttpGet("newMaterialCode")]
        public IActionResult GetNewMaterialCode()
        {
            try
            {
                //Xử lý kết quả trả vê
                var material = _materialService.GetNewMaterialCode();

                //Thành công
                if (material != null)
                {
                    var newMaterialId = Guid.NewGuid();
                    var str1 = material.MaterialCode.Substring(0, 7);
                    var str2 = material.MaterialCode.Substring(7);
                    var newMaterialCode = str1 + (int.Parse(str2) + 1);
                    return StatusCode((int)HTTPStatusCode.Success, new
                    {
                        newMaterialId,
                        newMaterialCode
                    });
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

        public override IActionResult InsertRecord([FromBody] Material material)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _materialService.InsertRecord(material);

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
                    UserMsg = "Mã nguyên vật liệu " + material.MaterialCode + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }
        }

        [HttpPut("{materialId}")]
        public override IActionResult UpdateRecord(Guid materialId, [FromBody] Material material)
        {
            try
            {
                //Xử lý kết quả trả vê
                var serviceResult = _materialService.UpdateRecord(materialId, material);

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
                    UserMsg = "Mã nguyên vật liệu " + material.MaterialCode + " đã tồn tại!",
                    TraceId = HttpContext.TraceIdentifier,
                    MoreInfo = Resources.MoreInfo
                });
            }

            #endregion
        }
    }
}
