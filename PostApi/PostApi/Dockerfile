﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PostApi/PostApi.csproj", "PostApi/"]
RUN dotnet restore "PostApi/PostApi.csproj"
COPY . .
WORKDIR "/src/PostApi"
RUN dotnet build "PostApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostApi.dll"]
