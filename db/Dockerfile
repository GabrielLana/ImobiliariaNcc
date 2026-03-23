FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=SenhaDb1701!

USER root
WORKDIR /app

COPY InitialStructure.sql /app/InitialStructure.sql
COPY entrypoint.sh /app/entrypoint.sh

RUN chmod +x /app/entrypoint.sh

CMD /app/entrypoint.sh