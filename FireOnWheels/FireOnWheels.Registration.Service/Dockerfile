#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FireOnWheels.Registration.Service/FireOnWheels.Registration.Service.csproj", "FireOnWheels.Registration.Service/"]
COPY ["FireOnWheels.Contracts/FireOnWheels.Contracts.csproj", "FireOnWheels.Contracts/"]
RUN dotnet restore "FireOnWheels.Registration.Service/FireOnWheels.Registration.Service.csproj"
COPY . .
WORKDIR "/src/FireOnWheels.Registration.Service"
RUN dotnet build "FireOnWheels.Registration.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FireOnWheels.Registration.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FireOnWheels.Registration.Service.dll"]