#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/Kiosk.DeliveryWebApi/Kiosk.DeliveryWebApi/Kiosk.DeliveryWebApi.csproj", "Microservices/Kiosk.DeliveryWebApi/Kiosk.DeliveryWebApi/"]
COPY ["Core/Kiosk.Core/Kiosk.Core.csproj", "Core/Kiosk.Core/"]
COPY ["Core/Kiosk.JwtAuthenticationManager/Kiosk.JwtAuthenticationManager.csproj", "Core/Kiosk.JwtAuthenticationManager/"]
RUN dotnet restore "Microservices/Kiosk.DeliveryWebApi/Kiosk.DeliveryWebApi/Kiosk.DeliveryWebApi.csproj"
COPY . .
WORKDIR "/src/Microservices/Kiosk.DeliveryWebApi/Kiosk.DeliveryWebApi"
RUN dotnet build "Kiosk.DeliveryWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kiosk.DeliveryWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kiosk.DeliveryWebApi.dll"]