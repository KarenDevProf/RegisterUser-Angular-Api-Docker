using RegisterUser.BusinessLayer.Interfaces;
using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RegisterUser.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RegisterUser.BusinessLayer.Services
{
    public class CountryBl : ICountry
    {
        private readonly IRepository<Country> _country;

        public CountryBl(IRepository<Country> country)
        {
            _country = country;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _country.GetAllAsync();
        }

        public async Task<bool> IsValidCountry(int countryId)
        {
            return await _country.FindBy(e => e.CountryId == countryId).FirstOrDefaultAsync() != null;
        }

    }
}
