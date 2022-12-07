#!/bin/sh
if [ ! -f initialized ] ; then
  while true ; do
    sleep 1s
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Your_password123 -d master -Q "SELECT 1" && break
  done
  sleep 5s
fi
