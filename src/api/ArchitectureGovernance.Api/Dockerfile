FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/api/ArchitectureGovernance.Api/ArchitectureGovernance.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /src/prompts ./prompts

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

USER app
ENTRYPOINT ["dotnet", "ArchitectureGovernance.Api.dll"]
