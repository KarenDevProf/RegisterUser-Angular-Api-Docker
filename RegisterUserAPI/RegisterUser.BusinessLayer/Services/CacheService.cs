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

        private const string CountryListCacheKey = "CountryCacheKey";
        private const string ProvinceByCountryCacheKey = "ProvinceByCountryCacheKey";
        private const string UsersCacheKey = "UsersCacheKey";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

        public CacheService(ICountry country, IProvince province, IUserDetail userDetail, IMemoryCache memoryCache)
        {
            _country = country;
            _province = province;
            _userDetail = userDetail;
            _memoryCache = memoryCache;
        }

        public async Task<List<Country>> GetCountriesAsync(bool shouldUpdateCache = false)
        {
            if (!shouldUpdateCache && _memoryCache.TryGetValue(CountryListCacheKey, out List<Country> cachedCountryList))
            {
                return cachedCountryList;
            }

            var countryListResponse = await _country.GetCountriesAsync();
            if (countryListResponse?.Count > 0)
            {
                _memoryCache.Set(CountryListCacheKey, countryListResponse, CacheDuration);
            }

            return countryListResponse;
        }

        public async Task<List<Province>> GetProvincesByCountryIdAsync(int countryId, bool shouldUpdateCache = false)
        {
            var provinceCacheKey = $"{ProvinceByCountryCacheKey}_{countryId}";

            if (!shouldUpdateCache && _memoryCache.TryGetValue(provinceCacheKey, out List<Province> cachedProvinceList))
            {
                return cachedProvinceList;
            }

            var provinceListResponse = await _province.GetProvincesByCountryIdAsync(countryId);
            if (provinceListResponse?.Count > 0)
            {
                _memoryCache.Set(provinceCacheKey, provinceListResponse, CacheDuration);
            }

            return provinceListResponse;
        }

        public async Task<List<UserDetail>> GetUsersAsync(bool shouldUpdateCache = false)
        {
            if (!shouldUpdateCache && _memoryCache.TryGetValue(UsersCacheKey, out List<UserDetail> cachedUsersList))
            {
                return cachedUsersList;
            }

            var usersListResponse = await _userDetail.GetUsersAsync();
            if (usersListResponse?.Count > 0)
            {
                _memoryCache.Set(UsersCacheKey, usersListResponse, CacheDuration);
            }

            return usersListResponse;
        }
    }
}
