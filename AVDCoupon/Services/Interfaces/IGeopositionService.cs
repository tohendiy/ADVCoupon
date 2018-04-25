using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ADVCoupon.Models;

namespace ADVCoupon.Services.Interfaces
{
    public interface IGeopositionService
    {
        Task<Geoposition> GetGeoposition(Guid Id);

        Task<List<Geoposition>> GetGeopositionsAsync();

        Task<Geoposition> CreateGeopositionAsync(Geoposition geoposition);

        Task UpdateGeopositionAsync(Geoposition geoposition);

        Task DeleteGeopositionAsync(Guid Id);

        bool IsExist(Guid Id);
    }
}
