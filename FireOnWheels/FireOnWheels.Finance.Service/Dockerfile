#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FireOnWheels.Finance.Service/FireOnWheels.Finance.Service.csproj", "FireOnWheels.Finance.Service/"]
COPY ["FireOnWheels.Contracts/FireOnWheels.Contracts.csproj", "FireOnWheels.Contracts/"]
RUN dotnet restore "FireOnWheels.Finance.Service/FireOnWheels.Finance.Service.csproj"
COPY . .
WORKDIR "/src/FireOnWheels.Finance.Service"
RUN dotnet build "FireOnWheels.Finance.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FireOnWheels.Finance.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FireOnWheels.Finance.Service.dll"]