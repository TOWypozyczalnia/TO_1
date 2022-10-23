FROM mcr.microsoft.com/mssql/server:latest

USER root

WORKDIR /db
EXPOSE 1433
COPY ./ ./
RUN chmod +x entrypoint.sh init-db.sh

CMD ["/bin/bash", "entrypoint.sh"]
