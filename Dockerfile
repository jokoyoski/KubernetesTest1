# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

ENV ASPNETCORE_ENVIRONMENT Production



expose 5000


WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .



ENTRYPOINT ["dotnet", "KubernetesTestClusterIP.dll"]