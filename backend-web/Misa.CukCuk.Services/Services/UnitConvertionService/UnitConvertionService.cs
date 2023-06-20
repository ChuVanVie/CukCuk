using Misa.Cukcuk.Repositories.UnitConvertionRepository;
using Misa.CukCuk.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Services.Services.UnitConvertionService
{
    public class UnitConvertionService : BaseService<UnitConvertion>, IUnitConvertionService
    {
        #region Field

        private readonly IUnitConvertionRepository _unitConvertionRepository;

        #endregion

        #region Constructor

        public UnitConvertionService(IUnitConvertionRepository unitConvertionRepository) : base(unitConvertionRepository)
        {
            _unitConvertionRepository = unitConvertionRepository;
        }

        #endregion

        public List<UnitConvertion> GetConvertionByMaterial(Guid materialId)
        {
            return _unitConvertionRepository.GetConvertionByMaterial(materialId);
        }
    }
}
