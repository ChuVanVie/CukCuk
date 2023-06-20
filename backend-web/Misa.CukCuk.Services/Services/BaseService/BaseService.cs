using Misa.CukCuk.Helpers.Entities.DTO;
using Misa.Cukcuk.Repositories.BaseRepository;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using Misa.CukCuk.Helpers;

namespace Misa.CukCuk.Materials.Services.BaseService
{
    public class BaseService<T> : IBaseService<T>
    {
        #region Field

        private IBaseRepository<T> _baseRepository;

        #endregion

        #region Contructor

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #endregion

        /// <summary>
        /// Hàm trả về tất cả các bản ghi T 
        /// </summary>
        /// <returns></returns>
        /// Created by: CVVIET (22/05/2023)
        public List<T> GetAllRecords()
        {
            return _baseRepository.GetAllRecords();
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
            return _baseRepository.GetPaging(pageRequest);
        }


        /// <summary>
        /// Hàm trả về số bản ghi đã thêm vào db (1)
        /// </summary>
        /// <param name="record">Bản ghi cần thêm</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)

        public ServiceResult InsertRecord(T record)
        {
            var validateFailures = ValidateRequestData(record);
            var validateFailuresCustom = ValidateRequestDataCustom(record);
            validateFailures.AddRange(validateFailuresCustom);
            if (validateFailures.Count > 0)
            {
                return new ServiceResult
                {
                    isSuccess = false,
                    message = Resources.Invalid_Data,
                    data = validateFailures
                };
            }

            return _baseRepository.InsertRecord(record);
        }


        /// <summary>
        /// Hàm trả về số bản ghi đã sửa trong db (1)
        /// </summary>
        /// <param name="recordId">Id của đối tượng T cần sửa</param>
        /// <param name="record">Đối tượng T mới với các trường đã được sửa</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public ServiceResult UpdateRecord(Guid recordId, T record)
        {
            var validateFailures = ValidateRequestData(record);
            var validateFailuresCustom = ValidateRequestDataCustom(record);
            validateFailures.AddRange(validateFailuresCustom);
            if (validateFailures.Count > 0)
            {
                return new ServiceResult
                {
                    isSuccess = false,
                    message = Resources.Invalid_Data,
                    data = validateFailures
                };
            }

            return _baseRepository.UpdateRecord(recordId, record);
        }

        /// <summary>
        /// Hàm trả về số bản ghi đã xóa khỏi db (1)
        /// </summary>
        /// <param name="recordId">Id của đối tượng cần xóa</param>
        /// <returns>int</returns>
        /// Created by: CVVIET (22/05/2023)
        public virtual int DeleteRecord(Guid recordId)
        {
            return _baseRepository.DeleteRecord(recordId);
        }

        /// <summary>
        /// Hàm validate các trường require của đối tượng
        /// </summary>
        /// <param name="record">Đối tượng cần kiểm tra</param>
        /// <returns>Mảng các lỗi</returns>
        /// Created by: CVVIET (22/05/2023)
        public List<string> ValidateRequestData(T record)
        {
            var validateFailures = new List<string>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(record);
                var requiredAttribute = (RequiredAttribute?)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                if (requiredAttribute != null && (propertyValue == null || propertyValue == ""))
                {
                    validateFailures.Add("Empty" + propertyName);
                }
            }
            return validateFailures;
        }


        /// <summary>
        /// Hàm validate các trường khác của đối tượng
        /// </summary>
        /// <param name="record">Đối tượng cần validate</param>
        /// <returns>List chứa các trường lỗi</returns>
        /// Created by: CVVIET (22/05/2023)
        public virtual List<string> ValidateRequestDataCustom(T record)
        {
            return new List<string>();
        }
    }
}
