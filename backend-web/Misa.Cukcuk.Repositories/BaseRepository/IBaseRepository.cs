using Misa.CukCuk.Helpers.Entities.DTO;
using System.Data;
using static Dapper.SqlMapper;

namespace Misa.Cukcuk.Repositories.BaseRepository
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Hàm khởi tạo connection
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: cvviet (22/05/2023)
        public IDbConnection GetOpenConnection();

        /// <summary>
        /// Hàm truy vấn tất cả các bản ghi thỏa mãn
        /// </summary>
        /// <typeparam name=""></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// CreatedBy: cvviet (22/05/2023)
        public List<T> Query<T>(IDbConnection cnn, string sql, object? param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm truy vấn bản ghi đầu tiên thỏa mãn
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// CreatedBy: cvviet (22/05/2023)
        public T QueryFirstOrDefault(IDbConnection cnn, string sql, object? param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm truy vấn tính ra số bản ghi ảnh hưởng
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// CreatedBy: cvviet (22/05/2023)
        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Hàm thực hiện nhiều truy vấn truy vấn
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// CreatedBy: cvviet (22/05/2023)

        public GridReader QueryMultiple(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

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
