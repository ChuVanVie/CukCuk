using Misa.Cukcuk.Repositories.MaterialRepository;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;

namespace Misa.CukCuk.Materials.Services.MaterialService
{
    public class MaterialService : BaseService<Material>, IMaterialService
    {
        #region Field

        private readonly IMaterialRepository _materialRepository;

        #endregion

        #region Constructor

        public MaterialService(IMaterialRepository materialRepository) : base(materialRepository)
        {
            _materialRepository = materialRepository;
        }

        #endregion

        #region Method

        /// <summary>
        /// Xử lý lấy mã nguyên vật liệu mới (lớn nhất trong db)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: CVVIET (22/05/2023)
        public Material GetNewMaterialCode()
        {
            return _materialRepository.GetNewMaterialCode();
        } 

        #endregion
    }
}
