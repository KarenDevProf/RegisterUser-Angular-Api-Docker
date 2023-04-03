using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace RegisterUser.DAL.Models
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<RegisterUserContext>();

                if (context != null)
                {
                    context.Database.Migrate();

                    context.Database.EnsureCreated();

                    // Seed Countries
                    var countries = new List<Country>()
                        {
                            new Country { Name = "United States" },
                            new Country { Name = "Canada" },
                            new Country { Name = "Mexico" },
                            new Country { Name = "United Kingdom" }
                        };


                    // Seed Provinces
                    var provinces = new List<Province>()
                        {
                            new Province { Name = "California", CountryId = 1 },
                            new Province { Name = "New York", CountryId = 1 },
                            new Province { Name = "Ontario", CountryId = 2 },
                            new Province { Name = "Quebec", CountryId = 2 },
                            new Province { Name = "Jalisco", CountryId = 3 }
                        };

                    // Seed UserDetail
                    var users = new List<UserDetail>()
                        {
                            new UserDetail { Login = "TestLogin1@gmail.com", Password="Password1", ProvinceId = 1 },
                            new UserDetail { Login = "TestLogin2@gmail.com", Password="Password2", ProvinceId = 2 },
                            new UserDetail { Login = "TestLogin3@gmail.com", Password="Password3", ProvinceId = 3 },
                            new UserDetail { Login = "TestLogin4@gmail.com", Password="Password4", ProvinceId = 4 },
                            new UserDetail { Login = "TestLogin5@gmail.com", Password="Password5", ProvinceId = 5 }
                        };


                    if (!context.Countries.Any())
                    {
                        context.Countries.AddRange(countries);
                        context.SaveChanges();
                    }

                    if (!context.Provinces.Any())
                    {
                        context.Provinces.AddRange(provinces);
                        context.SaveChanges();
                    }

                    if (!context.UserDetails.Any())
                    {
                        context.UserDetails.AddRange(users);
                        context.SaveChanges();
                    }


                    


                }
            }
        }
    }
}

