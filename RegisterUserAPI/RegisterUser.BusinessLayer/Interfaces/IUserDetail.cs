using Microsoft.AspNetCore.JsonPatch;
using RegisterUser.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RegisterUser.BusinessLayer.Models.User.RequestModels;

namespace RegisterUser.BusinessLayer.Interfaces
{
    public interface IUserDetail
    {
        Task<List<UserDetail>> GetUsersAsync();
        Task<UserDetail> CreateUserAsync(CreateUserRequest createUserRequest);
    }
}