using System.ComponentModel.DataAnnotations;

namespace Misa.CukCuk.Materials.Helpers.Entities
{
    public class CalculationUnit
    {
        /// <summary>
        /// Id đơn vị tính
        /// </summary>
        [Required]
        public Guid CalculationUnitId { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        [Required]
        public string CalculationUnitName { get; set; }

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
