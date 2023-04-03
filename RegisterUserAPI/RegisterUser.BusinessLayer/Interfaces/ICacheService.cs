using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegisterUser.BusinessLayer.Interfaces
{
    public interface ICacheService
    {
        Task<List<Country>> GetCountriesAsync(bool shouldUpdateCache = false);
        Task<List<Province>> GetProvincesByCountryIdAsync(int countryId, bool shouldUpdateCache = false);
        Task<List<UserDetail>> GetUsersAsync(bool shouldUpdateCache = false);

    }
}