using Microsoft.AspNetCore.Mvc;
using RegisterUser.BusinessLayer.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using RegisterUser.API.Models;
using RegisterUser.BusinessLayer.Models.LocationData.ResponseModels;

namespace RegisterUser.API.Controllers
{
    public class LocationDataController : RegisterBaseController
    {
        private readonly ICacheService _cacheService;

        public LocationDataController(IRegisterUserServices services, ICacheService cacheService) : base(services)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("getCountries")]
        public async Task<ResponseObjectModel<List<LocationDataModel>>> GetCountries()
        {
            ResponseObjectModel<List<LocationDataModel>> responseObject = new ResponseObjectModel<List<LocationDataModel>>();
            var allCountries = await _cacheService.GetCountriesAsync();
            responseObject.Data = Mapper.Map<List<LocationDataModel>>(allCountries);
            return responseObject;
        }

        [HttpGet]
        [Route("getProvincesByCountry/{countryId}")]
        public async Task<ResponseObjectModel<List<LocationDataModel>>> GetProvincesByCountry(int countryId)
        {
            ResponseObjectModel<List<LocationDataModel>> responseObject = new ResponseObjectModel<List<LocationDataModel>>();
            var allProvincesByCountry = await _cacheService.GetProvincesByCountryIdAsync(countryId);
            responseObject.Data = Mapper.Map<List<LocationDataModel>>(allProvincesByCountry);
            return responseObject;
        }
    }
}