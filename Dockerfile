# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env

WORKDIR /app
ADD /src .
RUN dotnet publish Starfish.Web/Starfish.Web.csproj -c Release -o ./output

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build-env /app/output .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Starfish.Web.dll"]