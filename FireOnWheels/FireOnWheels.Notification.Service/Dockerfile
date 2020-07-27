#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FireOnWheels.Notification.Service/FireOnWheels.Notification.Service.csproj", "FireOnWheels.Notification.Service/"]
COPY ["FireOnWheels.Contracts/FireOnWheels.Contracts.csproj", "FireOnWheels.Contracts/"]
RUN dotnet restore "FireOnWheels.Notification.Service/FireOnWheels.Notification.Service.csproj"
COPY . .
WORKDIR "/src/FireOnWheels.Notification.Service"
RUN dotnet build "FireOnWheels.Notification.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FireOnWheels.Notification.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FireOnWheels.Notification.Service.dll"]