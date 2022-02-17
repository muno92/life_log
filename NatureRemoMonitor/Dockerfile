FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NatureRemoMonitor/NatureRemoMonitor.csproj", "NatureRemoMonitor/"]
RUN dotnet restore "NatureRemoMonitor/NatureRemoMonitor.csproj"
COPY . .
WORKDIR "/src/NatureRemoMonitor"
RUN dotnet build "NatureRemoMonitor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NatureRemoMonitor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NatureRemoMonitor.dll"]
