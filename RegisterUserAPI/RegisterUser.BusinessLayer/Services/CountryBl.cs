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
        private readonly IRepository<Country> _countryRepository;

        public CountryBl(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public async Task<bool> IsValidCountry(int countryId)
        {
            return await _countryRepository.FindBy(e => e.CountryId == countryId).AnyAsync();
        }
    }
}
