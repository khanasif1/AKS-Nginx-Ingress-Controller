FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["k8.kubernetesWorld.Web.csproj", "www/web/"]
RUN dotnet restore "www/web/k8.kubernetesWorld.Web.csproj"
WORKDIR /src/www/web/
COPY . .
RUN dotnet build "k8.kubernetesWorld.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "k8.kubernetesWorld.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "k8.kubernetesWorld.Web.dll"]