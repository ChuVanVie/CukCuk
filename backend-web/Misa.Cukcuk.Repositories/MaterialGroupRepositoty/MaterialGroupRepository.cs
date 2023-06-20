using Dapper;
using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Materials.Helpers.Entities;
using System.Data;

namespace Misa.Cukcuk.Repositories.MaterialGroupRepositoty
{
    public class MaterialGroupRepository : BaseRepository<MaterialGroup>, IMaterialGroupRepository
    {
        #region Override

        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin nhóm NVL
        /// </summary>
        /// <param name="materialGroup">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public override ServiceResult InsertRecord(MaterialGroup materialGroup)
        {
            //Validate dữ liệu đầu 

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();
            // bắt đầu transaction
            IDbTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tên stored procedure
                string storedProcedureName = "Proc_MaterialGroup_Post";

                //Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();

                //Thực hiện gọi vào database để chạy stored procedure
                var record = QueryFirstOrDefault(dbConnection, storedProcedureName, materialGroup, transaction, commandType: System.Data.CommandType.StoredProcedure);

                // nếu các thao tác đều thành công, commit transaction
                transaction.Commit();
                Console.WriteLine("Transaction committed.");

                if (record != null)
                {
                    return new ServiceResult
                    {
                        isSuccess = true,
                        message = Resources.ServiceResult_Msg_InsertResolve,
                        data = record
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
        /// Hàm gọi tới database sửa thông tin nhóm NVL từ Id
        /// </summary>
        /// <param name="materialGroupId">Id của nhóm NVL cần sửa</param>
        /// <param name="materialGroup">Đối tượng nhóm NVL mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public override ServiceResult UpdateRecord(Guid materialGroupId, MaterialGroup materialGroup)
        {
            //Validate dữ liệu đầu 

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();
            // bắt đầu transaction
            IDbTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tên stored procedure
                string storedProcedureName = $"Proc_MaterialGroup_Update";

                //Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();

                //Thực hiện gọi vào database để chạy stored procedure
                var rowsAffected = Execute(dbConnection, storedProcedureName, materialGroup, transaction, commandType: System.Data.CommandType.StoredProcedure);

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
