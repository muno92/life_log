FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nature_remo_monitor/nature_remo_monitor.csproj", "nature_remo_monitor/"]
RUN dotnet restore "nature_remo_monitor/nature_remo_monitor.csproj"
COPY . .
WORKDIR "/src/nature_remo_monitor"
RUN dotnet build "nature_remo_monitor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "nature_remo_monitor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nature_remo_monitor.dll"]
