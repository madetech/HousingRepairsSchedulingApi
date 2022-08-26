FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HousingRepairsSchedulingApi/HousingRepairsSchedulingApi.csproj", "HousingRepairsSchedulingApi/"]
ARG USERNAME
ARG PASSWORD
RUN dotnet nuget add source --username $USERNAME --password $PASSWORD --store-password-in-clear-text --name github "https://nuget.pkg.github.com/City-of-Lincoln-Council/index.json"
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
