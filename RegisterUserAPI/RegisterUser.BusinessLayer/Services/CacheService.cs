using RegisterUser.BusinessLayer.Interfaces;
using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace RegisterUser.BusinessLayer.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICountry _country;
        private readonly IProvince _province;
        private readonly IUserDetail _userDetail;
        private readonly IMemoryCache _memoryCache;

        public const string _CountryListCacheKey = "CountryCacheKey";
        public const string _ProvinceByCountryCacheKey = "ProvinceByCountryCacheKey";
        public const string _UsersCacheKey = "UsersCacheKey";

        public CacheService(ICountry country, IProvince province, IMemoryCache memoryCache, IUserDetail userDetail)
        {
            _country = country;
            _province = province;
            _memoryCache = memoryCache;
            _userDetail = userDetail;
        }

        public async Task<List<Country>> GetCountriesAsync(bool shouldUpdateCache = false)
        {
            if (!shouldUpdateCache && _memoryCache.TryGetValue($"{_CountryListCacheKey}", out List<Country> cachedCountryList))
            {
                return cachedCountryList;
            }

            var countryListResponse = await _country.GetCountriesAsync();
            if (countryListResponse?.Count > 0)
            {
                _memoryCache.Set($"{_CountryListCacheKey}", countryListResponse, TimeSpan.FromMinutes(10));
            }

            return countryListResponse;
        }

        public async Task<List<Province>> GetProvincesByCountryIdAsync(int countryId, bool shouldUpdateCache = false)
        {
            if (!shouldUpdateCache && _memoryCache.TryGetValue($"{_ProvinceByCountryCacheKey}_{countryId}", out List<Province> cachedProvinceByCountryId))
            {
                return cachedProvinceByCountryId;
            }

            var provinceListResponse = await _province.GetProvincesByCountryIdAsync(countryId);
            if (provinceListResponse?.Count > 0)
            {
                _memoryCache.Set($"{_ProvinceByCountryCacheKey}_{countryId}", provinceListResponse, TimeSpan.FromMinutes(10));
            }

            return provinceListResponse;
        }

        public async Task<List<UserDetail>> GetUsersAsync(bool shouldUpdateCache = false)
        {
            if (!shouldUpdateCache && _memoryCache.TryGetValue($"{_UsersCacheKey}", out List<UserDetail> cachedUsersList))
            {
                return cachedUsersList;
            }

            var usersListResponse = await _userDetail.GetUsersAsync();
            if (usersListResponse?.Count > 0)
            {
                _memoryCache.Set($"{_UsersCacheKey}", usersListResponse, TimeSpan.FromMinutes(10));
            }

            return usersListResponse;
        }
    }
}
