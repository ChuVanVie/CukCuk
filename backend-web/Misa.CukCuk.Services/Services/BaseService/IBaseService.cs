using Misa.CukCuk.Helpers.Entities.DTO;

namespace Misa.CukCuk.Materials.Services.BaseService
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Hàm trả về tất cả các bản ghi T 
        /// </summary>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public List<T> GetAllRecords();

        /// <summary>
        /// Hàm trả về tất cả các bản ghi thỏa mãn theo tìm kiếm và phân trang
        /// </summary>
        /// <param name="filterString">chuỗi tìm kiếm theo mã, tên hoặc SĐT nhân viên</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <returns>(Tổng số bản ghi thỏa mãn, List các bản ghi đối tượng T thỏa mãn)</returns>
        /// Created by: CVVIET (22/05/2023)
        public PageResult<T> GetPaging(PageRequest pageRequest);


        /// <summary>
        /// Hàm trả về số bản ghi đã thêm vào db (1)
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult InsertRecord(T record);

        /// <summary>
        /// Hàm trả về số bản ghi đã sửa trong db (1)
        /// </summary>
        /// <param name="recordId">Id của đối tượng T cần sửa</param>
        /// <param name="record">Đối tượng T mới với các trường đã được sửa</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult UpdateRecord(Guid recordId, T record);

        /// <summary>
        /// Hàm trả về số bản ghi đã xóa khỏi db (1)
        /// </summary>
        /// <param name="recordId">Id của đối tượng cần xóa</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public int DeleteRecord(Guid recordId);
    }
}
