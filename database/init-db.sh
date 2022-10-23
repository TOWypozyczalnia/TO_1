#!/bin/sh
sleep 20s
echo "---------------------------------------- running script ----------------------------------------"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i scripts/init_database.sql