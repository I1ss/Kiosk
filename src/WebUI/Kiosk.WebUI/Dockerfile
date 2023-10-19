#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebUI/Kiosk.WebUI/Kiosk.WebUI.csproj", "WebUI/Kiosk.WebUI/"]
COPY ["Core/Kiosk.Core/Kiosk.Core.csproj", "Core/Kiosk.Core/"]
RUN dotnet restore "WebUI/Kiosk.WebUI/Kiosk.WebUI.csproj"
COPY . .
WORKDIR "/src/WebUI/Kiosk.WebUI"
RUN dotnet build "Kiosk.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kiosk.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kiosk.WebUI.dll"]