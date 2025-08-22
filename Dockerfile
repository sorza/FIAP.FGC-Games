FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução
COPY FIAP.FGC-Games.sln ./

# Copia cada csproj no caminho correto
COPY Fgc.Api/Fgc.Api.csproj Fgc.Api/
COPY Fgc.Application/Fgc.Application.csproj Fgc.Application/
COPY Fgc.Domain/Fgc.Domain.csproj Fgc.Domain/
COPY Fgc.Infrastructure/Fgc.Infrastructure.csproj Fgc.Infrastructure/
COPY Fgc.Tests/Fgc.Tests.csproj Fgc.Tests/

# Restaura as dependências
RUN dotnet restore FIAP.FGC-Games.sln

# Copia o restante do código
COPY . .

# Publica o projeto principal
RUN dotnet publish Fgc.Api/Fgc.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Fgc.Api.dll"]