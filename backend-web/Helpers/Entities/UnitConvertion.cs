using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Helpers.Entities
{
    public class UnitConvertion
    {
        public Guid UnitConvertionId { get; set; }

        public Guid MaterialId { get; set; }

        public Guid CalculationUnitId { get; set; }

        public string? CalculationUnitName { get; set; }

        public int? Scale { get; set; }

        public string? Calculate { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
