FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY ["Myriolang.ConlangDev.API/Myriolang.ConlangDev.API.csproj", "./"]
RUN dotnet restore "./Myriolang.ConlangDev.API.csproj"

COPY ["Myriolang.ConlangDev.API/.", "."]
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80

COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Myriolang.ConlangDev.API.dll"]