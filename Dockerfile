FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine3.9 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore

RUN dotnet publish ./Onboarding-Backend/Onboarding-Backend.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine3.9
WORKDIR /Onboarding-Backend
COPY --from=build-env /app/Onboarding-Backend/out .
ENTRYPOINT ["dotnet","Onboarding-Backend.dll"]