#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateway/Kiosk.ApiGateway/Kiosk.ApiGateway.csproj", "ApiGateway/Kiosk.ApiGateway/"]
COPY ["Core/Kiosk.JwtAuthenticationManager/Kiosk.JwtAuthenticationManager.csproj", "Core/Kiosk.JwtAuthenticationManager/"]
COPY ["Core/Kiosk.Core/Kiosk.Core.csproj", "Core/Kiosk.Core/"]
RUN dotnet restore "ApiGateway/Kiosk.ApiGateway/Kiosk.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway/Kiosk.ApiGateway"
RUN dotnet build "Kiosk.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kiosk.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kiosk.ApiGateway.dll"]