FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
USER root
WORKDIR /app

COPY InitialStructure.sql ./InitialStructure.sql
COPY entrypoint.sh ./entrypoint.sh

RUN chmod +x ./entrypoint.sh

CMD ["/bin/bash", "./entrypoint.sh"]