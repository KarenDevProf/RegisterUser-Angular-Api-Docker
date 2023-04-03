using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RegisterUser.BusinessLayer.Exceptions;
using RegisterUser.BusinessLayer.Interfaces;

namespace RegisterUser.API.Controllers
{
    public class RegisterBaseController : Controller
    {
        private readonly IRegisterUserServices _services;
        protected IMapper Mapper => _services.GetService<IMapper>();
        public RegisterBaseController(IRegisterUserServices services)
        {
            this._services = services;
        }
        protected void CheckModelState()
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException();
        }
    }
}
