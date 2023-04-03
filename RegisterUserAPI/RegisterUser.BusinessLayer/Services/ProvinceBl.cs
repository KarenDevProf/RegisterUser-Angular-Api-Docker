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
        private readonly IRepository<Province> _province;
        private readonly ICountry _country;

        public ProvinceBl(IRepository<Province> province, ICountry country)
        {
            _province = province;
            _country = country;
        }

        public async Task<List<Province>> GetProvincesByCountryIdAsync(int countryId)
        {
            var isValidCountry = await _country.IsValidCountry(countryId);
            if (!isValidCountry)
            {
                throw new NotFoundException("Country");
            }

            return await _province.FindBy(e => e.CountryId == countryId).ToListAsync();
        }

        public async Task<bool> IsValidProvinceForCountry(int countryId, int provinceId)
        {
            return await _province.FindBy(e => e.CountryId == countryId && e.ProvinceId == provinceId).FirstOrDefaultAsync() != null;
        }

    }
}
