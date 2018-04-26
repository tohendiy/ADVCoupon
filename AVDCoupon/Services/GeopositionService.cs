using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.Services.Interfaces;
using AVDCoupon.Data;
using Microsoft.EntityFrameworkCore;

namespace ADVCoupon.Services
{
    public class GeopositionService : IGeopositionService
    {
        private ApplicationDbContext _context;
        public GeopositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Geoposition> CreateGeopositionAsync(Geoposition geoposition)
        {
            geoposition.Id = Guid.NewGuid();
            _context.Add(geoposition);
            await _context.SaveChangesAsync();
            return geoposition;
        }

        public async Task DeleteGeopositionAsync(Guid Id)
        {
            var geoposition = await _context.Geopositions.SingleOrDefaultAsync(m => m.Id == Id);
            _context.Geopositions.Remove(geoposition);
            await _context.SaveChangesAsync();
        }

        public async Task<Geoposition> GetGeoposition(Guid Id)
        {
            var geoposition = await _context.Geopositions
               .SingleOrDefaultAsync(m => m.Id == Id);
            return geoposition;
        }

        public async Task<List<Geoposition>> GetGeopositionsAsync()
        {
            var geopositions = await _context.Geopositions.ToListAsync();
            return geopositions;
        }

        public bool IsExist(Guid Id)
        {
            return _context.Geopositions.Any(e => e.Id == Id);
        }

        public async Task UpdateGeopositionAsync(Geoposition geoposition)
        {
            _context.Update(geoposition);
            await _context.SaveChangesAsync();
        }
    }
}
