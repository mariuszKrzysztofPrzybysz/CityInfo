using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CityInfo.Api.DataStores;
using CityInfo.Api.DataTransferObjects;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/pointsOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsOfInterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var pointOfInterest = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId)
                ?.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointsOfInterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if ((pointOfInterest.Name ?? string.Empty).Equals(pointOfInterest.Description, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities
                .SelectMany(c => c.PointsOfInterest)
                .Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new { cityId, id = finalPointOfInterest.Id },
                finalPointOfInterest);
        }

        [HttpPut("{cityId}/pointsOfInterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if ((pointOfInterest.Name ?? string.Empty).Equals(pointOfInterest.Description, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pointOfInterestFromStore = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId)
                ?.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsOfInterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var pointFromInterestFromStore = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId)
                ?.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointFromInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto
            {
                Name = pointFromInterestFromStore.Name,
                Description = pointFromInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if ((pointOfInterestToPatch.Name ?? string.Empty).Equals(pointOfInterestToPatch.Description, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointFromInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointFromInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsOfInterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var cityFromStore = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (cityFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = cityFromStore.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            cityFromStore.PointsOfInterest.Remove(pointOfInterestFromStore);

            return NoContent();
        }
    }
}