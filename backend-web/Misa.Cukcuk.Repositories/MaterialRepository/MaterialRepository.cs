using Dapper;
using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Materials.Helpers.Entities;
using System.Data;

namespace Misa.Cukcuk.Repositories.MaterialRepository
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        #region Method

        /// <summary>
        /// Xử lý lấy mã nguyên vật liệu mới (lớn nhất trong db)
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: CVVIET (22/05/2023)
        public Material GetNewMaterialCode()
        {
            //Validate dữ liệu đầu 
            //Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Material_GetNewCode";
            //Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();

            //Thực hiện gọi vào database để chạy stored procedure
            var record = QueryFirstOrDefault(dbConnection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //Đóng kết nối tới db
            dbConnection.Close();

            return record;
        }

        #endregion

        #region Override


        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin nguyên vật liệu
        /// </summary>
        /// <param name="material">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public override ServiceResult InsertRecord(Material material)
        {
            //Validate dữ liệu đầu 

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();
            // bắt đầu transaction
            IDbTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tên stored procedure
                string storedProcedureName = $"Proc_Material_Post";

                //Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();

                //Thực hiện gọi vào database để chạy stored procedure
                var rowsAffected = Execute(dbConnection, storedProcedureName, material, transaction, commandType: System.Data.CommandType.StoredProcedure);

                // nếu các thao tác đều thành công, commit transaction
                transaction.Commit();
                Console.WriteLine("Transaction committed.");

                if (rowsAffected > 0)
                {
                    return new ServiceResult
                    {
                        isSuccess = true,
                        message = Resources.ServiceResult_Msg_InsertResolve,
                        data = ""
                    };
                }
                else
                {
                    return new ServiceResult
                    {
                        isSuccess = false,
                        message = Resources.ServiceResult_Msg_InsertReject,
                        data = ""
                    };
                }
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


        /// <summary>
        /// Hàm gọi tới database sửa thông tin nguyên vật liệu từ Id
        /// </summary>
        /// <param name="materialId">Id của nguyên vật liệu cần sửa</param>
        /// <param name="material">Đối tượng nguyên vật liệu mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public override ServiceResult UpdateRecord(Guid materialId, Material material)
        {
            //Validate dữ liệu đầu 

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();
            // bắt đầu transaction
            IDbTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tên stored procedure
                string storedProcedureName = $"Proc_Material_Update";

                //Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();

                //Thực hiện gọi vào database để chạy stored procedure
                var rowsAffected = Execute(dbConnection, storedProcedureName, material, transaction, commandType: System.Data.CommandType.StoredProcedure);

                // nếu các thao tác đều thành công, commit transaction
                transaction.Commit();
                Console.WriteLine("Transaction committed.");

                if (rowsAffected > 0)
                {
                    return new ServiceResult
                    {
                        isSuccess = true,
                        message = Resources.ServiceResult_Msg_InsertResolve,
                        data = ""
                    };
                }
                else
                {
                    return new ServiceResult
                    {
                        isSuccess = false,
                        message = Resources.ServiceResult_Msg_InsertReject,
                        data = ""
                    };
                }
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
