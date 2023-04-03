using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegisterUser.BusinessLayer.Interfaces
{
    public interface IProvince
    {
        Task<List<Province>> GetProvincesByCountryIdAsync(int countryId);
        Task<bool> IsValidProvinceForCountry(int countryId, int provinceId);
    }
}