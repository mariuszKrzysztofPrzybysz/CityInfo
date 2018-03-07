namespace CityInfo.Api.Interfaces
{
    using CityInfo.Api.Entities;
    using System.Collections.Generic;

    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool includePointOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterest);
    }
}