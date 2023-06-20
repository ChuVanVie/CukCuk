using Misa.CukCuk.Helpers.Entities;
using Misa.Cukcuk.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misa.CukCuk.Helpers.Entities.DTO;

namespace Misa.Cukcuk.Repositories.UnitConvertionRepository
{
    public interface IUnitConvertionRepository : IBaseRepository<UnitConvertion>
    {
        public List<UnitConvertion> GetConvertionByMaterial(Guid materialId);

        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin đơn vị chuyển đổi
        /// </summary>
        /// <param name="unitConvertion">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult InsertRecord(UnitConvertion unitConvertion);

        /// <summary>
        /// Hàm gọi tới database sửa thông tin đơn vị chuyển đổi từ Id
        /// </summary>
        /// <param name="unitConvertionId">Id của đơn vị chuyển đổi cần sửa</param>
        /// <param name="unitConvertion">Đối tượng đơn vị chuyển đổi mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult UpdateRecord(Guid unitConvertionId, UnitConvertion unitConvertion);
    }
}
