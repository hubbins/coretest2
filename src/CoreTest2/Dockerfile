FROM microsoft/dotnet:core
MAINTAINER "Sean O'Connor <sean.oconnor@morningstar.com>"
RUN mkdir /root/coretest
COPY ./bin/release/netcoreapp1.0/publish /root/coretest
EXPOSE 5000
WORKDIR /root/coretest
CMD ["dotnet", "./CoreTest2.dll"]
