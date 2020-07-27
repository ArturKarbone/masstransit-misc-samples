#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FireOnWheels.Registration/FireOnWheels.Registration.csproj", "FireOnWheels.Registration/"]
COPY ["FireOnWheels.Contracts/FireOnWheels.Contracts.csproj", "FireOnWheels.Contracts/"]
RUN dotnet restore "FireOnWheels.Registration/FireOnWheels.Registration.csproj"
COPY . .
WORKDIR "/src/FireOnWheels.Registration"
RUN dotnet build "FireOnWheels.Registration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FireOnWheels.Registration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FireOnWheels.Registration.dll"]