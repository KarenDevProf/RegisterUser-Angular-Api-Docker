#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RegisterUserAPI/RegisterUser.API/RegisterUser.API.csproj", "RegisterUserAPI/RegisterUser.API/"]
COPY ["RegisterUserAPI/RegisterUser.BusinessLayer/RegisterUser.BusinessLayer.csproj", "RegisterUserAPI/RegisterUser.BusinessLayer/"]
COPY ["RegisterUserAPI/RegisterUser.Repositories/RegisterUser.Repositories.csproj", "RegisterUserAPI/RegisterUser.Repositories/"]
COPY ["RegisterUserAPI/RegisterUser.DAL/RegisterUser.DAL.csproj", "RegisterUserAPI/RegisterUser.DAL/"]
RUN dotnet restore "RegisterUserAPI/RegisterUser.API/RegisterUser.API.csproj"
COPY . .
WORKDIR "/src/RegisterUserAPI/RegisterUser.API"
RUN dotnet build "RegisterUser.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RegisterUser.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RegisterUser.API.dll"]