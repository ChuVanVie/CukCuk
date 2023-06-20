using System.ComponentModel.DataAnnotations;

namespace Misa.CukCuk.Materials.Helpers.Entities
{
    public class Material
    {
        /// <summary>
        /// Id nguyên vật liệu
        /// </summary>
        [Required]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// Mã NVL
        /// </summary>
        [Required]
        public string MaterialCode { get; set; }

        /// <summary>
        /// Tên NVL
        /// </summary>
        [Required]
        public string MaterialName { get; set; }

        /// <summary>
        /// Thuộc tính
        /// </summary>
        [Required]
        public string MaterialProperty { get; set; }

        /// <summary>
        /// Id đơn vị tính
        /// </summary>
        [Required]
        public Guid CalculationUnitId { get; set; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        public string CalculationUnitName { get; set; }

        /// <summary>
        /// Id nhóm NVL
        /// </summary>
        public Guid? MaterialGroupId { get; set; }

        /// <summary>
        /// Nhóm NVL
        /// </summary>
        public string? GroupName { get; set; }

        /// <summary>
        /// Id kho ngầm định
        /// </summary>
        public Guid? WarehouseId { get; set; }

        /// <summary>
        /// Kho ngầm định
        /// </summary>
        public string? WarehouseName { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public int? Expiry { get; set; }

        public string? ExpiryText { get; set; }

        /// <summary>
        /// Số lượng tồn tối thiểu
        /// </summary>
        public double? MinimumStock { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Ngừng theo dõi
        /// </summary>
        public int? StopFollowing { get; set; }

        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người thay đổi gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày thay đổi gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set;}
    }
}
