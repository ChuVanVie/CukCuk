using Misa.Cukcuk.Repositories.MaterialGroupRepositoty;
using Misa.Cukcuk.Repositories.MaterialRepository;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;

namespace Misa.CukCuk.Materials.Services.MaterialGroupService
{
    public class MaterialGroupService : BaseService<MaterialGroup>, IMaterialGroupService
    {
        #region Field

        private readonly IMaterialGroupRepository _materialGroupRepository;

        #endregion

        #region Constructor

        public MaterialGroupService(IMaterialGroupRepository materialGroupRepository) : base(materialGroupRepository)
        {
            _materialGroupRepository = materialGroupRepository;
        }

        #endregion
    }
}
