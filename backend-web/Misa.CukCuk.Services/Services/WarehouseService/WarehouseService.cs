using Misa.Cukcuk.Repositories.MaterialRepository;
using Misa.Cukcuk.Repositories.WarehouseRepository;
using Misa.CukCuk.Materials.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;

namespace Misa.CukCuk.Materials.Services.WarehouseService
{
    public class WarehouseService : BaseService<Warehouse>, IWarehouseService
    {
        #region Field

        private readonly IWarehouseRepository _warehouseRepository;

        #endregion

        #region Constructor

        public WarehouseService(IWarehouseRepository warehouseRepository) : base(warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        #endregion
    }
}
