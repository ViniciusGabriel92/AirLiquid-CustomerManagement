FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["CustomerManagement/CustomerManagement.csproj", "CustomerManagement/"]
COPY ["CustomerManagement.WebAPI/CustomerManagement.WebAPI.csproj", "CustomerManagement.WebAPI/"]
RUN dotnet restore "CustomerManagement.WebAPI/CustomerManagement.WebAPI.csproj"
RUN dotnet restore "CustomerManagement/CustomerManagement.csproj"

COPY . .
WORKDIR "/src/CustomerManagement.WebAPI"
RUN dotnet build "CustomerManagement.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerManagement.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerManagement.WebAPI.dll"]
