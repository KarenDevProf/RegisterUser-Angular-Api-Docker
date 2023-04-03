using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegisterUser.BusinessLayer.Interfaces
{
    public interface ICountry
    {
        Task<List<Country>> GetCountriesAsync();
        Task<bool> IsValidCountry(int countryId);

    }
}