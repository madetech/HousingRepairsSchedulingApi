﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HousingRepairsSchedulingApi/HousingRepairsSchedulingApi.csproj", "HousingRepairsSchedulingApi/"]
RUN dotnet restore "HousingRepairsSchedulingApi/HousingRepairsSchedulingApi.csproj"
COPY . .
WORKDIR "/src/HousingRepairsSchedulingApi"
RUN dotnet build "HousingRepairsSchedulingApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HousingRepairsSchedulingApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HousingRepairsSchedulingApi.dll"]
