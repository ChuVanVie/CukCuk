using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;

namespace Misa.CukCuk.Materials.Services.MaterialService
{
    public interface IMaterialService : IBaseService<Material>
    {
        /// <summary>
        /// Xử lý lấy mã nguyên vật liệu mới (lớn nhất trong db)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: CVVIET (22/05/2023)
        public Material GetNewMaterialCode();
    }
}
