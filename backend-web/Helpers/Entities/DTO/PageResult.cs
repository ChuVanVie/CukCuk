using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Helpers.Entities.DTO
{
    public class PageResult<T>
    {
        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// List employee thỏa mãn
        /// </summary>
        public List<T> ListData { get; set; }
    }
}
