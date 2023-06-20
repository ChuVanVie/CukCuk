using Dapper;
using Misa.Cukcuk.Repositories.BaseRepository;
using Misa.Cukcuk.Repositories.WarehouseRepository;
using Misa.CukCuk.Helpers;
using Misa.CukCuk.Helpers.Entities;
using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.CukCuk.Materials.Helpers.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Cukcuk.Repositories.UnitConvertionRepository
{
    public class UnitConvertionRepository : BaseRepository<UnitConvertion>, IUnitConvertionRepository
    {

        #region Method
        public List<UnitConvertion> GetConvertionByMaterial(Guid materialId)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_UnitConvertion_GetByMaterial";
            //Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("materialId", materialId);

            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();

            //Thực hiện gọi vào database để chạy stored procedure
            var record = Query<UnitConvertion>(dbConnection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //Đóng kết nối tới db
            dbConnection.Close();

            return record;
        } 
        #endregion

        #region Override

        /// <summary>
        /// Hàm gọi tới database thêm mới thông tin đơn vị chuyển đổi
        /// </summary>
        /// <param name="unitConvertion">Các tham số tương ứng với các trường thông tin</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public override ServiceResult InsertRecord(UnitConvertion unitConvertion)
        {
            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();
            // bắt đầu transaction
            IDbTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tên stored procedure
                string storedProcedureName = "Proc_UnitConvertion_Post";

                //Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();

                //Thực hiện gọi vào database để chạy stored procedure
                var rowEffected = Execute(dbConnection, storedProcedureName, unitConvertion, transaction, commandType: System.Data.CommandType.StoredProcedure);

                // nếu các thao tác đều thành công, commit transaction
                transaction.Commit();
                Console.WriteLine("Transaction committed.");

                if (rowEffected > 0)
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
        /// Hàm gọi tới database sửa thông tin đơn vị chuyển đổi từ Id
        /// </summary>
        /// <param name="unitConvertionId">Id của đơn vị chuyển đổi cần sửa</param>
        /// <param name="unitConvertion">Đối tượng đơn vị chuyển đổi mới với các trường đã được sửa</param>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public override ServiceResult UpdateRecord(Guid unitConvertionId, UnitConvertion unitConvertion)
        {
            //Khởi tạo kết nối tới database
            var dbConnection = GetOpenConnection();
            // bắt đầu transaction
            IDbTransaction transaction = dbConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tên stored procedure
                string storedProcedureName = "Proc_UnitConvertion_Update";

                //Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();

                //Thực hiện gọi vào database để chạy stored procedure
                var rowsAffected = Execute(dbConnection, storedProcedureName, unitConvertion, transaction, commandType: System.Data.CommandType.StoredProcedure);

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
