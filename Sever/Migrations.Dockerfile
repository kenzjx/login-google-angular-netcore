FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app
COPY Sever.csproj ./
COPY Setup.sh Setup.sh

RUN dotnet tool install --global dotnet-ef

RUN dotnet restore ./Sever.csproj
COPY . .
WORKDIR /app/.

RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations

RUN chmod +x ./Setup.sh
CMD /bin/bash ./Setup.sh