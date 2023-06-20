using Dapper;
using Misa.CukCuk.Helpers.Entities.DTO;
using MySqlConnector;
using System.Data;
using static Dapper.SqlMapper;

namespace Misa.Cukcuk.Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Method

        /// <summary>
        /// Hàm khởi tạo connection
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: cvviet (22/05/2023)
        public IDbConnection GetOpenConnection()
        {
            var mySqlConnection = new MySqlConnection(DatabaseConnect.connectionString);
            mySqlConnection.Open();
            return mySqlConnection;
        }

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
        public List<T> Query<T>(IDbConnection cnn, string sql, object? param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return (List<T>)cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

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
        public T QueryFirstOrDefault(IDbConnection cnn, string sql, object? param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

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
        public GridReader QueryMultiple(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }

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
        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Hàm trả về tất cả các bản ghi T 
        /// </summary>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public List<T> GetAllRecords()
        {
            //Validate dữ liệu đầu 
            //Chuẩn bị tên stored procedure
            string storedProcedureName = $"Proc_{typeof(T).Name}_GetAll";
            //Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();

            //Thực hiện gọi vào database để chạy stored procedure
            var record = Query<T>(dbConnection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //Đóng kết nối tới db
            dbConnection.Close();

            return record;

        }

        /// <summary>
        /// Hàm trả về tất cả các bản ghi thỏa mãn theo tìm kiếm và phân trang
        /// </summary>
        /// <param name="filterString">chuỗi tìm kiếm theo mã, tên hoặc SĐT nhân viên</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <returns>(Tổng số bản ghi thỏa mãn, List các bản ghi đối tượng T thỏa mãn)</returns>
        /// Created by: CVVIET (22/05/2023)
        public PageResult<T> GetPaging(PageRequest pageRequest)
        {
            //Validate dữ liệu đầu vào

            //Chuẩn bị tên stored procedure
            string storedProcedureName;

            //Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            if ((pageRequest.stringCode == null) && (pageRequest.stringName == null) && (pageRequest.stringAddress == null) && (pageRequest.stringProperty == null) 
                && (pageRequest.stringUnit == null) && (pageRequest.stringMaterialgroup == null) && (pageRequest.stringNote == null) &&(pageRequest.stringStopFollow == null))
            {
                //Procedure xử lý phân trang
                storedProcedureName = $"Proc_{typeof(T).Name}_GetByPagination";
            }
            else
            {
                //Procedure xử lý tìm kiếm và phân trang
                storedProcedureName = $"Proc_{typeof(T).Name}_GetByPaginationAndFilter";
            }

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();

            //Thực hiện gọi vào database để chạy stored procedure
            var multi = QueryMultiple(dbConnection, storedProcedureName, pageRequest, commandType: System.Data.CommandType.StoredProcedure);
            //Lưu kết quả trả về vào pagingresult(tổng số bản ghi thỏa mãn, list các nhân viên thỏa mãn)
            var result = new PageResult<T>
            {
                TotalRecord = multi.Read<int>().Single(),
                ListData = multi.Read<T>().ToList()
            };

            //Đóng kết nối tới db
            dbConnection.Close();

            return result;
        }


        /// <summary>
        /// Hàm trả về số bản ghi đã thêm vào db (1)
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public virtual ServiceResult InsertRecord(T record)
        {
            return new ServiceResult();
        }

        /// <summary>
        /// Hàm trả về số bản ghi đã sửa trong db (1)
        /// </summary>
        /// <param name="recordId">Id của đối tượng T cần sửa</param>
        /// <param name="record">Đối tượng T mới với các trường đã được sửa</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public virtual ServiceResult UpdateRecord(Guid recordId, T record)
        {
            return new ServiceResult();
        }

        /// <summary>
        /// Hàm trả về số bản ghi đã xóa khỏi db (1)
        /// </summary>
        /// <param name="recordId">Id của nhân viên cần xóa</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public int DeleteRecord(Guid recordId)
        {
            //Validate dữ liệu đầu 
            //Chuẩn bị tên stored procedure
            string storedProcedureName = $"Proc_{typeof(T).Name}_Delete";

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();
            // bắt đầu transaction
            IDbTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();
                parameters.Add($"delete{typeof(T).Name}Id", recordId);

                //Thực hiện gọi vào database để chạy stored procedure
                var rowsAffected = Execute(dbConnection, storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                // nếu các thao tác đều thành công, commit transaction
                transaction.Commit();
                Console.WriteLine("Transaction committed.");

                return rowsAffected;
            }

            catch (Exception ex)
            {
                // nếu có lỗi, rollback transaction
                transaction.Rollback();
                Console.WriteLine("Transaction rolled back. " + ex.Message);

                throw ex;
            }

            finally
            {
                //Đóng kết nối tới db
                dbConnection.Close();
            }
        }

        #endregion
    }
}
