﻿FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SpeedTest/SpeedTest.csproj", "SpeedTest/"]
RUN dotnet restore "SpeedTest/SpeedTest.csproj"
COPY . .
WORKDIR "/src/SpeedTest"
RUN dotnet build "SpeedTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpeedTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpeedTest.dll"]
