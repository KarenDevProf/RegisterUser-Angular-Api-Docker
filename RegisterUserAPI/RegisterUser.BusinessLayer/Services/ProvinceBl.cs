using Microsoft.EntityFrameworkCore;
using RegisterUser.BusinessLayer.Interfaces;
using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RegisterUser.Repositories.Repositories.Interfaces;
using RegisterUser.BusinessLayer.Exceptions;

namespace RegisterUser.BusinessLayer.Services
{
    public class ProvinceBl : IProvince
    {
        private readonly IRepository<Province> _provinceRepository;
        private readonly ICountry _countryService;

        public ProvinceBl(IRepository<Province> provinceRepository, ICountry countryService)
        {
            _provinceRepository = provinceRepository;
            _countryService = countryService;
        }

        public async Task<List<Province>> GetProvincesByCountryIdAsync(int countryId)
        {
            if (!await _countryService.IsValidCountry(countryId))
            {
                throw new NotFoundException(nameof(Country));
            }

            return await _provinceRepository.FindBy(p => p.CountryId == countryId).ToListAsync();
        }

        public async Task<bool> IsValidProvinceForCountry(int countryId, int provinceId)
        {
            return await _provinceRepository.FindBy(p => p.CountryId == countryId && p.ProvinceId == provinceId).AnyAsync();
        }
    }
}
