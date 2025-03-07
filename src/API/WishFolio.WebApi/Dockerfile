#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/API/WishFolio.WebApi/WishFolio.WebApi.csproj", "src/API/WishFolio.WebApi/"]
COPY ["src/API/WishFolio.Infrastructure/WishFolio.Infrastructure.csproj", "src/API/WishFolio.Infrastructure/"]
COPY ["src/API/WishFolio.Application/WishFolio.Application.csproj", "src/API/WishFolio.Application/"]
COPY ["src/API/WishFolio.Domain/WishFolio.Domain.csproj", "src/API/WishFolio.Domain/"]
RUN dotnet restore "./src/API/WishFolio.WebApi/WishFolio.WebApi.csproj"
COPY . .
WORKDIR "/src/src/API/WishFolio.WebApi"
RUN dotnet build "./WishFolio.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WishFolio.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WishFolio.WebApi.dll"]