#!/bin/bash

/opt/mssql/bin/sqlservr &

echo "Aguardando o SQL Server iniciar..."
for i in {1..50};
do
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" -C &> /dev/null
    if [ $? -eq 0 ]
    then
        echo "SQL Server pronto! Executando script de estrutura..."
        /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /app/InitialStructure.sql -C
        break
    else
        echo "Ainda não está pronto... (tentativa $i)"
        sleep 2
    fi
done

wait