namespace CityInfo.Api.Repositories
{
    using CityInfo.Api.DbContexts;
    using CityInfo.Api.Entities;
    using CityInfo.Api.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities() => _context.Cities
            .OrderBy(c => c.Name)
            .ToList();

        public City GetCity(int cityId, bool includePointOfInterest)
        {
            var city = _context.Cities
                .Where(c => c.Id == cityId);
            if (includePointOfInterest)
                city = city.Include(p => p.PointsOfInterest);

            return city.FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterest)
            => _context.PointsOfInterest
                .Where(p => p.CityId == cityId && p.Id == pointOfInterest)
                .FirstOrDefault();

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
            => _context.PointsOfInterest
                .Where(p => p.CityId == cityId)
                .ToList();
    }
}