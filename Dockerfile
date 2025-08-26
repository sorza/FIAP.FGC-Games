# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia os arquivos da solução
COPY . .

# Restaura os pacotes NuGet
RUN dotnet restore

# Publica a aplicação em modo Release
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Expõe a porta usada pela API
EXPOSE 5000

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "Fgc.Api.dll"]