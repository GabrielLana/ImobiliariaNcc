FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ImobiliariaNcc/src/ImobiliariaNcc.Application/ImobiliariaNcc.Application.csproj", "ImobiliariaNcc.Application/"]
COPY ["ImobiliariaNcc/src/ImobiliariaNcc.WebAPI/ImobiliariaNcc.WebAPI.csproj", "ImobiliariaNcc.WebAPI/"]
COPY ["ImobiliariaNcc/src/ImobiliariaNcc.Domain/ImobiliariaNcc.Domain.csproj", "ImobiliariaNcc.Domain/"]
COPY ["ImobiliariaNcc/src/ImobiliariaNcc.Infrastructure/ImobiliariaNcc.Infrastructure.csproj", "ImobiliariaNcc.Infrastructure/"]

RUN dotnet restore "ImobiliariaNcc.WebAPI/ImobiliariaNcc.WebAPI.csproj"

COPY . .
WORKDIR "/src/ImobiliariaNcc/src/ImobiliariaNcc.WebAPI"
RUN dotnet build "ImobiliariaNcc.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImobiliariaNcc.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ImobiliariaNcc.WebAPI.dll"]