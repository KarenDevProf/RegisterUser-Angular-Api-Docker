using Microsoft.AspNetCore.Mvc;
using RegisterUser.BusinessLayer.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using RegisterUser.API.Models;
using RegisterUser.BusinessLayer.Models.Users.ResponseModels;
using RegisterUser.BusinessLayer.Models.User.RequestModels;

namespace RegisterUser.API.Controllers
{

    [Route("[controller]/[action]")]
    public class UserController : RegisterBaseController
    {
        private readonly IUserDetail _userDetail;
        private readonly ICacheService _cacheService;

        public UserController(IRegisterUserServices services, ICacheService cacheService) : base(services)
        {
            _userDetail = services.GetService<IUserDetail>();
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ResponseObjectModel<List<UserResponseModel>>> GetUsers()
        {
            ResponseObjectModel<List<UserResponseModel>> responseObject = new ResponseObjectModel<List<UserResponseModel>>();
            var allUsers = await _cacheService.GetUsersAsync();
            responseObject.Data = Mapper.Map<List<UserResponseModel>>(allUsers);
            return responseObject;
        }

        [HttpPost]
        public async Task<ResponseObjectModel<UserResponseModel>> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            ResponseObjectModel<UserResponseModel> responseObject = new ResponseObjectModel<UserResponseModel>();
            CheckModelState();

            var createdUser = await _userDetail.CreateUserAsync(createUserRequest);

            responseObject.Data = Mapper.Map<UserResponseModel>(createdUser);
            return responseObject;
        }
    }
}