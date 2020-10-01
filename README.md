# TechEval

Applcaition uses DB, amend db connection string or use docker sql instance.
Migration runs every time you run the project.
### To run local docker containter for DB 
```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=.Passw0rd' -p 1433:1433 --rm -d mcr.microsoft.com/mssql/server:2019-GA-ubuntu-16.04
```

To query loaded date use url /api/Transaction .

You can use OData to get necessary data i.e.:

https://localhost:32784/api/Transaction?$orderby=Amount&$filter=CurrencyCode eq 'USD'
https://localhost:32784/api/Transaction?$orderby=Amount&$filter=TransactionDate gt '2019-02-20' and TransactionDate lt '2020-02-20'
https://localhost:32784/api/Transaction?$filter=Status eq 'A'

### Unit tests
Not impemented :( Do not have time


