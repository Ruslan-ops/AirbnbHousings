#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Airbnb.Web/Airbnb.Web.csproj", "Airbnb.Web/"]
COPY ["Airbnb.Application/Airbnb.Application.csproj", "Airbnb.Application/"]
COPY ["Airbnb.Domain/Airbnb.Domain.csproj", "Airbnb.Domain/"]
RUN dotnet restore "Airbnb.Web/Airbnb.Web.csproj"
COPY . .
WORKDIR "/src/Airbnb.Web"
RUN dotnet build "Airbnb.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Airbnb.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Airbnb.Web.dll"]