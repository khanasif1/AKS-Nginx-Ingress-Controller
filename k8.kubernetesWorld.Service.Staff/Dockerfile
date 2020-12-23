FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["k8.kubernetesWorld.Service.Staff.csproj", "www/staff/"]
RUN dotnet restore www/staff/k8.kubernetesWorld.Service.Staff.csproj
WORKDIR "/src/www/staff"
COPY . .
RUN dotnet build "k8.kubernetesWorld.Service.Staff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "k8.kubernetesWorld.Service.Staff.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "k8.kubernetesWorld.Service.Staff.dll"]