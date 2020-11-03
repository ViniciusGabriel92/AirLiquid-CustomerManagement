# CustomerManagement
Projeto desenvolvido para atender a necessidade de gerenciamento de clientes da AirLiquid.

# Getting Started

### Requisitos

[SDK .NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.401-windows-x64-installer)
[.NET Core Windows Server Hosting 3.1](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-3.1.7-windows-hosting-bundle-installer)

## Instalação
Instalar pacotes
```
dotnet restore
```

## Build run and test

Build production
```bash
dotnet build
dotnet publish -c Release
```
Rodar local
```bash
dotnet run
```
Executar testes
```bash
dotnet test
```

# Docker
## Build docker image
```bash
docker build -t image-customermanagement .
```
## Run docker image
```bash
docker run -d -p 8080:80 --name customermanagementapi image-customermanagement
```