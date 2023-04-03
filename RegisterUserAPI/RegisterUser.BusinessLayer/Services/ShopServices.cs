using RegisterUser.BusinessLayer.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterUser.BusinessLayer.Services
{
    public class RegisterUserServices : IRegisterUserServices
    {
        readonly IServiceProvider _serviceProvider;
        public RegisterUserServices(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public T GetService<T>() => _serviceProvider.GetRequiredService<T>();
    }
}
