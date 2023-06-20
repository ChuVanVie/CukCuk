using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Materials.Helpers.Entities;

namespace Misa.Cukcuk.Repositories.WarehouseRepository
{
    public interface IWarehouseRepository : IBaseRepository<Warehouse>
    {
        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin kho ngầm định
        /// </summary>
        /// <param name="warehouse">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult InsertRecord(Warehouse warehouse);

        /// <summary>
        /// Hàm gọi tới database sửa thông tin kho ngầm định từ Id
        /// </summary>
        /// <param name="warehouseId">Id của kho ngầm định cần sửa</param>
        /// <param name="warehouse">Đối tượng kho ngầm định mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult UpdateRecord(Guid warehouseId, Warehouse warehouse);
    }
}
