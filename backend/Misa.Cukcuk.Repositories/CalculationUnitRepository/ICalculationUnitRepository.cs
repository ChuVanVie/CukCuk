using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Materials.Helpers.Entities;

namespace Misa.Cukcuk.Repositories.CalculationUnitRepository
{
    public interface ICalculationUnitRepository : IBaseRepository<CalculationUnit>
    {
        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin đơn vị tính
        /// </summary>
        /// <param name="calculationUnit">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult InsertRecord(CalculationUnit calculationUnit);

        /// <summary>
        /// Hàm gọi tới database sửa thông tin đơn vị tính từ Id
        /// </summary>
        /// <param name="calculationUnitId">Id của đơn vị tính cần sửa</param>
        /// <param name="calculationUnit">Đối tượng đơn vị tính mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult UpdateRecord(Guid calculationUnitId, CalculationUnit calculationUnit);
    }
}
