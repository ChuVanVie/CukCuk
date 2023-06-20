using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Materials.Helpers.Entities;

namespace Misa.Cukcuk.Repositories.MaterialRepository
{
    public interface IMaterialRepository : IBaseRepository<Material>
    {
        /// <summary>
        /// Xử lý lấy mã nguyên vật liệu mới (lớn nhất trong db)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: CVVIET (22/05/2023)
        public Material GetNewMaterialCode();

        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin nguyên vật liệu
        /// </summary>
        /// <param name="material">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult InsertRecord(Material material);

        /// <summary>
        /// Hàm gọi tới database sửa thông tin nguyên vật liệu từ Id
        /// </summary>
        /// <param name="materialId">Id của nguyên vật liệu cần sửa</param>
        /// <param name="material">Đối tượng nguyên vật liệu mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult UpdateRecord(Guid materialId, Material material);
    }
}
