FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

COPY *.sln ./
COPY ./source ./source
COPY ./tests ./tests

RUN dotnet restore

WORKDIR /app/source/Checkout.Payment.Gateway.Api
RUN dotnet publish -c Release -o out

FROM build-env AS testrunner

WORKDIR /app

RUN dotnet test

FROM scratch as export-test-results

COPY --from=testrunner /app/tests .

FROM mcr.microsoft.com/dotnet/aspnet:6.0

COPY --from=build-env /app/source/Checkout.Payment.Gateway.Api/out .

ENTRYPOINT ["dotnet", "Checkout.Payment.Gateway.Api.dll"]