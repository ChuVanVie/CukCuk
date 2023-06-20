using System.ComponentModel.DataAnnotations;

namespace Misa.CukCuk.Materials.Helpers.Entities
{
    public class MaterialGroup
    {
        /// <summary>
        /// Id nhóm nguyên vật liệu
        /// </summary>
        [Required]
        public Guid MaterialGroupId { get; set; }

        /// <summary>
        /// Mã nhóm NVL
        /// </summary>
        [Required]
        public string MaterialGroupCode { get; set; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        [Required]
        public string GroupName { get; set; }

        /// <summary>
        /// Thuộc nhóm
        /// </summary>
        public string? ParentGroupName { get; set; }

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
