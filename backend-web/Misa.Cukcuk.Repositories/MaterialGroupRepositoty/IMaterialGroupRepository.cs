using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Materials.Helpers.Entities;

namespace Misa.Cukcuk.Repositories.MaterialGroupRepositoty
{
    public interface IMaterialGroupRepository : IBaseRepository<MaterialGroup>
    {
        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin nhóm NVL
        /// </summary>
        /// <param name="materialGroup">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult InsertRecord(MaterialGroup materialGroup);

        /// <summary>
        /// Hàm gọi tới database sửa thông tin nhóm NVL từ Id
        /// </summary>
        /// <param name="materialGroupId">Id của nhóm NVL cần sửa</param>
        /// <param name="materialGroup">Đối tượng nhóm NVL mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult UpdateRecord(Guid materialGroupId, MaterialGroup materialGroup);
    }
}
