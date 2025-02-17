FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Exadel.HEH.Backend.Host/Exadel.HEH.Backend.Host.csproj", "Exadel.HEH.Backend.Host/"]
RUN dotnet restore "Exadel.HEH.Backend.Host/Exadel.HEH.Backend.Host.csproj"
COPY . .
WORKDIR "/src/Exadel.HEH.Backend.Host"
RUN dotnet build "Exadel.HEH.Backend.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exadel.HEH.Backend.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Exadel.HEH.Backend.Host.dll
#ENTRYPOINT [ "dotnet", "Exadel.HEH.Backend.Host.dll" ]
