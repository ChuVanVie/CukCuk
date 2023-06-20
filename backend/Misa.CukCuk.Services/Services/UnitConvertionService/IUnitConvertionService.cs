using Misa.CukCuk.Helpers.Entities;
using Misa.CukCuk.Materials.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.CukCuk.Services.Services.UnitConvertionService
{
    public interface IUnitConvertionService : IBaseService<UnitConvertion>
    {
        public List<UnitConvertion> GetConvertionByMaterial(Guid materialId);
    }
}
