using Microsoft.EntityFrameworkCore;
using RegisterUser.BusinessLayer.Interfaces;
using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RegisterUser.Repositories.Repositories.Interfaces;
using RegisterUser.BusinessLayer.Exceptions;
using RegisterUser.BusinessLayer.Models.User.RequestModels;

namespace RegisterUser.BusinessLayer.Services
{
    public class UserDetailBl : IUserDetail
    {
        private readonly IRepository<UserDetail> _userDetail;
        private readonly ICountry _country;
        private readonly IProvince _province;

        public UserDetailBl(IRepository<UserDetail> userDetail, ICountry country, IProvince province)
        {
            _userDetail = userDetail;
            _country = country;
            _province = province;
        }

        public async Task<List<UserDetail>> GetUsersAsync()
        {
            return await _userDetail.GetAll().Include(e => e.Province).ThenInclude(e => e.Country).ToListAsync();
        }

        public async Task<UserDetail> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var isFreeEmail = await IsFreeEmail(createUserRequest.Email.ToLower());
            if (!isFreeEmail)
            {
                throw new EmailUsedException();
            }

            var isValidCountry = await _country.IsValidCountry(createUserRequest.CountryId);
            if (!isValidCountry)
            {
                throw new NotFoundException("Country");
            }

            var isValidProvinceForCountry = await _province.IsValidProvinceForCountry(createUserRequest.CountryId, createUserRequest.ProvinceId);

            if (!isValidProvinceForCountry)
            {
                throw new NotFoundException("Province");
            }

            var user = new UserDetail()
            {
                Login = createUserRequest.Email,
                Password = createUserRequest.Password,
                ProvinceId = createUserRequest.ProvinceId
            };

            var newUser = await _userDetail.AddAsync(user);

            await _userDetail.SaveChangesAsync();

            return newUser;
        }

        private async Task<bool> IsFreeEmail(string login)
        {
            return await _userDetail.FindBy(e => e.Login == login).FirstOrDefaultAsync() == null;
        }

    }
}
