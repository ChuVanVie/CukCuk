using System.ComponentModel.DataAnnotations;

namespace Misa.CukCuk.Materials.Helpers.Entities
{
    public class Warehouse
    {

        /// <summary>
        /// Id kho
        /// </summary>
        [Required]
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// Mã kho
        /// </summary>
        [Required]
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Tên kho
        /// </summary>
        [Required]
        public string WarehouseName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? WarehouseAddress { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Explanation { get; set; }

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
        public DateTime? ModifiedDate { get; set; }

    }
}
