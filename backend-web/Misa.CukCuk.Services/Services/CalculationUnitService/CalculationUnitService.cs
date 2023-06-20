using Misa.Cukcuk.Repositories.CalculationUnitRepository;
using Misa.Cukcuk.Repositories.MaterialGroupRepositoty;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;

namespace Misa.CukCuk.Materials.Services.CalculationUnitService
{
    public class CalculationUnitService : BaseService<CalculationUnit>, ICalculationUnitService
    {
        #region Field

        private readonly ICalculationUnitRepository _calculationUnitRepository;

        #endregion

        #region Constructor

        public CalculationUnitService(ICalculationUnitRepository calculationUnitRepository) : base(calculationUnitRepository)
        {
            _calculationUnitRepository = calculationUnitRepository;
        }

        #endregion
    }
}
