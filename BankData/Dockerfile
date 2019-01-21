FROM microsoft/dotnet:2.1-aspnetcore-runtime
ARG source
WORKDIR /app
EXPOSE 80
LABEL com.proactis.product="training_application"
COPY ${source:-bin/Debug/netcoreapp2.1/publish} .
ENTRYPOINT ["dotnet", "BankData.dll"]
